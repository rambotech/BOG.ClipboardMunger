using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BOG.ClipboardMunger.Common.Base
{
	public class ClipboardMungerProviderBase : IClipboardMungerProvider
	{
		/// <summary>
		/// The name of the method (e.g. Base64Encode) as displayed in the UI and context menu.
		/// </summary>
		public virtual string MethodName { get => throw new NotImplementedException("MethodName not defined"); }
		/// <summary>
		/// The name of the group (e.g. Encoding) as displayed in the UI and context menu.
		/// </summary>
		public virtual string GroupName { get => throw new NotImplementedException("GroupName not defined"); }
		public virtual string Description { get => "No description available"; }

		protected Dictionary<string, Argument> Arguments = new Dictionary<string, Argument>();
		protected Dictionary<string, Example> Examples = new Dictionary<string, Example>();
		protected Dictionary<string, string> ArgumentValues = new Dictionary<string, string>();

		public string ClipboardSource { get; set; } = Clipboard.GetText();

		/// <summary>
		/// Inheriting class must override.  This is the method called to change the clipboard or example text.
		/// No try/catch block needed: the caller in the Windows form handles any exceptions.
		/// Caller should use base.ClipboardSource as the argument when not executing an example.
		/// </summary>
		/// <param name="textToMunge"></param>
		/// <returns></returns>
		public virtual string Munge(string textToMunge)
		{
			throw new NotImplementedException();
		}

		public List<string> GetExampleNames()
		{
			return Examples.Keys.ToList();
		}

		public Example GetExample(string exampleName)
		{
			if (Examples.ContainsKey(exampleName))
			{
				return Examples[exampleName];
			}
			else
			{
				return null;
			}
		}

		public void SetArgument(Argument argument)
		{
			if (Arguments.Keys.Contains(argument.Name))
			{
				Arguments[argument.Name] = argument;
			}
			else
			{
				Arguments.Add(argument.Name, argument);
			}
		}

		public Dictionary<string, Argument> GetArguments()
		{
			var result = new Dictionary<string, Argument>();
			foreach (var key in Arguments.Keys)
			{
				result.Add(key, Arguments[key]);
			}
			return result;
		}

		public string MungeClipboard(Dictionary<string, string> argumentValues)
		{
			return MungeClipboard(argumentValues, this.ClipboardSource);
		}

		public string MungeClipboard(Dictionary<string, string> argumentValues, string textToMunge)
		{
			ArgumentValues.Clear();
			foreach (var key in Arguments.Keys)
			{
				ArgumentValues[key] = string.Empty;
			}

			foreach (var key in argumentValues.Keys)
			{
				if (Arguments.ContainsKey(key))
				{
					ValidateValue(Arguments[key].ValidatorRegex, Arguments[key].Name, argumentValues[key]);
					ArgumentValues[key] = argumentValues[key];
				}
			}
			var result = Munge(textToMunge);
			ArgumentValues.Clear();
			return result;
		}

		private void ValidateValue(string regExpression, string argName, string value)
		{
			var regex = new Regex(regExpression, RegexOptions.Singleline | RegexOptions.IgnoreCase);
			if (!regex.IsMatch(value))
			{
				throw new ArgumentException($"Argument {argName}: value \"{value}\" does not validate against regex pattern \"{regExpression}\"");
			}
		}
	}
}
