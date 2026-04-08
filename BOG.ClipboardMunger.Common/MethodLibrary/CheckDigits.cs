using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class CheckDigits : ClipboardMungerProvider, IClipboardMungerProvider
	{
		public override string MethodName { get => "Check Digits"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description
		{
			get =>
		@"
           Check a number for a valid check-digit at the end, per the method selected";
		}

		public CheckDigits()
		{
			base.SetArgument(new Argument
			{
				Name = "Algorithm",
				Title = "Test with which algorithm?",
				DefaultValue = "Luhn Digit (validate)",
				Description = "The method to use for calucation",
				ValidatorRegex = @".+",
				SelectionList = new Dictionary<string, string>
				{
					{ "Luhn Digit (validate)", "Luhn.Validate"},
					{ "Luhn Digit (calculate)", "Luhn.Calculate"},
					{ "371 (validate)", "ABA.Routing.Validate"},
					{ "371 (calculate)", "ABA.Routing.Calculate"}
				}
			});

			base.Examples.Add("Luhn Valid", new Example
			{
				Input = "2222 4000-6000 0007 this is ignored",
				ArgumentValues = new Dictionary<string, string> { { "Algorithm", "Luhn.Validate" } }
			});

			// 345765597942575
			base.Examples.Add("Luhn Invalid", new Example
			{
				Input = "345765597942575",
				ArgumentValues = new Dictionary<string, string> { { "Algorithm", "Luhn.Validate" } }
			});
		}

		public override string Munge(string textToMunge)
		{
			var algorithm = this.ArgumentValues["Algorithm"];
			var result = string.Empty;

			if (algorithm == "Luhn.Validate")
			{
				var number = OnlyDigits(textToMunge);

				var sum = 0;
				var shouldApplyDouble = true;
				for (var index = number.Length - 2; index >= 0; index--)
				{
					var currentDigit = (Int32)Char.GetNumericValue(number, index);
					if (shouldApplyDouble)
					{
						if (currentDigit > 4)
						{
							sum += currentDigit * 2 - 9;
						}
						else
						{
							sum += currentDigit * 2;
						}
					}
					else
					{
						sum += currentDigit;
					}
					shouldApplyDouble = !shouldApplyDouble;
				}
				var checkDigit = 10 - (sum % 10);

				result = Char.GetNumericValue(number[number.Length - 1]) == checkDigit ? "Valid" : "NOT Valid";
			}
			else if (algorithm == "ABA.Routing.Validate")
			{
				var number = OnlyDigits(textToMunge);

				// Routing numbers must be exactly 9 digits
				if (string.IsNullOrWhiteSpace(number) || number.Length != 9)
					throw new ArgumentException("ABA Ruting number must be 9 digits in length");

				int sum = 0;
				for (int i = 0; i < 9; i++)
				{
					int digit = number[i] - '0';

					// Apply weights: 3 for 1st, 4th, 7th; 7 for 2nd, 5th, 8th; 1 for 3rd, 6th, 9th
					if (i % 3 == 0) sum += 3 * digit;
					else if (i % 3 == 1) sum += 7 * digit;
					else sum += digit;
				}
				result = (sum % 10 == 0) ? "Valid" : "NOT Valid";
			}

			else throw new NotImplementedException($"The algorithm {algorithm} is not implemented yet.");

			return result;
		}

		private string OnlyDigits(string input)
		{
			var result = string.Empty;
			foreach (var character in input)
			{
				if (Char.IsDigit(character))
				{
					result += character;
				}
			}
			return result;
		}	
	}
}
