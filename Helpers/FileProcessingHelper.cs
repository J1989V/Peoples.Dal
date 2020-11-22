using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.WebPages;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using Peoples.Dal.Dtos;

namespace Peoples.Dal.Helpers
{
	public static class FileProcessingHelper
	{
		public static void ProcessTextFile( DataSourceData dataSourceData, MemoryStream stream )
		{
			dataSourceData.DataStoreType = "text/csv";
			dataSourceData.DataStoreTabName = "NA";
			dataSourceData.DataStoreLocation = "Unavailable";

			List<string> textLines = new List<string>( );

			using ( StreamReader sr = new StreamReader( stream ) )
			{
				string line;
				while ( ( line = sr.ReadLine( ) ) != null )
				{
					textLines.Add( line );
				}

				sr.Close( );
			}

			if ( textLines.Any( ) )
			{
				foreach ( var textLine in textLines )
				{
					string[ ] lineFields = textLine.Split( ',' ); //Assume normal comma separated (csv)

					if ( lineFields.Any( ) )
					{
						if ( !dataSourceData.ColumnsNames.Any( ) )
						{
							foreach ( var value in lineFields )
							{
								dataSourceData.ColumnsNames.Add( value );
							}

							continue;
						}

						FieldRow fieldRow = new FieldRow( );
						foreach ( var value in lineFields )
						{
							fieldRow.Fields.Add( new Field
							{
								Value = value,
								Approved = false,
								Category = Enums.Categories.Unknown,
								Column = dataSourceData.ColumnsNames[ fieldRow.Fields.Count ],
								Row = dataSourceData.FieldRows.Count
							} );
						}

						dataSourceData.FieldRows.Add( fieldRow );
					}
				}
			}

			ProcessDataSourceData( dataSourceData );
		}

		public static void ProcessExcelFile( DataSourceData dataSourceData, MemoryStream stream )
		{
			dataSourceData.DataStoreType = "Excel";
			dataSourceData.DataStoreLocation = "Unavailable";

			using ( XLWorkbook workbook = new XLWorkbook( stream ) )
			{
				IXLWorksheet worksheet = workbook.Worksheets.First( );
				dataSourceData.DataStoreTabName = worksheet.Name;

				IXLRange range = worksheet.RangeUsed( );

				if ( range.RowsUsed( ).Any( ) )
				{
					foreach ( var row in range.RowsUsed( ) )
					{
						if ( row.CellsUsed( ).Any( ) )
						{
							if ( !dataSourceData.ColumnsNames.Any( ) )
							{
								foreach ( var cell in row.CellsUsed( ) )
								{
									dataSourceData.ColumnsNames.Add( cell.Value.ToString( ).Trim( ) );
								}

								continue;
							}

							FieldRow fieldRow = new FieldRow( );
							foreach ( var cell in row.CellsUsed( ) )
							{
								fieldRow.Fields.Add( new Field
								{
									Value = cell.Value.ToString( ).Trim( ),
									Approved = false,
									Category = Enums.Categories.Unknown,
									Column = dataSourceData.ColumnsNames[ fieldRow.Fields.Count ],
									Row = dataSourceData.FieldRows.Count
								} );
							}

							dataSourceData.FieldRows.Add( fieldRow );
						}
					}
				}
			}

			ProcessDataSourceData( dataSourceData );
		}

