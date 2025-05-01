namespace BOG.ClipboardMunger
{
	partial class frmScriptExec
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptExec));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayContextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.btnClearFilter = new System.Windows.Forms.Button();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.trvScripts = new System.Windows.Forms.TreeView();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tabstripMain = new System.Windows.Forms.TabControl();
			this.tabpageClipboardContent = new System.Windows.Forms.TabPage();
			this.txtClipboardContent = new System.Windows.Forms.TextBox();
			this.tabpageError = new System.Windows.Forms.TabPage();
			this.txtError = new System.Windows.Forms.TextBox();
			this.tabpageScript = new System.Windows.Forms.TabPage();
			this.tabstripScript = new System.Windows.Forms.TabControl();
			this.tabArguments = new System.Windows.Forms.TabPage();
			this.dgScriptArguments = new System.Windows.Forms.DataGridView();
			this.ArgumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentValidator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabExamples = new System.Windows.Forms.TabPage();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.cbxExampleList = new System.Windows.Forms.ComboBox();
			this.lblExampleSelection = new System.Windows.Forms.Label();
			this.tabTests = new System.Windows.Forms.TabControl();
			this.tabTestInput = new System.Windows.Forms.TabPage();
			this.txtTestInput = new System.Windows.Forms.TextBox();
			this.tabTestOutput = new System.Windows.Forms.TabPage();
			this.txtTestOutput = new System.Windows.Forms.TextBox();
			this.tabArgValues = new System.Windows.Forms.TabPage();
			this.dgvArgValues = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblSelectScript = new System.Windows.Forms.Label();
			this.btnMunge = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timerShutdown = new System.Windows.Forms.Timer(this.components);
			this.trayContextMenu1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tabstripMain.SuspendLayout();
			this.tabpageClipboardContent.SuspendLayout();
			this.tabpageError.SuspendLayout();
			this.tabpageScript.SuspendLayout();
			this.tabstripScript.SuspendLayout();
			this.tabArguments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgScriptArguments)).BeginInit();
			this.tabExamples.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.tabTests.SuspendLayout();
			this.tabTestInput.SuspendLayout();
			this.tabTestOutput.SuspendLayout();
			this.tabArgValues.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvArgValues)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipText = "Right-click for options";
			this.notifyIcon1.BalloonTipTitle = "Clipboard Munger";
			this.notifyIcon1.ContextMenuStrip = this.trayContextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Clipboard Munger";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// trayContextMenu1
			// 
			this.trayContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
			this.trayContextMenu1.Name = "trayContextMenu";
			this.trayContextMenu1.Size = new System.Drawing.Size(136, 32);
			this.trayContextMenu1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.trayContextMenu1_ItemClicked);
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
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 678);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1234, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(159, 17);
			this.toolStripStatusLabel1.Text = "Clipboard Munger Utility, 2.0";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.MinimumSize = new System.Drawing.Size(800, 428);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(1234, 678);
			this.splitContainer1.SplitterDistance = 418;
			this.splitContainer1.TabIndex = 2;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.btnClearFilter);
			this.splitContainer2.Panel1.Controls.Add(this.txtFilter);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.trvScripts);
			this.splitContainer2.Size = new System.Drawing.Size(418, 678);
			this.splitContainer2.SplitterDistance = 41;
			this.splitContainer2.TabIndex = 1;
			// 
			// btnClearFilter
			// 
			this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearFilter.Location = new System.Drawing.Point(388, 3);
			this.btnClearFilter.Name = "btnClearFilter";
			this.btnClearFilter.Size = new System.Drawing.Size(22, 19);
			this.btnClearFilter.TabIndex = 1;
			this.btnClearFilter.Text = "X";
			this.btnClearFilter.UseVisualStyleBackColor = true;
			this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
			// 
			// txtFilter
			// 
			this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilter.Location = new System.Drawing.Point(12, 3);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(369, 20);
			this.txtFilter.TabIndex = 0;
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			// 
			// trvScripts
			// 
			this.trvScripts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvScripts.Location = new System.Drawing.Point(0, 0);
			this.trvScripts.Name = "trvScripts";
			this.trvScripts.Size = new System.Drawing.Size(418, 633);
			this.trvScripts.TabIndex = 1;
			this.trvScripts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvScripts_AfterSelect);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tabstripMain);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnMunge);
			this.splitContainer3.Size = new System.Drawing.Size(812, 678);
			this.splitContainer3.SplitterDistance = 587;
			this.splitContainer3.TabIndex = 4;
			// 
			// tabstripMain
			// 
			this.tabstripMain.Controls.Add(this.tabpageClipboardContent);
			this.tabstripMain.Controls.Add(this.tabpageError);
			this.tabstripMain.Controls.Add(this.tabpageScript);
			this.tabstripMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabstripMain.Location = new System.Drawing.Point(0, 0);
			this.tabstripMain.Name = "tabstripMain";
			this.tabstripMain.SelectedIndex = 0;
			this.tabstripMain.Size = new System.Drawing.Size(812, 587);
			this.tabstripMain.TabIndex = 4;
			// 
			// tabpageClipboardContent
			// 
			this.tabpageClipboardContent.Controls.Add(this.txtClipboardContent);
			this.tabpageClipboardContent.Location = new System.Drawing.Point(4, 22);
			this.tabpageClipboardContent.Name = "tabpageClipboardContent";
			this.tabpageClipboardContent.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageClipboardContent.Size = new System.Drawing.Size(804, 561);
			this.tabpageClipboardContent.TabIndex = 0;
			this.tabpageClipboardContent.Text = "Clipboard Content";
			this.tabpageClipboardContent.UseVisualStyleBackColor = true;
			// 
			// txtClipboardContent
			// 
			this.txtClipboardContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtClipboardContent.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClipboardContent.Location = new System.Drawing.Point(3, 3);
			this.txtClipboardContent.Multiline = true;
			this.txtClipboardContent.Name = "txtClipboardContent";
			this.txtClipboardContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtClipboardContent.Size = new System.Drawing.Size(798, 555);
			this.txtClipboardContent.TabIndex = 0;
			// 
			// tabpageError
			// 
			this.tabpageError.Controls.Add(this.txtError);
			this.tabpageError.Location = new System.Drawing.Point(4, 22);
			this.tabpageError.Name = "tabpageError";
			this.tabpageError.Size = new System.Drawing.Size(804, 561);
			this.tabpageError.TabIndex = 2;
			this.tabpageError.Text = "Error";
			this.tabpageError.UseVisualStyleBackColor = true;
			// 
			// txtError
			// 
			this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtError.Location = new System.Drawing.Point(0, 0);
			this.txtError.Multiline = true;
			this.txtError.Name = "txtError";
			this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtError.Size = new System.Drawing.Size(804, 561);
			this.txtError.TabIndex = 1;
			// 
			// tabpageScript
			// 
			this.tabpageScript.Controls.Add(this.tabstripScript);
			this.tabpageScript.Controls.Add(this.lblSelectScript);
			this.tabpageScript.Location = new System.Drawing.Point(4, 22);
			this.tabpageScript.Name = "tabpageScript";
			this.tabpageScript.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageScript.Size = new System.Drawing.Size(804, 561);
			this.tabpageScript.TabIndex = 1;
			this.tabpageScript.Text = "Arguments";
			this.tabpageScript.ToolTipText = "Any values needed for the munge process";
			this.tabpageScript.UseVisualStyleBackColor = true;
			// 
			// tabstripScript
			// 
			this.tabstripScript.Controls.Add(this.tabArguments);
			this.tabstripScript.Controls.Add(this.tabExamples);
			this.tabstripScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabstripScript.Location = new System.Drawing.Point(3, 3);
			this.tabstripScript.Name = "tabstripScript";
			this.tabstripScript.SelectedIndex = 0;
			this.tabstripScript.Size = new System.Drawing.Size(798, 555);
			this.tabstripScript.TabIndex = 1;
			this.tabstripScript.Visible = false;
			// 
			// tabArguments
			// 
			this.tabArguments.Controls.Add(this.dgScriptArguments);
			this.tabArguments.Location = new System.Drawing.Point(4, 22);
			this.tabArguments.Name = "tabArguments";
			this.tabArguments.Padding = new System.Windows.Forms.Padding(3);
			this.tabArguments.Size = new System.Drawing.Size(790, 529);
			this.tabArguments.TabIndex = 0;
			this.tabArguments.Text = "Arguments";
			this.tabArguments.UseVisualStyleBackColor = true;
			// 
			// dgScriptArguments
			// 
			this.dgScriptArguments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgScriptArguments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArgumentName,
            this.ArgumentDefaultValue,
            this.ArgumentDescription,
            this.ArgumentValidator});
			this.dgScriptArguments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgScriptArguments.Location = new System.Drawing.Point(3, 3);
			this.dgScriptArguments.Name = "dgScriptArguments";
			this.dgScriptArguments.Size = new System.Drawing.Size(784, 523);
			this.dgScriptArguments.TabIndex = 4;
			// 
			// ArgumentName
			// 
			this.ArgumentName.HeaderText = "Argument Name";
			this.ArgumentName.Name = "ArgumentName";
			this.ArgumentName.ReadOnly = true;
			// 
			// ArgumentDefaultValue
			// 
			this.ArgumentDefaultValue.HeaderText = "Default Value";
			this.ArgumentDefaultValue.Name = "ArgumentDefaultValue";
			// 
			// ArgumentDescription
			// 
			this.ArgumentDescription.HeaderText = "Argument Description";
			this.ArgumentDescription.Name = "ArgumentDescription";
			this.ArgumentDescription.ReadOnly = true;
			// 
			// ArgumentValidator
			// 
			this.ArgumentValidator.HeaderText = "Argument Validator";
			this.ArgumentValidator.Name = "ArgumentValidator";
			this.ArgumentValidator.ReadOnly = true;
			// 
			// tabExamples
			// 
			this.tabExamples.Controls.Add(this.splitContainer4);
			this.tabExamples.Location = new System.Drawing.Point(4, 22);
			this.tabExamples.Name = "tabExamples";
			this.tabExamples.Size = new System.Drawing.Size(790, 529);
			this.tabExamples.TabIndex = 1;
			this.tabExamples.Text = "Examples";
			this.tabExamples.UseVisualStyleBackColor = true;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.IsSplitterFixed = true;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.cbxExampleList);
			this.splitContainer4.Panel1.Controls.Add(this.lblExampleSelection);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.tabTests);
			this.splitContainer4.Size = new System.Drawing.Size(790, 529);
			this.splitContainer4.SplitterDistance = 32;
			this.splitContainer4.TabIndex = 0;
			// 
			// cbxExampleList
			// 
			this.cbxExampleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxExampleList.FormattingEnabled = true;
			this.cbxExampleList.Location = new System.Drawing.Point(106, 6);
			this.cbxExampleList.Name = "cbxExampleList";
			this.cbxExampleList.Size = new System.Drawing.Size(677, 21);
			this.cbxExampleList.TabIndex = 1;
			this.cbxExampleList.SelectedIndexChanged += new System.EventHandler(this.cbxExampleList_SelectedIndexChanged);
			// 
			// lblExampleSelection
			// 
			this.lblExampleSelection.AutoSize = true;
			this.lblExampleSelection.Location = new System.Drawing.Point(14, 10);
			this.lblExampleSelection.Name = "lblExampleSelection";
			this.lblExampleSelection.Size = new System.Drawing.Size(86, 13);
			this.lblExampleSelection.TabIndex = 0;
			this.lblExampleSelection.Text = "Choose Example";
			// 
			// tabTests
			// 
			this.tabTests.Controls.Add(this.tabTestInput);
			this.tabTests.Controls.Add(this.tabTestOutput);
			this.tabTests.Controls.Add(this.tabArgValues);
			this.tabTests.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabTests.Location = new System.Drawing.Point(0, 0);
			this.tabTests.Name = "tabTests";
			this.tabTests.SelectedIndex = 0;
			this.tabTests.Size = new System.Drawing.Size(790, 493);
			this.tabTests.TabIndex = 0;
			// 
			// tabTestInput
			// 
			this.tabTestInput.Controls.Add(this.txtTestInput);
			this.tabTestInput.Location = new System.Drawing.Point(4, 22);
			this.tabTestInput.Name = "tabTestInput";
			this.tabTestInput.Padding = new System.Windows.Forms.Padding(3);
			this.tabTestInput.Size = new System.Drawing.Size(782, 467);
			this.tabTestInput.TabIndex = 0;
			this.tabTestInput.Text = "Test Input";
			this.tabTestInput.UseVisualStyleBackColor = true;
			// 
			// txtTestInput
			// 
			this.txtTestInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTestInput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTestInput.Location = new System.Drawing.Point(3, 3);
			this.txtTestInput.Multiline = true;
			this.txtTestInput.Name = "txtTestInput";
			this.txtTestInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTestInput.Size = new System.Drawing.Size(776, 461);
			this.txtTestInput.TabIndex = 1;
			// 
			// tabTestOutput
			// 
			this.tabTestOutput.Controls.Add(this.txtTestOutput);
			this.tabTestOutput.Location = new System.Drawing.Point(4, 22);
			this.tabTestOutput.Name = "tabTestOutput";
			this.tabTestOutput.Padding = new System.Windows.Forms.Padding(3);
			this.tabTestOutput.Size = new System.Drawing.Size(782, 467);
			this.tabTestOutput.TabIndex = 1;
			this.tabTestOutput.Text = "Test Output";
			this.tabTestOutput.UseVisualStyleBackColor = true;
			// 
			// txtTestOutput
			// 
			this.txtTestOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTestOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTestOutput.Location = new System.Drawing.Point(3, 3);
			this.txtTestOutput.Multiline = true;
			this.txtTestOutput.Name = "txtTestOutput";
			this.txtTestOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTestOutput.Size = new System.Drawing.Size(776, 461);
			this.txtTestOutput.TabIndex = 2;
			// 
			// tabArgValues
			// 
			this.tabArgValues.Controls.Add(this.dgvArgValues);
			this.tabArgValues.Location = new System.Drawing.Point(4, 22);
			this.tabArgValues.Name = "tabArgValues";
			this.tabArgValues.Size = new System.Drawing.Size(782, 467);
			this.tabArgValues.TabIndex = 2;
			this.tabArgValues.Text = "Argument Values";
			this.tabArgValues.UseVisualStyleBackColor = true;
			// 
			// dgvArgValues
			// 
			this.dgvArgValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvArgValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
			this.dgvArgValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvArgValues.Location = new System.Drawing.Point(0, 0);
			this.dgvArgValues.Name = "dgvArgValues";
			this.dgvArgValues.Size = new System.Drawing.Size(782, 467);
			this.dgvArgValues.TabIndex = 5;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Argument Name";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "Test Value";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// lblSelectScript
			// 
			this.lblSelectScript.AutoSize = true;
			this.lblSelectScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSelectScript.Location = new System.Drawing.Point(191, 270);
			this.lblSelectScript.Name = "lblSelectScript";
			this.lblSelectScript.Size = new System.Drawing.Size(423, 20);
			this.lblSelectScript.TabIndex = 6;
			this.lblSelectScript.Text = "Please select a munger function from the left panel.";
			// 
			// btnMunge
			// 
			this.btnMunge.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMunge.Enabled = false;
			this.btnMunge.Location = new System.Drawing.Point(0, 0);
			this.btnMunge.Name = "btnMunge";
			this.btnMunge.Size = new System.Drawing.Size(812, 87);
			this.btnMunge.TabIndex = 0;
			this.btnMunge.Text = "Munge";
			this.btnMunge.UseVisualStyleBackColor = true;
			this.btnMunge.Click += new System.EventHandler(this.btnMunge_Click);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timerShutdown
			// 
			this.timerShutdown.Tick += new System.EventHandler(this.timerShutdown_Tick);
			// 
			// frmScriptExec
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1234, 700);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmScriptExec";
			this.Text = "Clipboard Munger Utility";
			this.Activated += new System.EventHandler(this.frmScriptExec_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScriptExec_FormClosing);
			this.Resize += new System.EventHandler(this.frmScriptExec_Resize);
			this.trayContextMenu1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tabstripMain.ResumeLayout(false);
			this.tabpageClipboardContent.ResumeLayout(false);
			this.tabpageClipboardContent.PerformLayout();
			this.tabpageError.ResumeLayout(false);
			this.tabpageError.PerformLayout();
			this.tabpageScript.ResumeLayout(false);
			this.tabpageScript.PerformLayout();
			this.tabstripScript.ResumeLayout(false);
			this.tabArguments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgScriptArguments)).EndInit();
			this.tabExamples.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel1.PerformLayout();
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.tabTests.ResumeLayout(false);
			this.tabTestInput.ResumeLayout(false);
			this.tabTestInput.PerformLayout();
			this.tabTestOutput.ResumeLayout(false);
			this.tabTestOutput.PerformLayout();
			this.tabArgValues.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvArgValues)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip trayContextMenu1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TreeView trvScripts;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.Button btnClearFilter;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TabControl tabstripMain;
		private System.Windows.Forms.TabPage tabpageClipboardContent;
		private System.Windows.Forms.TextBox txtClipboardContent;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.TabPage tabpageScript;
		private System.Windows.Forms.TabControl tabstripScript;
		private System.Windows.Forms.TabPage tabArguments;
		private System.Windows.Forms.DataGridView dgScriptArguments;
		private System.Windows.Forms.Button btnMunge;
		private System.Windows.Forms.TabPage tabpageError;
		private System.Windows.Forms.TextBox txtError;
		private System.Windows.Forms.Timer timerShutdown;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentDefaultValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentValidator;
		private System.Windows.Forms.TabPage tabExamples;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.Label lblExampleSelection;
		private System.Windows.Forms.ComboBox cbxExampleList;
		private System.Windows.Forms.TabControl tabTests;
		private System.Windows.Forms.TabPage tabTestInput;
		private System.Windows.Forms.TabPage tabTestOutput;
		private System.Windows.Forms.TextBox txtTestInput;
		private System.Windows.Forms.TextBox txtTestOutput;
		private System.Windows.Forms.TabPage tabArgValues;
		private System.Windows.Forms.DataGridView dgvArgValues;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.Label lblSelectScript;
	}
}