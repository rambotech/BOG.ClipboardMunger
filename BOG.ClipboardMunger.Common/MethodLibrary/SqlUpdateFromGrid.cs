using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class SqlUpdateFromGrid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Sql UPDATE From Grid"; }
		public override string GroupName { get => "SQL"; }
		public override string Description { get; }

		public SqlUpdateFromGrid() 
        {
			base.SetArgument(new Argument
			{
				Name = "databaseName",
				Title = "Database Name",
				DefaultValue = "MyDatabaseName",
				Description = @"The database name for the USE statement",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "tableName",
				Title = "Table Name",
				DefaultValue = "MyTableName",
				Description = @"The table name for the inserted rows",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "columnsForPrimaryKey",
				Title = "Primary Key",
				DefaultValue = "1",
				Description = @"How many of the left most columns comprise the primary key",
				ValidatorRegex = @"[\d]+"
			});
		}

		public override string Munge(string textToMunge)
		{
			var databaseName = System.Web.HttpUtility.UrlDecode(ArgumentValues["databaseName"]);
			var tableName = System.Web.HttpUtility.UrlDecode(ArgumentValues["tableName"]);
			var ColumnsInPrimaryKey = int.Parse(ArgumentValues["columnsForPrimaryKey"]);

			StringBuilder result = new StringBuilder();
			int LineIndex = 0;
			string InsertHeader = string.Empty;
			if (ColumnsInPrimaryKey < 1)
			{
				ColumnsInPrimaryKey = 1;
			}

			result.AppendLine(string.Format("BEGIN TRAN", tableName));
			result.AppendLine();
			result.AppendLine(string.Format("BEGIN TRY", tableName));
			result.AppendLine();

			string[] Lines = textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			string[] HeaderColumnNames = Lines[0].Split(new char[] { '\t' });
			if (HeaderColumnNames.Length <= ColumnsInPrimaryKey)
			{
				throw new Exception(string.Format("WTF?.. Columns in row == {0}, and primary key column count == {1}", HeaderColumnNames.Length, ColumnsInPrimaryKey));
			}

			foreach (string ThisLine in Lines)
			{
				LineIndex++;
				if (LineIndex == 1) continue;

				string[] ColumnNames = ThisLine.Split(new char[] { '\t' });
				string UpdateLine = string.Empty;

				if (ColumnNames.Length != HeaderColumnNames.Length)
				{
					result.AppendLine(string.Format("\t/* Line {0}: header count is: {1}, but value count is: {2} -- skipped */", LineIndex, HeaderColumnNames.Length, ColumnNames.Length));
					result.AppendLine();
					continue;
				}
				UpdateLine = string.Format("\tUPDATE [{0}] SET\r\n", tableName);
				for (int Index = ColumnsInPrimaryKey; Index < ColumnNames.Length; Index++)
				{
					if (Index > ColumnsInPrimaryKey && Index < ColumnNames.Length)
					{
						UpdateLine += ",\r\n";
					}
					if (ColumnNames[Index] == "NULL")
					{
						UpdateLine += "\t\t[" + HeaderColumnNames[Index] + "] = " + ColumnNames[Index];
					}
					else
					{
						UpdateLine += "\t\t[" + HeaderColumnNames[Index] + "] = '" + ColumnNames[Index] + "'";
					}
				}
				UpdateLine += "\r\n\tWHERE ";
				for (int Index = 0; Index < ColumnsInPrimaryKey; Index++)
				{
					if (Index > 0)
					{
						UpdateLine += "\r\n\tAND   ";
					}
					if (ColumnNames[Index] == "NULL")
					{
						UpdateLine += "[" + HeaderColumnNames[Index] + "] = '" + ColumnNames[Index] + "'";
					}
					else
					{
						UpdateLine += "[" + HeaderColumnNames[Index] + "] = '" + ColumnNames[Index] + "'";
					}
				}
				result.AppendLine(UpdateLine);
				result.AppendLine();
			}

			result.AppendLine("\tCOMMIT TRAN  /* Success if at this point */");
			result.AppendLine("\tPRINT 'Successful... committed'");
			result.AppendLine("END TRY");
			result.AppendLine();
			result.AppendLine("BEGIN CATCH");
			result.AppendLine("\tROLLBACK TRAN");
			result.AppendLine("\tPRINT 'Failure... rollback'");
			result.AppendLine("END CATCH");
			result.AppendLine();
			result.AppendLine("GO");
			result.AppendLine();
			result.AppendLine("/*  END OF DOCUMENT  */");
			result.AppendLine();

			return result.ToString();
		}
	}
}
