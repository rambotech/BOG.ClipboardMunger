using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BOG.Framework;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace BOG.ClipboardMunger
{
	public partial class MainForm : Form
	{
		private string ScriptPath = Path.GetDirectoryName(Application.ExecutablePath);
		private string SelectedScriptName = string.Empty;
		private bool ContentsChanged = false;
		private bool AssemblyListRebuilding = false;
		private Interfaces.IClipboard _compiledScript = null;
		private string _ExecutablePath = string.Empty;
		private string _ConfigurationPath = string.Empty;
		private string _ConfigurationFile = string.Empty;
		private string _DefaultConfigurationFile = string.Empty;
		private SettingsDictionary _AppSettings;
		private string _StorageFolder = string.Empty;
		private ClipboardMungerScriptContainer _csmc = new ClipboardMungerScriptContainer();

		private int ContextMenuScriptSelection = -1;
		private int CurrentScriptSelectedIndex = -1;
		private bool Terminating = false;

		public MainForm()
		{
			InitializeComponent();
			this.notifyIcon1.BalloonTipTitle = "Clipboard Munger";
			this.notifyIcon1.BalloonTipText = "Right-click for options";
			BOG.Framework.AssemblyVersion a = new BOG.Framework.AssemblyVersion(System.Reflection.Assembly.GetEntryAssembly());
			_ExecutablePath = a.FullPath.Substring(0, a.FullPath.Length - a.Name.Length);
			_ConfigurationPath = Path.Combine(Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), "Bits of Genius"), "Weed Killer Manager");
			_ConfigurationFile = Path.Combine(_ConfigurationPath, "usersettings.xml");
			_DefaultConfigurationFile = Path.Combine(_ExecutablePath, "default_usersettings.xml");
			_StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

			SetHighlighting("C#");
			this.txtScript.Refresh();
			this.txtScript.ShowTabs = false;
			this.txtScript.ShowSpaces = false;
			this.txtScript.ShowEOLMarkers = false;

			try
			{
				if (!File.Exists(_DefaultConfigurationFile))
				{
					_AppSettings = new SettingsDictionary(_DefaultConfigurationFile);
					_AppSettings.SetSetting("ClipboardMunger.ScriptFolder", ScriptPath);
					_AppSettings.SaveSettings();
				}
			}
			catch
			{
			}

			try
			{
				if (!File.Exists(_ConfigurationFile) && File.Exists(_DefaultConfigurationFile))
				{
					File.Copy(_DefaultConfigurationFile, _ConfigurationFile);
				}
			}
			catch
			{
			}

			_AppSettings = new SettingsDictionary(_ConfigurationFile);
			_AppSettings.LoadSettings();
			ContentsChanged = false;
		}

		private void LoadGacList()
		{
			AssemblyListRebuilding = true;
			if (lvwGacList.Items.Count > 0)
			{
				// does a delta to adjust the two listviews
				foreach (ListViewItem lvi in lvwGacReference.Items)
				{
					if (!_csmc.References.Contains((string) lvi.Tag))
					{
						GacReferenceChange(false, lvi);
					}
				}

				foreach (ListViewItem lvi in lvwGacList.Items)
				{
					if (_csmc.References.Contains((string) lvi.Tag))
					{
						GacReferenceChange(true, lvi);
					}
				}
			}
			else
			{
				// Full Load only when it has never been loaded.
				lvwGacReference.Items.Clear();
				lvwGacList.Items.Clear();
				System.GAC.IAssemblyEnum ae = System.GAC.AssemblyCache.CreateGACEnum();
				System.GAC.IAssemblyName an;
				while (System.GAC.AssemblyCache.GetNextAssembly(ae, out an) == 0)
				{
					string GacDisplayName = System.GAC.AssemblyCache.GetDisplayName(an, System.GAC.ASM_DISPLAY_FLAGS.CULTURE | System.GAC.ASM_DISPLAY_FLAGS.PUBLIC_KEY_TOKEN | System.GAC.ASM_DISPLAY_FLAGS.VERSION);
					string GacNamespace = System.GAC.AssemblyCache.GetName(an);
					ListViewItem l = new ListViewItem(GacDisplayName);
					l.Tag = GacDisplayName;
					l.SubItems.Add(GacNamespace);
					l.Checked = false;
					if (_csmc.References.Contains(GacDisplayName))
					{
						lvwGacReference.Items.Add(l);
					}
					else
					{
						lvwGacList.Items.Add(l);
					}
				}
			}

			AssemblyListRebuilding = false;
		}

		private int GetIndexForTag(ref ListView v, object tag)
		{
			int ResultIndex = 0;
			for (ResultIndex = 0; ResultIndex < v.Items.Count; ResultIndex++)
			{
				if (v.Items[ResultIndex].Tag == tag)
				{
					break;
				}
			}

			return ResultIndex == v.Items.Count ? -1 : ResultIndex;
		}

		private void GacReferenceChange(bool isAdding, ListViewItem lvi)
		{
			ContentsChanged = true;
			lvi.Checked = false;
			if (isAdding)
			{
				if (!_csmc.References.Contains(lvi.Name))
				{
					_csmc.References.Add(lvi.Name);
				}

				lvwGacReference.Items.Add((ListViewItem) lvi.Clone());
				//lvwGacReference.SelectedIndices = new ListView.SelectedIndexCollection(lvwGacReference).Add(GetIndexForTag(ref lvwGacReference, lvi.Tag));
				int ListIndex = GetIndexForTag(ref lvwGacList, lvi.Tag);
				lvwGacList.Items.Remove(lvi);
				//lvwGacList.SelectedIndices = new ListView.SelectedIndexCollection(lvwGacList).Add(ListIndex < lvwGacList.Items.Count ? ListIndex : lvwGacList.Items.Count - 1);
			}
			else
			{
				if (_csmc.References.Contains(lvi.Name))
				{
					_csmc.References.Remove(lvi.Name);
				}

				lvwGacList.Items.Add((ListViewItem) lvi.Clone());
				//lvwGacList.SelectedIndices = new ListView.SelectedIndexCollection(lvwGacList).Add(GetIndexForTag(ref lvwGacList, lvi.Tag));
				int ListIndex = GetIndexForTag(ref lvwGacReference, lvi.Tag);
				lvwGacReference.Items.Remove(lvi);
				//lvwGacReference.SelectedIndices = new ListView.SelectedIndexCollection(lvwGacReference).Add(ListIndex < lvwGacReference.Items.Count ? ListIndex : lvwGacReference.Items.Count - 1);
			}
		}

		private void SetHighlighting(string Strategy)
		{
			FileSyntaxModeProvider provider = new FileSyntaxModeProvider(Application.StartupPath);
			HighlightingManager manager = HighlightingManager.Manager;
			manager.AddSyntaxModeFileProvider(provider);
			this.txtScript.Document.HighlightingStrategy = manager.FindHighlighter(Strategy);
		}

		private bool LoadScriptList()
		{
			if (!Directory.Exists(ScriptPath))
			{
				return false;
			}

			lblScriptsSourceFolder.Text = ScriptPath;
			this.lstMungeScripts.Items.Clear();
			this.trayContextMenu.Items.Clear();
			this.trayContextMenu.Items.Add("&Restore GUI");
			this.trayContextMenu.Items.Add("-");

			string[] ScriptFiles = Directory.GetFiles(ScriptPath, "cm_*.xml", SearchOption.TopDirectoryOnly);

			if (ScriptFiles.Length > 0)
			{
				// Sort the scripts and display by an alphabet grouping.
				Dictionary<string, List<string>> SortedScripts = new Dictionary<string, List<string>>();
				foreach (string f in ScriptFiles)
				{
					string filenameonly = Path.GetFileName(f);
					string scriptname = filenameonly.Substring(3, filenameonly.Length - 7);
					this.lstMungeScripts.Items.Add((object) scriptname);
					string firstLetter = scriptname.Substring(0, 1);
					if (!SortedScripts.ContainsKey(firstLetter))
						SortedScripts.Add(firstLetter, new List<string>());
					SortedScripts[firstLetter].Add(scriptname);
				}

				foreach (string firstLetter in SortedScripts.Keys)
				{
					ToolStripMenuItem submenu, item;
					submenu = new ToolStripMenuItem();
					submenu.Text = firstLetter.ToUpper();
					foreach (string scriptName in SortedScripts[firstLetter])
					{
						item = new ToolStripMenuItem();
						item.Click += ScriptMenuItemClicked;
						item.Text = scriptName;
						submenu.DropDownItems.Add(item);
					}

					this.trayContextMenu.Items.Add(submenu);
				}

				this.trayContextMenu.Items.Add("-");
			}

			this.trayContextMenu.Items.Add("E&xit");
			this.lstMungeScripts.Sorted = true;
			this.txtMungeScriptDescription.Text = "(select a script from the list)";
			this.lstMungeScripts.SelectedIndex = -1;
			return true;
		}

		private void LoadAndCompileScript()
		{
			string fullfilename = Path.Combine(ScriptPath, "cm_" + this.lstMungeScripts.Items[this.lstMungeScripts.SelectedIndex].ToString()) + ".xml";
			_csmc = new ClipboardMungerScriptContainer();

			try
			{
				_csmc = ObjectXMLSerializer<ClipboardMungerScriptContainer>.LoadDocumentFormat(fullfilename);
			}
			catch (Exception e1)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref e1, string.Format("The following error occurred attempting to load from file {0}", fullfilename), string.Empty), "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.txtScript.Text = _csmc.Script.Replace("\n", "\r\n");
			LoadGacList();

			if (_csmc.Language == (int) Scripting.Languages.CSharp)
			{
				SetHighlighting("C#");
				this.rbCSharp.Checked = true;
			}

			if (_csmc.Language == (int) Scripting.Languages.VB)
			{
				SetHighlighting("VBNET");
				this.rbVB.Checked = true;
			}
			this.txtTestSource.Text = _csmc.TestData.Replace("\n", "\r\n");
			this.txtStoreTitle.Text = _csmc.Title;
			this.txtStoreDescription.Text = _csmc.Description.Replace("\n", "\r\n");
			this.txtMungeScriptDescription.Text = _csmc.Description.Replace("\n", "\r\n");

			this.lvwOtherReference.Items.Clear();
			if (_csmc.OtherReferences != null)
			{
				foreach (string thisfile in _csmc.OtherReferences)
				{
					ListViewItem l = new ListViewItem();
					l.Tag = (object) thisfile;
					l.Checked = false;
					if (File.Exists(thisfile))
					{
						try
						{
							BOG.Framework.AssemblyVersion a = new AssemblyVersion(thisfile);
							l.Text = a.DisplayString();
							l.SubItems.Add(Path.GetDirectoryName(thisfile));
							l.Tag = (object) thisfile;
							l.Checked = true;
						}
						catch (Exception err)
						{
							MessageBox.Show(DetailedException.WithUserContent(ref err, string.Format("File {0}", thisfile), string.Empty), "Not a valid assembly", MessageBoxButtons.OK, MessageBoxIcon.Error);
							l.Text = string.Format("(invalid assembly) {0}", thisfile);
						}
					}
					else
					{
						MessageBox.Show(string.Format("Assembly file: {0}\r\n( Was this script possibly created on another workstation? )", thisfile), "Missing assembly", MessageBoxButtons.OK, MessageBoxIcon.Error);
						l.Text = string.Format("(missing assembly) {0}", thisfile);
					}
					lvwOtherReference.Items.Add(l);
				}
			}
			_compiledScript = null;
		}

		private bool ExecuteMungeScript(string source, out string mungedOutput, out string mungedMessage)
		{
			bool Result = false;
			mungedOutput = string.Empty;
			mungedMessage = string.Empty;

			if (_compiledScript == null)
			{
				CompileScript(true);
			}
			if (_compiledScript != null)
			{
				try
				{
					mungedOutput = _compiledScript.Munge(source);
					mungedMessage = "Successful";
					Result = true;
				}
				catch (Exception ex)
				{
					mungedMessage = DetailedException.WithUserContent(ref ex);
				}
			}
			else
			{
				mungedMessage = "The current script would not successfully compile.";
			}
			return Result;
		}

		#region Form Events

		private void MainForm_Load(object sender, EventArgs e)
		{
			LoadGacList();

			// Get default script source from embedded text file
			System.IO.Stream s;
			byte[] b;

			s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ClipboardMunger.Stub_Csharp.txt");
			if (s != null)
			{
				b = new byte[Convert.ToInt32(s.Length)];
				s.Read(b, 0, Convert.ToInt32(s.Length));
				this.txtScript.Text = System.Text.ASCIIEncoding.ASCII.GetString(b);
			}

			ScriptPath = (string) _AppSettings.GetSetting("ClipboardMunger.ScriptFolder", (object) ".");

			if (!LoadScriptList())
			{
				MessageBox.Show("The script path was not found", "Error with path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void MainForm_Activated(object sender, EventArgs e)
		{
			this.txtClipboardContents.Text = Clipboard.GetText();
			this.lblClipboardByteCount.Text = string.Format("{0:#,#} byte(s) currently on the clipboard", this.txtClipboardContents.Text.Length);
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState)
				Hide();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Terminating)
				return;

			bool SubjectToCancellation = true;
			if (!ContentsChanged)
			{
				SubjectToCancellation = false;
			}
			if (SubjectToCancellation && e.CloseReason != CloseReason.UserClosing)
			{
				SubjectToCancellation = false;
			}
			if (SubjectToCancellation && MessageBox.Show("The current script has not been saved.  If you continue, you will lose any changes.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
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

		private void lvwErrors_ItemActivate(object sender, EventArgs e)
		{
			int lineNumber = Convert.ToInt32(lvwErrors.SelectedItems[0].SubItems[1].Text);

			if (lineNumber != 0)
			{
				TextLocation startOfLine = new TextLocation(0, lineNumber - 1);
				txtScript.ActiveTextAreaControl.SelectionManager.SetSelection(startOfLine, new TextLocation(0, lineNumber));
				txtScript.ActiveTextAreaControl.Caret.Position = startOfLine;
			}
			txtScript.Focus();
		}

		private void lvwGacReference_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (AssemblyListRebuilding || !e.Item.Checked)
				return;
			// when the item is checked here, remove it from Reference, add it to List

			AssemblyListRebuilding = true;
			GacReferenceChange(false, e.Item);
			AssemblyListRebuilding = false;
		}

		private void lvwGacList_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (AssemblyListRebuilding || !e.Item.Checked)
				return;
			// when the item is checked here, remove it from Reference, add it to List

			AssemblyListRebuilding = true;
			GacReferenceChange(true, e.Item);
			AssemblyListRebuilding = false;
		}

		private void btnTestCode_Click(object sender, EventArgs e)
		{
			string ResultingOutput = string.Empty;
			string Message = string.Empty;
			if (ExecuteMungeScript(this.txtTestSource.Text, out ResultingOutput, out Message))
			{
				this.txtTestResult.Text = ResultingOutput;
			}
			else
			{
				this.txtTestResult.Text = Message;
				MessageBox.Show("See test result text box for details", "Script error encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnStore_Click(object sender, EventArgs e)
		{
			if (this.txtStoreTitle.Text.Trim().Length == 0)
			{
				MessageBox.Show("The title is part of the file nameand can not be blank", "Can not save script", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string thisfilename = "cm_" + this.txtStoreTitle.Text.Trim() + ".xml";
			string fullfilename = Path.Combine(ScriptPath, thisfilename);
			if (thisfilename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				MessageBox.Show("The title contains one or more invalid filename characters", "Can not save script", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (File.Exists(fullfilename))
			{
				if (MessageBox.Show("Overwrite existing file " + fullfilename, "Warning", MessageBoxButtons.OK | MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
				{
					return;
				}
			}
			ClipboardMungerScriptContainer c = new ClipboardMungerScriptContainer();

			c.Script = this.txtScript.Text;
			c.Language = (this.rbVB.Checked ? (int) Scripting.Languages.VB : (int) Scripting.Languages.CSharp);
			c.TestData = this.txtTestSource.Text;
			c.Title = this.txtStoreTitle.Text;
			c.Description = this.txtStoreDescription.Text;
			c.References.Clear();
			for (int i = 0; i < this.lvwGacReference.Items.Count; i++)
			{
				c.References.Add(this.lvwGacReference.Items[i].Text);
			}

			c.OtherReferences.Clear();
			for (int i = 0; i < this.lvwOtherReference.Items.Count; i++)
				if (this.lvwOtherReference.Items[i].Checked)
					c.OtherReferences.Add((string) this.lvwOtherReference.Items[i].Tag);

			try
			{
				ObjectXMLSerializer<ClipboardMungerScriptContainer>.SaveDocumentFormat(c, fullfilename);
				MessageBox.Show("File successfully saved", "Sucessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ContentsChanged = false;
			}
			catch (Exception e1)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref e1, "Unable to save the script...", string.Empty), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!LoadScriptList())
			{
				MessageBox.Show("The script path was not found", "Error with path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void lstMungeScripts_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CurrentScriptSelectedIndex == this.lstMungeScripts.SelectedIndex)
				return;

			if (!ContentsChanged || MessageBox.Show("The current script has not been saved.  If you continue, you will lose any changes.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				if (this.lstMungeScripts.SelectedIndex >= 0)
				{
					LoadAndCompileScript();
				}
				else
				{
					this.txtScript.Text = string.Empty;
					this.txtTestSource.Text = string.Empty;
					this.txtStoreTitle.Text = string.Empty;
					this.txtStoreDescription.Text = string.Empty;
					this.txtMungeScriptDescription.Text = string.Empty;

					_compiledScript = null;
				}
				CurrentScriptSelectedIndex = this.lstMungeScripts.SelectedIndex;
				ContentsChanged = false;
			}
			else
			{
				this.lstMungeScripts.SelectedIndex = CurrentScriptSelectedIndex;
				return;
			}
		}

		private void ScriptMenuItemClicked(object sender, EventArgs e)
		{
			if (!ContentsChanged || MessageBox.Show("The current script has not been saved.  If you continue, you will lose any changes.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				string clickedItem = ((ToolStripMenuItem) sender).Text;
				for (int Index = 0; Index < this.lstMungeScripts.Items.Count; Index++)
				{
					if (this.lstMungeScripts.Items[Index].ToString() == clickedItem)
					{
						ContentsChanged = false;
						ContextMenuScriptSelection = Index;
						timerScriptLaunch.Interval = 10;
						timerScriptLaunch.Enabled = true;
						timerScriptLaunch.Start();
						break;
					}
				}
			}
		}

		private void btnScriptFolder_Click(object sender, EventArgs e)
		{
			if (!ContentsChanged || MessageBox.Show("The current script has not been saved.  If you continue, you will lose any changes.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				dlgFolder.ShowNewFolderButton = true;
				dlgFolder.Description = "Select folder containing scripts";
				dlgFolder.RootFolder = Environment.SpecialFolder.Desktop;
				dlgFolder.SelectedPath = ScriptPath;
				switch (dlgFolder.ShowDialog())
				{
					case DialogResult.OK:
						if (ScriptPath != dlgFolder.SelectedPath)
						{
							string OldPath = ScriptPath;
							ScriptPath = dlgFolder.SelectedPath;
							if (!LoadScriptList())
							{
								MessageBox.Show("Error enumerating scripts in the selected path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								ScriptPath = OldPath;
							}
							else
							{
								_AppSettings.SetSetting("ClipboardMunger.ScriptFolder", ScriptPath);
								_AppSettings.ConfigurationFile = _ConfigurationFile;
								_AppSettings.SaveSettings();
							}
						}
						break;

					case DialogResult.Cancel:
						break;
				}
			}
		}

		private bool CompileScript(bool force)
		{
			bool WasSuccessful = true;
			if (_compiledScript != null && !force)
				return WasSuccessful;

			CompilerResults results;
			List<string> reference = new List<string>();

			lvwErrors.Items.Clear();
			lvwErrors.Refresh();
			ListViewItem l;

			l = new ListViewItem("Compiling...");
			l.SubItems.Add("busy");
			lvwErrors.Items.Add(l);
			lvwErrors.Refresh();

			Cursor = Cursors.WaitCursor;

			// Find reference to the interface for munging
			reference.Add(System.IO.Path.Combine(
				System.IO.Path.GetDirectoryName(Application.ExecutablePath),
				"ClipboardMunger.exe"));

			// add other GAC references as selected by the user.
			foreach (ListViewItem i in lvwGacReference.Items)
			{
				reference.Add(i.SubItems[1].Text + ".dll");
			}

			// add other external references as selected by the user.
			foreach (ListViewItem i in lvwOtherReference.Items)
			{
				if (i.Checked)
				{
					reference.Add((string) i.Tag);
				}
			}

			// Compile script
			lvwErrors.Items.Clear();
			results = Scripting.CompileScript(this.txtScript.Document.TextContent, reference.ToArray(), (this.rbVB.Checked ? Scripting.Languages.VB : Scripting.Languages.CSharp));

			if (results.Errors.Count == 0)
			{
				_compiledScript = (Interfaces.IClipboard) Scripting.FindInterface(results.CompiledAssembly, "IClipboard");
				l = new ListViewItem("Compile successful");
				l.BackColor = Color.White;
				l.ForeColor = Color.Green;
				l.SubItems.Add("0");
				lvwErrors.Items.Add(l);
			}
			else
			{
				WasSuccessful = false;
				_compiledScript = null;
				// Add each error as a listview item with its line number
				foreach (CompilerError err in results.Errors)
				{
					l = new ListViewItem(err.ErrorText);
					l.BackColor = Color.White;
					l.ForeColor = Color.Red;
					l.SubItems.Add(err.Line.ToString());
					lvwErrors.Items.Add(l);
				}
			}

			Cursor = Cursors.Default;
			return WasSuccessful;
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void btnExecuteScript_Click(object sender, EventArgs e)
		{
			if (!CompileScript(true))
			{
				this.tabDevelop.Focus();
				this.tabCode.Focus();
				return;
			}
			string ResultingOutput = string.Empty;
			string Message = string.Empty;
			if (ExecuteMungeScript(Clipboard.GetText(), out ResultingOutput, out Message))
			{
				Clipboard.SetText(ResultingOutput);
				MessageBox.Show("Results posted to clipboard", "Script OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show(Message, "Script error encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void trayContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "&Restore GUI")
			{
				Show();
				WindowState = FormWindowState.Normal;
				return;
			}
			else if (e.ClickedItem.Text == "E&xit")
			{
				if (!ContentsChanged || MessageBox.Show("The current script has not been saved.  If you continue, you will lose any changes.\r\nContinue anyway?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					this.Close();
				}
				return;
			}
		}

		private void timerScriptLaunch_Tick(object sender, EventArgs e)
		{
			timerScriptLaunch.Stop();
			timerScriptLaunch.Enabled = false;
			this.lstMungeScripts.SelectedIndex = ContextMenuScriptSelection;
			CompileScript(true);
			string ResultingOutput = string.Empty;
			string Message = string.Empty;
			if (ExecuteMungeScript(Clipboard.GetText(), out ResultingOutput, out Message))
			{
				Clipboard.SetText(ResultingOutput);
				MessageBox.Show("Results posted to clipboard", "Script OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show(Message, "Script error encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void txtScript_TextChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
		}

		private void rbVB_CheckedChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
			SetHighlighting("VBNET");
		}

		private void rbCSharp_CheckedChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
			SetHighlighting("C#");
		}

		private void txtTestSource_TextChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
		}

		private void txtStoreTitle_TextChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
		}

		private void txtStoreDescription_TextChanged(object sender, EventArgs e)
		{
			if (!ContentsChanged)
			{
				ContentsChanged = true;
			}
		}

		private void btnAddReference_Click(object sender, EventArgs e)
		{
			dlgFile.CheckFileExists = true;
			dlgFile.DefaultExt = "dll";
			dlgFile.FileName = "";
			dlgFile.Filter = "Assemblies (*.dll)|*.dll";
			dlgFile.InitialDirectory = ".";
			dlgFile.Multiselect = true;
			dlgFile.ShowReadOnly = false;
			dlgFile.Title = "Select one or more assemblies to include";
			if (dlgFile.ShowDialog() == DialogResult.OK)
			{
				foreach (string thisfile in dlgFile.FileNames)
				{
					BOG.Framework.AssemblyVersion a;
					try
					{
						a = new AssemblyVersion(thisfile);
					}
					catch (Exception err)
					{
						MessageBox.Show(DetailedException.WithUserContent(ref err, string.Format("File {0}", thisfile), string.Empty), "Not a valid assembly", MessageBoxButtons.OK, MessageBoxIcon.Error);
						continue;
					}
					for (int Index = 0; Index < lvwOtherReference.Items.Count; Index++)
					{
						if ((string) lvwOtherReference.Items[Index].Tag == a.FullPath)
						{
							MessageBox.Show("Duplicate", string.Format("The file {0} is already in the manifest", thisfile), MessageBoxButtons.OK, MessageBoxIcon.Error);
							continue;
						}
					}

					ListViewItem l = new ListViewItem();
					l.Text = a.DisplayString();
					l.Tag = (object) a.FullPath;
					l.Checked = true;
					lvwOtherReference.Items.Add(l);
				}
			}
		}

		private void btnCompile_Click(object sender, EventArgs e)
		{
			CompileScript(true);
		}

		#endregion

	}
}
