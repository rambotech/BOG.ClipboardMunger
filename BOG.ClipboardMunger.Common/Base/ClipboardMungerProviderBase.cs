using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

		public virtual string Munge(string clipboardSource)
		{
			throw new NotImplementedException();
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

		public Dictionary<string, Example> GetExamples()
		{
			var result = new Dictionary<string, Example>();
			foreach (var key in Examples.Keys)
			{
				result.Add(key, Examples[key]);
			}
			return result;
		}

		public string MungeExample(string source, Dictionary<string, string> dialogTestArguments)
		{
			ArgumentValues.Clear();
			foreach (var key in Arguments.Keys)
			{
				ArgumentValues[key] = string.Empty;
			}

			foreach (var key in dialogTestArguments.Keys)
			{
				if (Arguments.ContainsKey(key))
				{
					ValidateValue(Arguments[key].ValidatorRegex, Arguments[key].Name, dialogTestArguments[key]);
					ArgumentValues[key] = dialogTestArguments[key];
				}
			}
			var result = Munge(source);
			ArgumentValues.Clear();
			return result;
		}

		public string MungeClipboard(string source, Dictionary<string, string> dialogTestArguments)
		{
			ArgumentValues.Clear();
			foreach (var key in Arguments.Keys)
			{
				ArgumentValues[key] = string.Empty;
			}

			foreach (var key in dialogTestArguments.Keys)
			{
				if (Arguments.ContainsKey(key))
				{
					ValidateValue(Arguments[key].ValidatorRegex, Arguments[key].Name, dialogTestArguments[key]);
					ArgumentValues[key] = dialogTestArguments[key];
				}
			}
			var result = Munge(source);
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
