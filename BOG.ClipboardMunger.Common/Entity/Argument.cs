using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.ClipboardMunger.Common.Entity
{
	public class Argument
	{
		[CategoryAttribute("Admin"), DisplayNameAttribute("Name"), DescriptionAttribute("The primary key for the argument."), ReadOnly(false)]
		public string Name { get; set; }
		[CategoryAttribute("Admin"), DisplayNameAttribute("Description"), DescriptionAttribute("What the argument supplies."), ReadOnly(false)]
		public string Description { get; set; }
		[CategoryAttribute("Control"), DisplayNameAttribute("DefaultValue"), DescriptionAttribute("What the argument has if the operator or example provides no value."), ReadOnly(false)]
		public string DefaultValue { get; set; }
		[CategoryAttribute("Control"), DisplayNameAttribute("ValidatorRegex"), DescriptionAttribute("(Optional): The regular expression used to ensure valid data."), ReadOnly(false)]
		public string ValidatorRegex { get; set; }
	}
}
