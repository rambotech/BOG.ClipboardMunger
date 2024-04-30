using BOG.ClipboardMunger.Common.Helper;
using BOG.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace BOG.ClipboardMunger
{
	public partial class frmScriptExec : Form
	{
		private AssemblyVersion info = new BOG.Framework.AssemblyVersion(true);
		private string RootFormTitle;
		private string SelectedScriptName = string.Empty;
		private bool ScriptTreeRebuilding = false;
		private MethodRetriever _MethodRetriever = null;
		private StringDictionary _AppSettings = new StringDictionary();

		private string searchFilter = string.Empty;
		private string selectedNodeKey = string.Empty;
		private bool Terminating = false;

		public frmScriptExec()
		{
			try
			{
				InitializeComponent();

				RootFormTitle = this.Text + $" ({info.Version}, {info.BuildDate})";

				this.notifyIcon1.BalloonTipTitle = "Clipboard Munger";
				this.notifyIcon1.BalloonTipText = "Right-click for options";
				BOG.Framework.AssemblyVersion a = new BOG.Framework.AssemblyVersion(System.Reflection.Assembly.GetEntryAssembly());
				_MethodRetriever = new MethodRetriever();
				this.trayContextMenu1.Items.Clear();
				LoadScriptTree();
			}
			catch (Exception err)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref err), "Error during initialization", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Environment.Exit(1);
			}
		}

		private void LoadScriptTree()
		{
			ScriptTreeRebuilding = true;
			var methods = new List<string>();
			this.trayContextMenu1.Items.Clear();
			this.trayContextMenu1.Items.Add("&Restore GUI");
			this.trayContextMenu1.Items.Add("-");

			foreach (var itemKey in _MethodRetriever.mungers.Keys)
			{
				if (itemKey.ToLower().Contains(searchFilter))
				{
					methods.Add(itemKey);
				}
			}
			this.trvScripts.Nodes.Clear();
			if (methods.Count > 0)
			{
				methods.Sort();
				var groupName = string.Empty;

				TreeNode groupNode = null;
				foreach (var item in methods)
				{
					var values = item.Split(new string[] { "::" }, StringSplitOptions.None);
					if (values[0] != groupName)
					{
						if (groupNode != null)
						{
							this.trvScripts.Nodes.Add(groupNode);
						}
						groupName = values[0];
						groupNode = new TreeNode(groupName);
					}
					groupNode.Nodes.Add(values[1]);
					this.trayContextMenu1.Items.Add(item);
				}
				this.trvScripts.ExpandAll();
				if (groupNode != null)
				{
					this.trvScripts.Nodes.Add(groupNode);
				}
				this.trvScripts.ExpandAll();
			}
			if (!string.IsNullOrWhiteSpace(searchFilter))
			{
				this.toolStripStatusLabel1.Text = $"Matching displayed: {methods.Count}, Total: {_MethodRetriever.mungers.Keys.Count}";
			}
			else
			{
				this.toolStripStatusLabel1.Text = $"Total: {_MethodRetriever.mungers.Keys.Count}";
			}
			this.txtFilter.BackColor = methods.Count == 0 ? Color.LightCoral : Color.White;
			this.trayContextMenu1.Items.Add("-");
			this.trayContextMenu1.Items.Add("E&xit");

			ScriptTreeRebuilding = false;
		}

		private void trvScripts_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Nodes.Count > 0)
			{
				this.Text = RootFormTitle;
				this.tabpageClipboardContent.Select();
				this.tabstripScript.Visible = false;
				this.btnMunge.Enabled = false;
				this.cbxExampleList.Enabled = false;
				this.tabpageScript.Text = "Arguments";
				return;
			}
			selectedNodeKey = e.Node.Parent.Text + "::" + e.Node.Text;
			this.Text = RootFormTitle + $" - {selectedNodeKey}";

			this.dgScriptArguments.Rows.Clear();
			this.cbxExampleList.Items.Clear();
			this.txtTestInput.Text = string.Empty;
			this.txtTestOutput.Text = string.Empty;
			this.dgvArgValues.Rows.Clear();
			foreach (var item in _MethodRetriever.mungers[selectedNodeKey].GetArguments())
			{
                this.dgScriptArguments.Rows.Add(new object[]
				{
                    item.Value.Name,
					item.Value.DefaultValue,
					item.Value.Description,
					item.Value.ValidatorRegex
                });
                this.dgvArgValues.Rows.Add(new object[]
				{
					item.Value.Name,
					string.Empty
				});
            }
            foreach (var exampleName in _MethodRetriever.mungers[selectedNodeKey].GetExampleNames())
			{
				this.cbxExampleList.Items.Add(exampleName);
			}
			this.cbxExampleList.Enabled = this.cbxExampleList.Items.Count > 0;

			this.tabpageScript.Text = $"Arguments for {e.Node.Text} ({e.Node.Parent.Text})";
			this.tabstripScript.Visible = true;
			this.tabstripScript.Show();
			this.tabArguments.Select();
			this.btnMunge.Enabled = true;
		}

		private void frmScriptExec_Activated(object sender, EventArgs e)
		{
			this.txtClipboardContent.Text = Clipboard.GetText();
			this.toolStripStatusLabel1.Text = string.Format("{0:#,#} byte(s) currently on the clipboard", this.txtClipboardContent.Text.Length);
		}

		private void txtFilter_TextChanged(object sender, EventArgs e)
		{
			timer1.Stop();
			searchFilter = this.txtFilter.Text.ToLower();
			timer1.Interval = 700;
			timer1.Enabled = true;
			timer1.Start();
		}

		private void btnClearFilter_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			searchFilter = string.Empty;
			this.txtFilter.Text = string.Empty;
			timer1.Interval = 700;
			timer1.Enabled = true;
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (!ScriptTreeRebuilding)
			{
				timer1.Stop();
				timer1.Enabled = false;
				LoadScriptTree();
			}
		}

		private void btnMunge_Click(object sender, EventArgs e)
		{
			this.txtError.Text = String.Empty;
			var o = _MethodRetriever.mungers[selectedNodeKey];
			o.ClipboardSource = this.txtClipboardContent.Text;

			var args = new Dictionary<string, string>();
			string argName = string.Empty;
			string argValue = string.Empty;
			string argValidator = string.Empty;
			var IsScriptExecuted = true;
			try
			{
				var argSet = o.GetArguments();
				foreach (var key in argSet.Keys)
				{
					var input = new InputBox(argSet[key]);
					input.ShowDialog();
					if (input.IsCancelled)
					{
						IsScriptExecuted = false;
						break;
					}
					args.Add(argSet[key].Name, input.ValueEntered);
				}
			}
			catch (Exception err)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref err), "Error with argument(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!IsScriptExecuted)
			{
				MessageBox.Show("Cancelled", "Munger not run against clipboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				this.txtClipboardContent.Text = o.MungeClipboard(args);
				if (!string.IsNullOrEmpty(this.txtClipboardContent.Text))
				{
					Clipboard.SetText(this.txtClipboardContent.Text);
				}
				else
				{
					Clipboard.Clear();
				}
			}
			catch (Exception err)
			{
				this.txtError.Text = DetailedException.WithUserContent(ref err);
				MessageBox.Show(err.Message, "Error tab has details", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.tabpageError.Focus();
				this.txtError.Focus();
			}
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void frmScriptExec_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Terminating) return;

			bool SubjectToCancellation = true;
			if (SubjectToCancellation && e.CloseReason != CloseReason.UserClosing)
			{
				SubjectToCancellation = false;
			}
			if (SubjectToCancellation && MessageBox.Show("The application is normally minimized to the tray, but this action will shut it down.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
			if (!e.Cancel)
			{
				e.Cancel = true;
				this.notifyIcon1.Visible = false;
				this.notifyIcon1.Dispose();
				Terminating = true;
				this.timerShutdown.Interval = 100;
				this.timerShutdown.Enabled = true;
				this.timerShutdown.Start();
			}
		}

		private void timerShutdown_Tick(object sender, EventArgs e)
		{
			timerShutdown.Stop();
			timerShutdown.Enabled = false;
			if (Terminating)
			{
				this.Close();
			}
		}

		private void trayContextMenu1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "&Restore GUI")
			{
				Show();
				WindowState = FormWindowState.Normal;
			}
			else if (e.ClickedItem.Text == "E&xit")
			{
				this.Close();
			}
		}

		private void frmScriptExec_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState)
				Hide();
		}

		private void cbxExampleList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				var selectedTest = cbxExampleList.SelectedItem.ToString();
				var thisExample = _MethodRetriever.mungers[selectedNodeKey].GetExample(selectedTest);
				this.txtTestInput.Text = thisExample.Input;
				this.dgvArgValues.Rows.Clear();
				if (thisExample.ArgumentValues?.Count > 0)
				{
					foreach (var argItem in thisExample.ArgumentValues.Keys)
					{
						this.dgvArgValues.Rows.Add(new object[]
						{
						argItem,
						thisExample.ArgumentValues[argItem]
						});
					}
				}
				this.txtTestOutput.Text = _MethodRetriever.mungers[selectedNodeKey].MungeClipboard(
					thisExample.ArgumentValues,
					thisExample.Input
				);
			}
			catch (Exception err)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref err), "Error executing example", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void trvScripts_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Nodes.Count > 0)
			{
				this.Text = RootFormTitle;
				this.tabstripScript.Visible = false;
				this.btnMunge.Enabled = false;
				this.cbxExampleList.Enabled = false;
				this.tabpageScript.Text = "Arguments";
				return;
			}
			selectedNodeKey = e.Node.Parent.Text + "::" + e.Node.Text;
			this.Text = RootFormTitle + $" - {selectedNodeKey}";

			this.dgScriptArguments.Rows.Clear();
			this.cbxExampleList.Items.Clear();
			this.txtTestInput.Text = string.Empty;
			this.txtTestOutput.Text = string.Empty;
			this.dgvArgValues.Rows.Clear();
			foreach (var item in _MethodRetriever.mungers[selectedNodeKey].GetArguments())
			{
				this.dgScriptArguments.Rows.Add(new object[]
				{
					item.Value.Name,
					item.Value.DefaultValue,
					item.Value.Description,
					item.Value.ValidatorRegex
				});
				this.dgvArgValues.Rows.Add(new object[]
				{
					item.Value.Name
				});
			}
			foreach (var exampleName in _MethodRetriever.mungers[selectedNodeKey].GetExampleNames())
			{
				this.cbxExampleList.Items.Add(exampleName);
			}
			if (this.cbxExampleList.Items.Count == 0)
			{
				this.cbxExampleList.Enabled = false;
			}
			else
			{
				this.cbxExampleList.Enabled = true;
				this.cbxExampleList.SelectedIndex = 0;
			}

			this.tabpageScript.Text = $"Arguments for {e.Node.Text} ({e.Node.Parent.Text})";
			this.tabstripScript.Visible = true;
			this.tabstripScript.Show();
			this.btnMunge.Enabled = true;
		}
	}
}
