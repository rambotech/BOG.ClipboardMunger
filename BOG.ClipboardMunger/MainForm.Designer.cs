using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using BOG.Framework;
using System.Configuration;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace BOG.ClipboardMunger
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private StatusStrip statusStrip1;
		private TabControl tabsFunctions;
		private TabPage tabExecute;
		private Button btnExecuteScript;
		private Label lblClipboardByteCount;
		private TabPage tabTest;
		private TabPage tabStore;
		private Button btnTestCode;
		private TabPage tabDevelop;
		private ToolStripStatusLabel toolStripStatusLabel1;
		private TabControl tabDevNav;
		private TabPage tabCode;
		private TabPage tabGacReference;
		private CheckBox checkBox1;
		private RadioButton radioButton1;
		private RadioButton radioButton2;
		internal Button button2;
		internal ListView listView1;
		internal ColumnHeader columnHeader3;
		internal ColumnHeader columnHeader4;
		private Button btnStore;
		private TextBox txtStoreTitle;
		private TextBox txtStoreDescription;
		private Label lblDescription;
		private Label lblTitle;

		private Button btnScriptFolder;
		private FolderBrowserDialog dlgFolder;
		private Label lblScriptsSourceFolder;
		private TabPage tabClipboardContent;
		private CheckBox chkWorkWrap;
		private NotifyIcon notifyIcon1;
		private ContextMenuStrip trayContextMenu;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripSeparator toolStripMenuItem2;
		private Timer timerScriptLaunch;

		private Timer timerShutdown;
		private SplitContainer splitContainer1;
		private ListBox lstMungeScripts;
		private TextBox txtMungeScriptDescription;
		private SplitContainer splitContainer2;
		private CheckBox chkWordWrap;
		private RadioButton rbCSharp;
		private RadioButton rbVB;
		internal Button btnCompile;
		internal ListView lvwErrors;
		internal ColumnHeader ColumnHeader1;
		internal ColumnHeader ColumnHeader2;
		private SplitContainer splitContainer3;
		private GroupBox gbxTestSource;
		private TextBox txtTestSource;
		private GroupBox gbxTestResult;
		private TextBox txtTestResult;
		private TabPage tabOtherReference;
		private Button btnAddReference;
		private ListView lvwOtherReference;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private OpenFileDialog dlgFile;
		private System.ServiceProcess.ServiceController serviceController1;
		private TextBox txtClipboardContents;
		private ICSharpCode.TextEditor.TextEditorControl txtScript;
		private SplitContainer splitContainer4;
		private GroupBox gbxGacIncluded;
		private GroupBox gbxGacNotUsed;
		private ListView lvwGacReference;
		private ColumnHeader colRefName;
		private ColumnHeader colRefAssembly;
		private ListView lvwGacList;
		private ColumnHeader columnHeader7;
		private ColumnHeader columnHeader8;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabsFunctions = new System.Windows.Forms.TabControl();
			this.tabExecute = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstMungeScripts = new System.Windows.Forms.ListBox();
			this.txtMungeScriptDescription = new System.Windows.Forms.TextBox();
			this.lblScriptsSourceFolder = new System.Windows.Forms.Label();
			this.btnScriptFolder = new System.Windows.Forms.Button();
			this.lblClipboardByteCount = new System.Windows.Forms.Label();
			this.btnExecuteScript = new System.Windows.Forms.Button();
			this.tabClipboardContent = new System.Windows.Forms.TabPage();
			this.chkWorkWrap = new System.Windows.Forms.CheckBox();
			this.txtClipboardContents = new System.Windows.Forms.TextBox();
			this.tabDevelop = new System.Windows.Forms.TabPage();
			this.tabDevNav = new System.Windows.Forms.TabControl();
			this.tabCode = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.txtScript = new ICSharpCode.TextEditor.TextEditorControl();
			this.chkWordWrap = new System.Windows.Forms.CheckBox();
			this.rbCSharp = new System.Windows.Forms.RadioButton();
			this.rbVB = new System.Windows.Forms.RadioButton();
			this.btnCompile = new System.Windows.Forms.Button();
			this.lvwErrors = new System.Windows.Forms.ListView();
			this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabGacReference = new System.Windows.Forms.TabPage();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.gbxGacIncluded = new System.Windows.Forms.GroupBox();
			this.lvwGacReference = new System.Windows.Forms.ListView();
			this.colRefName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colRefAssembly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gbxGacNotUsed = new System.Windows.Forms.GroupBox();
			this.lvwGacList = new System.Windows.Forms.ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabOtherReference = new System.Windows.Forms.TabPage();
			this.btnAddReference = new System.Windows.Forms.Button();
			this.lvwOtherReference = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabTest = new System.Windows.Forms.TabPage();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.gbxTestSource = new System.Windows.Forms.GroupBox();
			this.txtTestSource = new System.Windows.Forms.TextBox();
			this.gbxTestResult = new System.Windows.Forms.GroupBox();
			this.txtTestResult = new System.Windows.Forms.TextBox();
			this.btnTestCode = new System.Windows.Forms.Button();
			this.tabStore = new System.Windows.Forms.TabPage();
			this.btnStore = new System.Windows.Forms.Button();
			this.txtStoreTitle = new System.Windows.Forms.TextBox();
			this.txtStoreDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.button2 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.timerScriptLaunch = new System.Windows.Forms.Timer(this.components);
			this.timerShutdown = new System.Windows.Forms.Timer(this.components);
			this.dlgFile = new System.Windows.Forms.OpenFileDialog();
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			this.statusStrip1.SuspendLayout();
			this.tabsFunctions.SuspendLayout();
			this.tabExecute.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabClipboardContent.SuspendLayout();
			this.tabDevelop.SuspendLayout();
			this.tabDevNav.SuspendLayout();
			this.tabCode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tabGacReference.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.gbxGacIncluded.SuspendLayout();
			this.gbxGacNotUsed.SuspendLayout();
			this.tabOtherReference.SuspendLayout();
			this.tabTest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.gbxTestSource.SuspendLayout();
			this.gbxTestResult.SuspendLayout();
			this.tabStore.SuspendLayout();
			this.trayContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 406);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(661, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(159, 17);
			this.toolStripStatusLabel1.Text = "Clipboard Munger Utility, 2.0";
			// 
			// tabsFunctions
			// 
			this.tabsFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsFunctions.Controls.Add(this.tabExecute);
			this.tabsFunctions.Controls.Add(this.tabClipboardContent);
			this.tabsFunctions.Controls.Add(this.tabDevelop);
			this.tabsFunctions.Controls.Add(this.tabTest);
			this.tabsFunctions.Controls.Add(this.tabStore);
			this.tabsFunctions.Location = new System.Drawing.Point(0, 3);
			this.tabsFunctions.Name = "tabsFunctions";
			this.tabsFunctions.SelectedIndex = 0;
			this.tabsFunctions.Size = new System.Drawing.Size(661, 400);
			this.tabsFunctions.TabIndex = 1;
			// 
			// tabExecute
			// 
			this.tabExecute.Controls.Add(this.splitContainer1);
			this.tabExecute.Controls.Add(this.lblScriptsSourceFolder);
			this.tabExecute.Controls.Add(this.btnScriptFolder);
			this.tabExecute.Controls.Add(this.lblClipboardByteCount);
			this.tabExecute.Controls.Add(this.btnExecuteScript);
			this.tabExecute.Location = new System.Drawing.Point(4, 22);
			this.tabExecute.Name = "tabExecute";
			this.tabExecute.Padding = new System.Windows.Forms.Padding(3);
			this.tabExecute.Size = new System.Drawing.Size(653, 374);
			this.tabExecute.TabIndex = 0;
			this.tabExecute.Text = "Execute";
			this.tabExecute.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(3, 7);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstMungeScripts);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtMungeScriptDescription);
			this.splitContainer1.Size = new System.Drawing.Size(644, 305);
			this.splitContainer1.SplitterDistance = 214;
			this.splitContainer1.TabIndex = 7;
			// 
			// lstMungeScripts
			// 
			this.lstMungeScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMungeScripts.FormattingEnabled = true;
			this.lstMungeScripts.Location = new System.Drawing.Point(3, 3);
			this.lstMungeScripts.Name = "lstMungeScripts";
			this.lstMungeScripts.Size = new System.Drawing.Size(208, 290);
			this.lstMungeScripts.TabIndex = 1;
			this.lstMungeScripts.SelectedIndexChanged += new System.EventHandler(this.lstMungeScripts_SelectedIndexChanged);
			// 
			// txtMungeScriptDescription
			// 
			this.txtMungeScriptDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMungeScriptDescription.Location = new System.Drawing.Point(3, 3);
			this.txtMungeScriptDescription.Multiline = true;
			this.txtMungeScriptDescription.Name = "txtMungeScriptDescription";
			this.txtMungeScriptDescription.ReadOnly = true;
			this.txtMungeScriptDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMungeScriptDescription.Size = new System.Drawing.Size(420, 290);
			this.txtMungeScriptDescription.TabIndex = 2;
			// 
			// lblScriptsSourceFolder
			// 
			this.lblScriptsSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblScriptsSourceFolder.AutoSize = true;
			this.lblScriptsSourceFolder.Location = new System.Drawing.Point(38, 344);
			this.lblScriptsSourceFolder.Name = "lblScriptsSourceFolder";
			this.lblScriptsSourceFolder.Size = new System.Drawing.Size(16, 13);
			this.lblScriptsSourceFolder.TabIndex = 6;
			this.lblScriptsSourceFolder.Text = "...";
			// 
			// btnScriptFolder
			// 
			this.btnScriptFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnScriptFolder.Location = new System.Drawing.Point(41, 318);
			this.btnScriptFolder.Name = "btnScriptFolder";
			this.btnScriptFolder.Size = new System.Drawing.Size(184, 23);
			this.btnScriptFolder.TabIndex = 5;
			this.btnScriptFolder.Text = "Change Scripts &Folder";
			this.btnScriptFolder.UseVisualStyleBackColor = true;
			this.btnScriptFolder.Click += new System.EventHandler(this.btnScriptFolder_Click);
			// 
			// lblClipboardByteCount
			// 
			this.lblClipboardByteCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblClipboardByteCount.AutoSize = true;
			this.lblClipboardByteCount.Location = new System.Drawing.Point(408, 323);
			this.lblClipboardByteCount.Name = "lblClipboardByteCount";
			this.lblClipboardByteCount.Size = new System.Drawing.Size(164, 13);
			this.lblClipboardByteCount.TabIndex = 4;
			this.lblClipboardByteCount.Text = "X bytes currently on the clipboard";
			// 
			// btnExecuteScript
			// 
			this.btnExecuteScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExecuteScript.Location = new System.Drawing.Point(267, 318);
			this.btnExecuteScript.Name = "btnExecuteScript";
			this.btnExecuteScript.Size = new System.Drawing.Size(122, 23);
			this.btnExecuteScript.TabIndex = 3;
			this.btnExecuteScript.Text = "Run Script";
			this.btnExecuteScript.UseVisualStyleBackColor = true;
			this.btnExecuteScript.Click += new System.EventHandler(this.btnExecuteScript_Click);
			// 
			// tabClipboardContent
			// 
			this.tabClipboardContent.Controls.Add(this.chkWorkWrap);
			this.tabClipboardContent.Controls.Add(this.txtClipboardContents);
			this.tabClipboardContent.Location = new System.Drawing.Point(4, 22);
			this.tabClipboardContent.Name = "tabClipboardContent";
			this.tabClipboardContent.Size = new System.Drawing.Size(653, 374);
			this.tabClipboardContent.TabIndex = 4;
			this.tabClipboardContent.Text = "Clipboard Content";
			this.tabClipboardContent.UseVisualStyleBackColor = true;
			// 
			// chkWorkWrap
			// 
			this.chkWorkWrap.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.chkWorkWrap.AutoSize = true;
			this.chkWorkWrap.Location = new System.Drawing.Point(300, 336);
			this.chkWorkWrap.Name = "chkWorkWrap";
			this.chkWorkWrap.Size = new System.Drawing.Size(81, 17);
			this.chkWorkWrap.TabIndex = 1;
			this.chkWorkWrap.Text = "Word Wrap";
			this.chkWorkWrap.UseVisualStyleBackColor = true;
			// 
			// txtClipboardContents
			// 
			this.txtClipboardContents.AcceptsReturn = true;
			this.txtClipboardContents.AcceptsTab = true;
			this.txtClipboardContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtClipboardContents.Location = new System.Drawing.Point(3, 3);
			this.txtClipboardContents.Multiline = true;
			this.txtClipboardContents.Name = "txtClipboardContents";
			this.txtClipboardContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtClipboardContents.Size = new System.Drawing.Size(647, 327);
			this.txtClipboardContents.TabIndex = 0;
			this.txtClipboardContents.TabStop = false;
			this.txtClipboardContents.WordWrap = false;
			// 
			// tabDevelop
			// 
			this.tabDevelop.Controls.Add(this.tabDevNav);
			this.tabDevelop.Location = new System.Drawing.Point(4, 22);
			this.tabDevelop.Name = "tabDevelop";
			this.tabDevelop.Padding = new System.Windows.Forms.Padding(3);
			this.tabDevelop.Size = new System.Drawing.Size(653, 374);
			this.tabDevelop.TabIndex = 1;
			this.tabDevelop.Text = "Develop";
			this.tabDevelop.UseVisualStyleBackColor = true;
			// 
			// tabDevNav
			// 
			this.tabDevNav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabDevNav.Controls.Add(this.tabCode);
			this.tabDevNav.Controls.Add(this.tabGacReference);
			this.tabDevNav.Controls.Add(this.tabOtherReference);
			this.tabDevNav.Location = new System.Drawing.Point(3, 3);
			this.tabDevNav.Name = "tabDevNav";
			this.tabDevNav.SelectedIndex = 0;
			this.tabDevNav.Size = new System.Drawing.Size(647, 371);
			this.tabDevNav.TabIndex = 0;
			// 
			// tabCode
			// 
			this.tabCode.Controls.Add(this.splitContainer2);
			this.tabCode.Location = new System.Drawing.Point(4, 22);
			this.tabCode.Name = "tabCode";
			this.tabCode.Padding = new System.Windows.Forms.Padding(3);
			this.tabCode.Size = new System.Drawing.Size(639, 345);
			this.tabCode.TabIndex = 0;
			this.tabCode.Text = "Code";
			this.tabCode.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer2.Location = new System.Drawing.Point(3, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.txtScript);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.chkWordWrap);
			this.splitContainer2.Panel2.Controls.Add(this.rbCSharp);
			this.splitContainer2.Panel2.Controls.Add(this.rbVB);
			this.splitContainer2.Panel2.Controls.Add(this.btnCompile);
			this.splitContainer2.Panel2.Controls.Add(this.lvwErrors);
			this.splitContainer2.Size = new System.Drawing.Size(633, 343);
			this.splitContainer2.SplitterDistance = 227;
			this.splitContainer2.TabIndex = 26;
			// 
			// txtScript
			// 
			this.txtScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtScript.IsReadOnly = false;
			this.txtScript.Location = new System.Drawing.Point(4, 4);
			this.txtScript.Name = "txtScript";
			this.txtScript.Size = new System.Drawing.Size(626, 220);
			this.txtScript.TabIndex = 0;
			this.txtScript.Text = "// template (CSharp)\r\n\r\nusing System;\r\n\r\npublic class Script : Interfaces.IClipbo" +
    "ard\r\n{\t\r\n\tpublic string Munge (string clipboardSource)\r\n\t{\r\n\t\treturn clipboardSo" +
    "urce;\r\n\t}\r\n}\t";
			// 
			// chkWordWrap
			// 
			this.chkWordWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkWordWrap.AutoSize = true;
			this.chkWordWrap.Location = new System.Drawing.Point(529, 10);
			this.chkWordWrap.Name = "chkWordWrap";
			this.chkWordWrap.Size = new System.Drawing.Size(81, 17);
			this.chkWordWrap.TabIndex = 29;
			this.chkWordWrap.Text = "&Word Wrap";
			this.chkWordWrap.UseVisualStyleBackColor = true;
			// 
			// rbCSharp
			// 
			this.rbCSharp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.rbCSharp.AutoSize = true;
			this.rbCSharp.Checked = true;
			this.rbCSharp.Location = new System.Drawing.Point(529, 54);
			this.rbCSharp.Name = "rbCSharp";
			this.rbCSharp.Size = new System.Drawing.Size(60, 17);
			this.rbCSharp.TabIndex = 28;
			this.rbCSharp.TabStop = true;
			this.rbCSharp.Text = "C&Sharp";
			this.rbCSharp.UseVisualStyleBackColor = true;
			this.rbCSharp.CheckedChanged += new System.EventHandler(this.rbCSharp_CheckedChanged);
			// 
			// rbVB
			// 
			this.rbVB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.rbVB.AutoSize = true;
			this.rbVB.Location = new System.Drawing.Point(529, 33);
			this.rbVB.Name = "rbVB";
			this.rbVB.Size = new System.Drawing.Size(39, 17);
			this.rbVB.TabIndex = 27;
			this.rbVB.Text = "&VB";
			this.rbVB.UseVisualStyleBackColor = true;
			this.rbVB.CheckedChanged += new System.EventHandler(this.rbVB_CheckedChanged);
			// 
			// btnCompile
			// 
			this.btnCompile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCompile.Location = new System.Drawing.Point(520, 77);
			this.btnCompile.Name = "btnCompile";
			this.btnCompile.Size = new System.Drawing.Size(105, 25);
			this.btnCompile.TabIndex = 26;
			this.btnCompile.Text = "Co&mpile";
			this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
			// 
			// lvwErrors
			// 
			this.lvwErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
			this.lvwErrors.FullRowSelect = true;
			this.lvwErrors.GridLines = true;
			this.lvwErrors.Location = new System.Drawing.Point(4, 3);
			this.lvwErrors.MultiSelect = false;
			this.lvwErrors.Name = "lvwErrors";
			this.lvwErrors.Size = new System.Drawing.Size(486, 106);
			this.lvwErrors.TabIndex = 22;
			this.lvwErrors.UseCompatibleStateImageBehavior = false;
			this.lvwErrors.View = System.Windows.Forms.View.Details;
			this.lvwErrors.ItemActivate += new System.EventHandler(this.lvwErrors_ItemActivate);
			// 
			// ColumnHeader1
			// 
			this.ColumnHeader1.Text = "Error";
			this.ColumnHeader1.Width = 397;
			// 
			// ColumnHeader2
			// 
			this.ColumnHeader2.Text = "Line";
			// 
			// tabGacReference
			// 
			this.tabGacReference.Controls.Add(this.splitContainer4);
			this.tabGacReference.Location = new System.Drawing.Point(4, 22);
			this.tabGacReference.Name = "tabGacReference";
			this.tabGacReference.Padding = new System.Windows.Forms.Padding(3);
			this.tabGacReference.Size = new System.Drawing.Size(639, 345);
			this.tabGacReference.TabIndex = 1;
			this.tabGacReference.Text = "GAC References";
			this.tabGacReference.UseVisualStyleBackColor = true;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer4.Location = new System.Drawing.Point(7, 7);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.gbxGacIncluded);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.gbxGacNotUsed);
			this.splitContainer4.Size = new System.Drawing.Size(626, 332);
			this.splitContainer4.SplitterDistance = 166;
			this.splitContainer4.TabIndex = 1;
			// 
			// gbxGacIncluded
			// 
			this.gbxGacIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxGacIncluded.Controls.Add(this.lvwGacReference);
			this.gbxGacIncluded.Location = new System.Drawing.Point(4, 4);
			this.gbxGacIncluded.Name = "gbxGacIncluded";
			this.gbxGacIncluded.Size = new System.Drawing.Size(619, 159);
			this.gbxGacIncluded.TabIndex = 0;
			this.gbxGacIncluded.TabStop = false;
			this.gbxGacIncluded.Text = "Include ...";
			// 
			// lvwGacReference
			// 
			this.lvwGacReference.AllowColumnReorder = true;
			this.lvwGacReference.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwGacReference.CheckBoxes = true;
			this.lvwGacReference.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRefName,
            this.colRefAssembly});
			this.lvwGacReference.GridLines = true;
			this.lvwGacReference.Location = new System.Drawing.Point(6, 19);
			this.lvwGacReference.Name = "lvwGacReference";
			this.lvwGacReference.Size = new System.Drawing.Size(607, 134);
			this.lvwGacReference.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwGacReference.TabIndex = 2;
			this.lvwGacReference.UseCompatibleStateImageBehavior = false;
			this.lvwGacReference.View = System.Windows.Forms.View.Details;
			this.lvwGacReference.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwGacReference_ItemChecked);
			// 
			// colRefName
			// 
			this.colRefName.Text = "Name";
			this.colRefName.Width = 185;
			// 
			// colRefAssembly
			// 
			this.colRefAssembly.Text = "Namespace";
			this.colRefAssembly.Width = 119;
			// 
			// gbxGacNotUsed
			// 
			this.gbxGacNotUsed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxGacNotUsed.Controls.Add(this.lvwGacList);
			this.gbxGacNotUsed.Location = new System.Drawing.Point(4, 2);
			this.gbxGacNotUsed.Name = "gbxGacNotUsed";
			this.gbxGacNotUsed.Size = new System.Drawing.Size(619, 159);
			this.gbxGacNotUsed.TabIndex = 1;
			this.gbxGacNotUsed.TabStop = false;
			this.gbxGacNotUsed.Text = "Available ...";
			// 
			// lvwGacList
			// 
			this.lvwGacList.AllowColumnReorder = true;
			this.lvwGacList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwGacList.CheckBoxes = true;
			this.lvwGacList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
			this.lvwGacList.GridLines = true;
			this.lvwGacList.Location = new System.Drawing.Point(6, 14);
			this.lvwGacList.Name = "lvwGacList";
			this.lvwGacList.Size = new System.Drawing.Size(607, 139);
			this.lvwGacList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwGacList.TabIndex = 4;
			this.lvwGacList.UseCompatibleStateImageBehavior = false;
			this.lvwGacList.View = System.Windows.Forms.View.Details;
			this.lvwGacList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwGacList_ItemChecked);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Name";
			this.columnHeader7.Width = 185;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Namespace";
			this.columnHeader8.Width = 119;
			// 
			// tabOtherReference
			// 
			this.tabOtherReference.Controls.Add(this.btnAddReference);
			this.tabOtherReference.Controls.Add(this.lvwOtherReference);
			this.tabOtherReference.Location = new System.Drawing.Point(4, 22);
			this.tabOtherReference.Name = "tabOtherReference";
			this.tabOtherReference.Size = new System.Drawing.Size(639, 345);
			this.tabOtherReference.TabIndex = 2;
			this.tabOtherReference.Text = "Other Reference";
			this.tabOtherReference.UseVisualStyleBackColor = true;
			// 
			// btnAddReference
			// 
			this.btnAddReference.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnAddReference.Location = new System.Drawing.Point(300, 311);
			this.btnAddReference.Name = "btnAddReference";
			this.btnAddReference.Size = new System.Drawing.Size(75, 23);
			this.btnAddReference.TabIndex = 2;
			this.btnAddReference.Text = "&Add Reference";
			this.btnAddReference.UseVisualStyleBackColor = true;
			this.btnAddReference.Click += new System.EventHandler(this.btnAddReference_Click);
			// 
			// lvwOtherReference
			// 
			this.lvwOtherReference.AllowColumnReorder = true;
			this.lvwOtherReference.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwOtherReference.CheckBoxes = true;
			this.lvwOtherReference.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
			this.lvwOtherReference.GridLines = true;
			this.lvwOtherReference.Location = new System.Drawing.Point(6, 6);
			this.lvwOtherReference.Name = "lvwOtherReference";
			this.lvwOtherReference.Size = new System.Drawing.Size(626, 298);
			this.lvwOtherReference.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwOtherReference.TabIndex = 1;
			this.lvwOtherReference.UseCompatibleStateImageBehavior = false;
			this.lvwOtherReference.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Name";
			this.columnHeader5.Width = 185;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Path";
			this.columnHeader6.Width = 119;
			// 
			// tabTest
			// 
			this.tabTest.Controls.Add(this.splitContainer3);
			this.tabTest.Controls.Add(this.btnTestCode);
			this.tabTest.Location = new System.Drawing.Point(4, 22);
			this.tabTest.Name = "tabTest";
			this.tabTest.Size = new System.Drawing.Size(653, 374);
			this.tabTest.TabIndex = 2;
			this.tabTest.Text = "Test";
			this.tabTest.UseVisualStyleBackColor = true;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer3.Location = new System.Drawing.Point(3, 3);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.gbxTestSource);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.gbxTestResult);
			this.splitContainer3.Size = new System.Drawing.Size(647, 334);
			this.splitContainer3.SplitterDistance = 167;
			this.splitContainer3.TabIndex = 4;
			// 
			// gbxTestSource
			// 
			this.gbxTestSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxTestSource.Controls.Add(this.txtTestSource);
			this.gbxTestSource.Location = new System.Drawing.Point(5, 3);
			this.gbxTestSource.Name = "gbxTestSource";
			this.gbxTestSource.Size = new System.Drawing.Size(639, 161);
			this.gbxTestSource.TabIndex = 3;
			this.gbxTestSource.TabStop = false;
			this.gbxTestSource.Text = "Source";
			// 
			// txtTestSource
			// 
			this.txtTestSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTestSource.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTestSource.Location = new System.Drawing.Point(6, 19);
			this.txtTestSource.Multiline = true;
			this.txtTestSource.Name = "txtTestSource";
			this.txtTestSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTestSource.Size = new System.Drawing.Size(627, 134);
			this.txtTestSource.TabIndex = 1;
			this.txtTestSource.WordWrap = false;
			// 
			// gbxTestResult
			// 
			this.gbxTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbxTestResult.Controls.Add(this.txtTestResult);
			this.gbxTestResult.Location = new System.Drawing.Point(5, 3);
			this.gbxTestResult.Name = "gbxTestResult";
			this.gbxTestResult.Size = new System.Drawing.Size(637, 160);
			this.gbxTestResult.TabIndex = 2;
			this.gbxTestResult.TabStop = false;
			this.gbxTestResult.Text = "Result";
			// 
			// txtTestResult
			// 
			this.txtTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTestResult.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTestResult.Location = new System.Drawing.Point(6, 19);
			this.txtTestResult.Multiline = true;
			this.txtTestResult.Name = "txtTestResult";
			this.txtTestResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTestResult.Size = new System.Drawing.Size(625, 132);
			this.txtTestResult.TabIndex = 1;
			this.txtTestResult.WordWrap = false;
			// 
			// btnTestCode
			// 
			this.btnTestCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnTestCode.Location = new System.Drawing.Point(299, 343);
			this.btnTestCode.Name = "btnTestCode";
			this.btnTestCode.Size = new System.Drawing.Size(75, 23);
			this.btnTestCode.TabIndex = 3;
			this.btnTestCode.Text = "Run Test";
			this.btnTestCode.UseVisualStyleBackColor = true;
			this.btnTestCode.Click += new System.EventHandler(this.btnTestCode_Click);
			// 
			// tabStore
			// 
			this.tabStore.Controls.Add(this.btnStore);
			this.tabStore.Controls.Add(this.txtStoreTitle);
			this.tabStore.Controls.Add(this.txtStoreDescription);
			this.tabStore.Controls.Add(this.lblDescription);
			this.tabStore.Controls.Add(this.lblTitle);
			this.tabStore.Location = new System.Drawing.Point(4, 22);
			this.tabStore.Name = "tabStore";
			this.tabStore.Size = new System.Drawing.Size(653, 374);
			this.tabStore.TabIndex = 3;
			this.tabStore.Text = "Store / Update";
			this.tabStore.UseVisualStyleBackColor = true;
			// 
			// btnStore
			// 
			this.btnStore.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnStore.Location = new System.Drawing.Point(311, 331);
			this.btnStore.Name = "btnStore";
			this.btnStore.Size = new System.Drawing.Size(75, 23);
			this.btnStore.TabIndex = 4;
			this.btnStore.Text = "Save";
			this.btnStore.UseVisualStyleBackColor = true;
			this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
			// 
			// txtStoreTitle
			// 
			this.txtStoreTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStoreTitle.Location = new System.Drawing.Point(84, 11);
			this.txtStoreTitle.Name = "txtStoreTitle";
			this.txtStoreTitle.Size = new System.Drawing.Size(540, 20);
			this.txtStoreTitle.TabIndex = 3;
			this.txtStoreTitle.TextChanged += new System.EventHandler(this.txtStoreTitle_TextChanged);
			// 
			// txtStoreDescription
			// 
			this.txtStoreDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStoreDescription.Location = new System.Drawing.Point(84, 38);
			this.txtStoreDescription.Multiline = true;
			this.txtStoreDescription.Name = "txtStoreDescription";
			this.txtStoreDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtStoreDescription.Size = new System.Drawing.Size(540, 274);
			this.txtStoreDescription.TabIndex = 2;
			this.txtStoreDescription.Text = "(select a script from the list)";
			this.txtStoreDescription.TextChanged += new System.EventHandler(this.txtStoreDescription_TextChanged);
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(9, 41);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 1;
			this.lblDescription.Text = "Description";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(9, 14);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(27, 13);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Title";
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(540, 266);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(81, 17);
			this.checkBox1.TabIndex = 13;
			this.checkBox1.Text = "&Word Wrap";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(585, 318);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(60, 17);
			this.radioButton1.TabIndex = 12;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "C&Sharp";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(540, 318);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(39, 17);
			this.radioButton2.TabIndex = 11;
			this.radioButton2.Text = "&VB";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(540, 341);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(105, 25);
			this.button2.TabIndex = 10;
			this.button2.Text = "&Ok";
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(7, 261);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(524, 105);
			this.listView1.TabIndex = 5;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Error";
			this.columnHeader3.Width = 456;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Line";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.trayContextMenu;
			this.notifyIcon1.Text = "Clipboard Munger";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// trayContextMenu
			// 
			this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
			this.trayContextMenu.Name = "trayContextMenu";
			this.trayContextMenu.Size = new System.Drawing.Size(136, 32);
			this.trayContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.trayContextMenu_ItemClicked);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			this.toolStripMenuItem1.Text = "Restore GUI";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(132, 6);
			// 
			// timerScriptLaunch
			// 
			this.timerScriptLaunch.Tick += new System.EventHandler(this.timerScriptLaunch_Tick);
			// 
			// timerShutdown
			// 
			this.timerShutdown.Tick += new System.EventHandler(this.timerShutdown_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 428);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabsFunctions);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(669, 462);
			this.Name = "MainForm";
			this.Text = "Clipboard Munger";
			this.Activated += new System.EventHandler(this.MainForm_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabsFunctions.ResumeLayout(false);
			this.tabExecute.ResumeLayout(false);
			this.tabExecute.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabClipboardContent.ResumeLayout(false);
			this.tabClipboardContent.PerformLayout();
			this.tabDevelop.ResumeLayout(false);
			this.tabDevNav.ResumeLayout(false);
			this.tabCode.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tabGacReference.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.gbxGacIncluded.ResumeLayout(false);
			this.gbxGacNotUsed.ResumeLayout(false);
			this.tabOtherReference.ResumeLayout(false);
			this.tabTest.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.gbxTestSource.ResumeLayout(false);
			this.gbxTestSource.PerformLayout();
			this.gbxTestResult.ResumeLayout(false);
			this.gbxTestResult.PerformLayout();
			this.tabStore.ResumeLayout(false);
			this.tabStore.PerformLayout();
			this.trayContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
	}
}