		public static void ProcessJsonFile( DataSourceData dataSourceData, MemoryStream stream )
		{
			dataSourceData.DataStoreType = "json";
			dataSourceData.DataStoreTabName = "NA";
			dataSourceData.DataStoreLocation = "Unavailable";
			
			string text;
			using ( StreamReader sr = new StreamReader( stream ) )
			{
				text = sr.ReadToEnd( );
				sr.Close( );
			}

			JObject jObject = JObject.Parse( text );

			var data = jObject.First;

			JArray jArray = JArray.Parse( data.First.ToString( ) );

			IList<JsonDataBody> jsonDataBodies = jArray.ToObject<IList<JsonDataBody>>( );

			foreach ( var jsonDataBody in jsonDataBodies )
			{
				var propertyInfos = jsonDataBody.GetType( ).GetProperties( );

				if ( !dataSourceData.ColumnsNames.Any( ) )
				{
					foreach ( var propertyInfo in propertyInfos )
					{
						dataSourceData.ColumnsNames.Add( propertyInfo.Name );
					}
				}

				FieldRow fieldRow = new FieldRow( );
				foreach ( var propertyInfo in propertyInfos )
				{
					fieldRow.Fields.Add( new Field
					{
						Value = propertyInfo.GetValue( jsonDataBody, null ).ToString( ),
						Approved = false,
						Category = Enums.Categories.Unknown,
						Column = dataSourceData.ColumnsNames[ fieldRow.Fields.Count ],
						Row = dataSourceData.FieldRows.Count
					} );
				}

				dataSourceData.FieldRows.Add( fieldRow );

				ProcessDataSourceData( dataSourceData );
			}
		}

		private static void ProcessDataSourceData( DataSourceData dataSourceData )
		{
			foreach ( var fieldRow in dataSourceData.FieldRows )
			{
				foreach ( var field in fieldRow.Fields )
				{
					if ( IsIdNumber( field ) )
					{
						field.Category = Enums.Categories.IdNumber;
					}
					else if ( IsCellphoneNumber( field ) )
					{
						field.Category = Enums.Categories.CellphoneNumber;
					}
					else if ( IsEmailAddress( field ) )
					{
						field.Category = Enums.Categories.EmailAddress;
					}
					else if ( IsGender( field ) )
					{
						field.Category = Enums.Categories.Gender;
					}
					else if ( IsEthnicity( field ) )
					{
						field.Category = Enums.Categories.Ethnicity;
					}
					else if ( IsReligion( field ) )
					{
						field.Category = Enums.Categories.Religion;
					}
				}
			}
		}

		private static bool IsIdNumber( Field field )
		{
			return field.Value.IsDecimal( ) &&
			       field.Value.Length == 13 &&
			       CheckLuhn( field.Value );
		}

		private static bool CheckLuhn( string number )
		{
			int nDigits = number.Length;

			int nSum = 0;
			bool isSecond = false;
			for ( int i = nDigits - 1; i >= 0; i-- )
			{
				int d = number[ i ] - '0';

				if ( isSecond )
					d = d * 2;

				// We add two digits to handle
				// cases that make two digits 
				// after doubling
				nSum += d / 10;
				nSum += d % 10;

				isSecond = !isSecond;
			}

			return ( nSum % 10 == 0 );
		}

		private static bool IsCellphoneNumber( Field field )
		{
			return field.Value.IsDecimal( ) && field.Value.Length == 10;
		}

		private static bool IsEmailAddress( Field field )
		{
			return new EmailAddressAttribute( ).IsValid( field.Value );
		}

		private static bool IsGender( Field field )
		{
			return field.Value.Equals( "male", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "female", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "apache helicopter", StringComparison.InvariantCultureIgnoreCase );
		}

		private static bool IsEthnicity( Field field )
		{
			return field.Value.Equals( "white", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "black", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "indian", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "asian", StringComparison.InvariantCultureIgnoreCase );
		}

		private static bool IsReligion( Field field )
		{
			return field.Value.Equals( "christianity", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "islam", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "hindu", StringComparison.InvariantCultureIgnoreCase ) ||
			       field.Value.Equals( "judaism", StringComparison.InvariantCultureIgnoreCase );
		}

		private class JsonDataBody
		{
			public string national_id { get; set; }
			public string first_name { get; set; }
			public string last_name { get; set; }
			public string email_address { get; set; }
			public string cellphone_number { get; set; }
			public string gender { get; set; }
			public string ethnicity { get; set; }
			public string religion { get; set; }
			public string favourite_snack { get; set; }
		}
	}
}