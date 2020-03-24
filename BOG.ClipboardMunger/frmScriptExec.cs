using BOG.ClipboardMunger.Common.Helper;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOG.ClipboardMunger
{
	public partial class frmScriptExec : Form
	{
		private string SelectedScriptName = string.Empty;
		private bool ScriptTreeRebuilding = false;
		private MethodRetriever _MethodRetriever = null;
		private StringDictionary _AppSettings = new StringDictionary();

		private string searchFilter = string.Empty;

		private int ContextMenuScriptSelection = -1;
		private int CurrentScriptSelectedIndex = -1;
		private bool Terminating = false;

		public frmScriptExec()
		{
			try
			{
				InitializeComponent();
				this.notifyIcon.BalloonTipTitle = "Clipboard Munger";
				this.notifyIcon.BalloonTipText = "Right-click for options";
				BOG.Framework.AssemblyVersion a = new BOG.Framework.AssemblyVersion(System.Reflection.Assembly.GetEntryAssembly());
				_MethodRetriever = new MethodRetriever();
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
				}
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
			ScriptTreeRebuilding = false;
		}

		private void ScriptExec_Load(object sender, EventArgs e)
		{

		}

		private void trvScripts_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Nodes.Count > 0)
			{
				this.tabstripScript.Visible = false;
				this.tabpageScript.Text = "Select a script for use";
				return;
			}
			var nodeKey = e.Node.Parent.Text + "::" + e.Node.Text;
			this.dgScriptArguments.Rows.Clear();
			foreach (var item in _MethodRetriever.mungers[nodeKey].GetArguments())
			{
				this.dgScriptArguments.Rows.Add(new object[]
				{
					item.Value.Name,
					item.Value.DefaultValue,
					item.Value.Description,
					item.Value.ValidatorRegex
				});
			}
			this.tabpageScript.Text = $"Script for {e.Node.Text} ({e.Node.Parent.Text})";
			this.tabstripScript.Visible = true;
		}

		private void frmScriptExec_Activated(object sender, EventArgs e)
		{
			this.txtClipboardContent.Text = Clipboard.GetText();
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
	}
}
