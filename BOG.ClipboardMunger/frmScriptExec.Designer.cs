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
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
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
			this.tabpageScript = new System.Windows.Forms.TabPage();
			this.tabstripScript = new System.Windows.Forms.TabControl();
			this.tabArguments = new System.Windows.Forms.TabPage();
			this.dgScriptArguments = new System.Windows.Forms.DataGridView();
			this.ArgumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ArgumentValidator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnMunge = new System.Windows.Forms.Button();
			this.tabpageError = new System.Windows.Forms.TabPage();
			this.txtError = new System.Windows.Forms.TextBox();
			this.trayContextMenu.SuspendLayout();
			this.statusStrip.SuspendLayout();
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
			this.tabpageScript.SuspendLayout();
			this.tabstripScript.SuspendLayout();
			this.tabArguments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgScriptArguments)).BeginInit();
			this.tabpageError.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.Text = "Clipboard Munger";
			this.notifyIcon.Visible = true;
			// 
			// trayContextMenu
			// 
			this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
			this.trayContextMenu.Name = "trayContextMenu";
			this.trayContextMenu.Size = new System.Drawing.Size(136, 32);
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
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip.Location = new System.Drawing.Point(0, 428);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(800, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
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
			this.splitContainer1.Size = new System.Drawing.Size(800, 428);
			this.splitContainer1.SplitterDistance = 271;
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
			this.splitContainer2.Size = new System.Drawing.Size(271, 428);
			this.splitContainer2.SplitterDistance = 26;
			this.splitContainer2.TabIndex = 1;
			// 
			// btnClearFilter
			// 
			this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearFilter.Location = new System.Drawing.Point(241, 3);
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
			this.txtFilter.Size = new System.Drawing.Size(222, 20);
			this.txtFilter.TabIndex = 0;
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			// 
			// trvScripts
			// 
			this.trvScripts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvScripts.Location = new System.Drawing.Point(0, 0);
			this.trvScripts.Name = "trvScripts";
			this.trvScripts.Size = new System.Drawing.Size(271, 398);
			this.trvScripts.TabIndex = 1;
			this.trvScripts.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvScripts_NodeMouseClick);
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
			this.splitContainer3.Size = new System.Drawing.Size(525, 428);
			this.splitContainer3.SplitterDistance = 371;
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
			this.tabstripMain.Size = new System.Drawing.Size(525, 371);
			this.tabstripMain.TabIndex = 4;
			// 
			// tabpageClipboardContent
			// 
			this.tabpageClipboardContent.Controls.Add(this.txtClipboardContent);
			this.tabpageClipboardContent.Location = new System.Drawing.Point(4, 22);
			this.tabpageClipboardContent.Name = "tabpageClipboardContent";
			this.tabpageClipboardContent.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageClipboardContent.Size = new System.Drawing.Size(517, 345);
			this.tabpageClipboardContent.TabIndex = 0;
			this.tabpageClipboardContent.Text = "Clipboard Content";
			this.tabpageClipboardContent.UseVisualStyleBackColor = true;
			// 
			// txtClipboardContent
			// 
			this.txtClipboardContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtClipboardContent.Location = new System.Drawing.Point(3, 3);
			this.txtClipboardContent.Multiline = true;
			this.txtClipboardContent.Name = "txtClipboardContent";
			this.txtClipboardContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtClipboardContent.Size = new System.Drawing.Size(511, 339);
			this.txtClipboardContent.TabIndex = 0;
			// 
			// tabpageScript
			// 
			this.tabpageScript.Controls.Add(this.tabstripScript);
			this.tabpageScript.Location = new System.Drawing.Point(4, 22);
			this.tabpageScript.Name = "tabpageScript";
			this.tabpageScript.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageScript.Size = new System.Drawing.Size(517, 345);
			this.tabpageScript.TabIndex = 1;
			this.tabpageScript.Text = "Script";
			this.tabpageScript.ToolTipText = "Any values needed for the munge process";
			this.tabpageScript.UseVisualStyleBackColor = true;
			// 
			// tabstripScript
			// 
			this.tabstripScript.Controls.Add(this.tabArguments);
			this.tabstripScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabstripScript.Location = new System.Drawing.Point(3, 3);
			this.tabstripScript.Name = "tabstripScript";
			this.tabstripScript.SelectedIndex = 0;
			this.tabstripScript.Size = new System.Drawing.Size(511, 339);
			this.tabstripScript.TabIndex = 1;
			// 
			// tabArguments
			// 
			this.tabArguments.Controls.Add(this.dgScriptArguments);
			this.tabArguments.Location = new System.Drawing.Point(4, 22);
			this.tabArguments.Name = "tabArguments";
			this.tabArguments.Padding = new System.Windows.Forms.Padding(3);
			this.tabArguments.Size = new System.Drawing.Size(503, 313);
			this.tabArguments.TabIndex = 0;
			this.tabArguments.Text = "Arguments";
			this.tabArguments.UseVisualStyleBackColor = true;
			// 
			// dgScriptArguments
			// 
			this.dgScriptArguments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgScriptArguments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArgumentName,
            this.ArgumentValue,
            this.ArgumentDescription,
            this.ArgumentValidator});
			this.dgScriptArguments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgScriptArguments.Location = new System.Drawing.Point(3, 3);
			this.dgScriptArguments.Name = "dgScriptArguments";
			this.dgScriptArguments.Size = new System.Drawing.Size(497, 307);
			this.dgScriptArguments.TabIndex = 4;
			// 
			// ArgumentName
			// 
			this.ArgumentName.HeaderText = "ArgumentName";
			this.ArgumentName.Name = "ArgumentName";
			this.ArgumentName.ReadOnly = true;
			// 
			// ArgumentValue
			// 
			this.ArgumentValue.HeaderText = "ArgumentValue";
			this.ArgumentValue.Name = "ArgumentValue";
			// 
			// ArgumentDescription
			// 
			this.ArgumentDescription.HeaderText = "ArgumentDescription";
			this.ArgumentDescription.Name = "ArgumentDescription";
			this.ArgumentDescription.ReadOnly = true;
			// 
			// ArgumentValidator
			// 
			this.ArgumentValidator.HeaderText = "ArgumentValidator";
			this.ArgumentValidator.Name = "ArgumentValidator";
			this.ArgumentValidator.ReadOnly = true;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnMunge
			// 
			this.btnMunge.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMunge.Location = new System.Drawing.Point(0, 0);
			this.btnMunge.Name = "btnMunge";
			this.btnMunge.Size = new System.Drawing.Size(525, 53);
			this.btnMunge.TabIndex = 0;
			this.btnMunge.Text = "Munge";
			this.btnMunge.UseVisualStyleBackColor = true;
			this.btnMunge.Click += new System.EventHandler(this.btnMunge_Click);
			// 
			// tabpageError
			// 
			this.tabpageError.Controls.Add(this.txtError);
			this.tabpageError.Location = new System.Drawing.Point(4, 22);
			this.tabpageError.Name = "tabpageError";
			this.tabpageError.Size = new System.Drawing.Size(517, 345);
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
			this.txtError.Size = new System.Drawing.Size(517, 345);
			this.txtError.TabIndex = 1;
			// 
			// frmScriptExec
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmScriptExec";
			this.Text = "Clipboard Munger Utility";
			this.Activated += new System.EventHandler(this.frmScriptExec_Activated);
			this.Load += new System.EventHandler(this.ScriptExec_Load);
			this.trayContextMenu.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
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
			this.tabpageScript.ResumeLayout(false);
			this.tabstripScript.ResumeLayout(false);
			this.tabArguments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgScriptArguments)).EndInit();
			this.tabpageError.ResumeLayout(false);
			this.tabpageError.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip trayContextMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.StatusStrip statusStrip;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn ArgumentValidator;
		private System.Windows.Forms.Button btnMunge;
		private System.Windows.Forms.TabPage tabpageError;
		private System.Windows.Forms.TextBox txtError;
	}
}