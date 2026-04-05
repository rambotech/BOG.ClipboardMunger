using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Entity;
using BOG.Framework;

namespace BOG.ClipboardMunger
{
    public partial class frmSelectionBox : Form
    {
        public bool IsCancelled = true;
        public string ValueSelected = string.Empty;

        Argument _arg;

        public frmSelectionBox(Argument arg)
        {
            InitializeComponent();
            _arg = arg;
        }

        private void frmSelectionBox_Load(object sender, EventArgs e)
        {
            this.Text = _arg.Title;
            this.txtDescription.Text = _arg.Description;
            this.linkHelp.Tag = _arg.HelpUrl;
            this.linkHelp.Visible = !string.IsNullOrWhiteSpace(_arg.HelpUrl);

            var selectItemIndex = -1;
            var itemIndex = 0;
            if (_arg.SelectionList.Keys.Count == 0)
            {
                MessageBox.Show("The list of items to choose from is empty, closing dialog.", "Can't continue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IsCancelled = true;
                this.Close();
            }

            foreach (var item in _arg.SelectionList.Keys)
            {
                itemIndex = this.cbxitemSelections.Items.Count;
                this.cbxitemSelections.Items.Add(item);
                if (string.Compare(item, _arg.DefaultValue, true) == 0) // case insensitive match
                {
                    if (selectItemIndex == -1)
                    {
                        selectItemIndex = itemIndex;
                    }
                    if (string.Compare(item, _arg.DefaultValue, false) == 0) // case sensitive match
                    {
                        selectItemIndex = itemIndex; // preferred match as default
                    }
                }
            }
            this.cbxitemSelections.SelectedIndex = selectItemIndex == -1 ? 0 : selectItemIndex;

            if (this.linkHelp.Visible)
            {
                ToolTip t = new ToolTip();
                t.Active = true;
                t.AutoPopDelay = 4000;
                t.InitialDelay = 600;
                t.IsBalloon = true;
                this.linkHelp.Tag = _arg.HelpUrl;
                this.linkHelp.Text = "Hover here to preview site location, click to view it";
                t.SetToolTip(linkHelp, _arg.HelpUrl);
            }
            this.CenterToScreen();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.ValueSelected = _arg.SelectionList[(string)this.cbxitemSelections.Items[this.cbxitemSelections.SelectedIndex]];
                var regex = new Regex(_arg.ValidatorRegex);
                if (!regex.IsMatch(this.ValueSelected)) throw new Exception($"Fails validation with: {_arg.ValidatorRegex}");
                this.IsCancelled = false;
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(DetailedException.WithUserContent(ref err), "Error with item selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkHelp.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start((string)this.linkHelp.Tag);
        }


        private void cbxitemSelections_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValueSelected = (string)this.cbxitemSelections.Items[this.cbxitemSelections.SelectedIndex];
        }
    }
}
