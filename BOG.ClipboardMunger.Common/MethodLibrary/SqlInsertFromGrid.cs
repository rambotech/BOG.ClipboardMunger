using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;
using System.Windows.Forms;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class SqlInsertFromGrid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Sql INSERT From Grid"; }
		public override string GroupName { get => "SQL"; }
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
			base.SetArgument(new Argument
			{
				Name = "maxValuesPerInsert",
				Title = "Value objects per insert",
				DefaultValue = "1",
				Description = @"More than one value (row) can be used per insert in later versions.  Range 1 to 100 (highest recommended)",
				ValidatorRegex = @"\d+"
			});
		}

		public override string Munge(string textToMunge)
		{
			var databaseName = System.Web.HttpUtility.UrlDecode(ArgumentValues["databaseName"]);
			var tableName = System.Web.HttpUtility.UrlDecode(ArgumentValues["tableName"]);
			var maxValuesPerInsert = int.Parse(System.Web.HttpUtility.UrlDecode(ArgumentValues["maxValuesPerInsert"]));

			int orgValueCount = maxValuesPerInsert;
			maxValuesPerInsert = (maxValuesPerInsert < 0) ? 1 : ((maxValuesPerInsert > 100) ? 100 : maxValuesPerInsert);
			if (maxValuesPerInsert != orgValueCount)
			{
				MessageBox.Show($"The value has been adjusted to {maxValuesPerInsert} for {orgValueCount} due to limits", "Value count range adjustment", MessageBoxButtons.OK);
			}
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
			if (maxValuesPerInsert == 1)
			{
				result.AppendLine();
			}

			var ValueLinesCreated = 0;
			foreach (string ThisLine in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (LineIndex == 0)
				{
					ColumnIndex = 0;
					if (maxValuesPerInsert > 1)
					{
						InsertHeader += "\r\n";
					}
					InsertHeader += string.Format("\tINSERT INTO [{0}] (", tableName);
					foreach (string ColumnName in ThisLine.Split(new char[] { '\t' }))
					{
						if (ColumnIndex > 0)
						{
							InsertHeader += ",";
						}
						InsertHeader += "[" + ColumnName + "]";
						ColumnIndex++;
					}
					InsertHeader += "\r\n\t) VALUES\r\n";
					ColumnCount = ColumnIndex;
				}
				else
				{
					string InsertLine = string.Empty;
					if (ValueLinesCreated == 0)
					{
						InsertLine = InsertHeader + "\t  (";
					}
					else if (maxValuesPerInsert > 1)
					{
						InsertLine += "\t  ,(";
					}

					ColumnIndex = 0;
					foreach (string ColumnValue in ThisLine.Split(new char[] { '\t' }))
					{
						if (ColumnIndex > 0)
						{
							InsertLine += ",";
						}
						if (ColumnValue == "NULL")
						{
							InsertLine += ColumnValue;
						}
						else
						{
							InsertLine += "'" + ColumnValue.Replace("'", "''") + "'";
						}
						ColumnIndex++;
					}
					if (ColumnIndex == ColumnCount)
					{
						InsertLine += ")";
						ValueLinesCreated++;
						result.AppendLine(InsertLine);
					}
					else
					{
						result.AppendLine(string.Format("\t/* Line {0}: header count is: {1}, but value count is: {2} -- skipped */", LineIndex + 1, ColumnCount, ColumnIndex));
						result.AppendLine();
					}
					ValueLinesCreated++;
					ValueLinesCreated %= maxValuesPerInsert;
				}
				LineIndex++;
			}

			result.AppendLine();
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
