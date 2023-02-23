using BOG.ClipboardMunger.Common.Entity;
using BOG.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOG.ClipboardMunger
{
	public partial class InputBox : Form
	{
		public bool IsCancelled = true;
		public string ValueEntered = string.Empty;

		Argument _arg;

		public InputBox(Argument arg)
		{
			InitializeComponent();
			_arg = arg;
		}

		private void InputBox_Load(object sender, EventArgs e)
		{
			this.Text = _arg.Title;
			this.txtDescription.Text = _arg.Description;
			this.txtValue.Text = _arg.DefaultValue;
			this.linkHelp.Visible = ! string.IsNullOrWhiteSpace(_arg.HelpUrl);
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
				var regex = new Regex(_arg.ValidatorRegex);
				if (!regex.IsMatch(this.txtValue.Text)) throw new Exception($"Fails validation with: {_arg.ValidatorRegex}");
				this.ValueEntered = this.txtValue.Text;
				this.IsCancelled = false;
				this.Close();
			}
			catch (Exception err)
			{
				MessageBox.Show(DetailedException.WithUserContent(ref err), "Invalid value entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			System.Diagnostics.Process.Start((string) this.linkHelp.Tag);
		}
	}
}
