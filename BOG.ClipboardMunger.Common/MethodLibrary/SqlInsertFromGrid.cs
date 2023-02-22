using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class SqlInsertFromGrid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "SqlInsertFromGrid"; }
		public override string GroupName { get => "String-Magic"; }
		public override string Description { get; }

		public SqlInsertFromGrid()
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
		}

		public override string Munge(string clipboardSource)
		{
			var databaseName = System.Web.HttpUtility.UrlDecode(ArgumentValues["databaseName"]);
			var tableName = System.Web.HttpUtility.UrlDecode(ArgumentValues["tableName"]);

			StringBuilder result = new StringBuilder();
			int LineIndex = 0;
			int ColumnIndex = 0;
			int ColumnCount = 0;
			string InsertHeader = string.Empty;

			if (databaseName != string.Empty)
			{
				result.AppendLine(string.Format("USE [{0}]", databaseName));
				result.AppendLine("GO");
				result.AppendLine();
			}

			result.AppendLine(string.Format("-- SET IDENTITY_INSERT {0} ON;", tableName));
			result.AppendLine(string.Format("BEGIN TRAN", tableName));
			result.AppendLine();
			result.AppendLine(string.Format("BEGIN TRY", tableName));
			result.AppendLine();

			foreach (string ThisLine in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (LineIndex == 0)
				{
					ColumnIndex = 0;
					InsertHeader = string.Format("\tINSERT INTO [{0}] (\r\n", tableName);
					foreach (string ColumnName in ThisLine.Split(new char[] { '\t' }))
					{
						if (ColumnIndex > 0)
						{
							InsertHeader += ",\r\n";
						}
						InsertHeader += "\t\t[" + ColumnName + "]";
						ColumnIndex++;
					}
					InsertHeader += "\r\n\t) VALUES (\r\n";
					ColumnCount = ColumnIndex;
					LineIndex++;
				}
				else
				{
					string InsertLine = InsertHeader;
					ColumnIndex = 0;
					foreach (string ColumnValue in ThisLine.Split(new char[] { '\t' }))
					{
						if (ColumnIndex > 0)
						{
							InsertLine += ",\r\n";
						}
						if (ColumnValue == "NULL")
						{
							InsertLine += "\t\t" + ColumnValue;
						}
						else
						{
							InsertLine += "\t\t'" + ColumnValue.Replace("'", "''") + "'";
						}
						ColumnIndex++;
					}
					if (ColumnIndex == ColumnCount)
					{
						InsertLine += "\r\n\t)";
						result.AppendLine(InsertLine);
						result.AppendLine();
					}
					else
					{
						result.AppendLine(string.Format("\t/* Line {0}: header count is: {1}, but value count is: {2} -- skipped */", LineIndex + 1, ColumnCount, ColumnIndex));
						result.AppendLine();
					}
				}
			}
			LineIndex++;

			result.AppendLine("\tCOMMIT /* Success if at this point */");
			result.AppendLine("\tPRINT 'Successful... committed'");
			result.AppendLine("END TRY");
			result.AppendLine();
			result.AppendLine("BEGIN CATCH");
			result.AppendLine("\tROLLBACK");
			result.AppendLine("\tPRINT 'Failure... rollback'");
			result.AppendLine("END CATCH");
			result.AppendLine();
			result.AppendLine(string.Format("-- SET IDENTITY_INSERT {0} OFF;", tableName));
			result.AppendLine();
			result.AppendLine("GO");
			result.AppendLine();
			result.AppendLine("/*  END OF DOCUMENT  */");
			result.AppendLine();

			return result.ToString();
		}
	}
}
