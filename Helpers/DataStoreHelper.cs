using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Peoples.Dal.Dtos;
using Peoples.Dal.Enums;
using Peoples.Dal.Models;
using Field = Peoples.Dal.Models.Field;

namespace Peoples.Dal.Helpers
{
	public class DataStoreHelper
	{
		private readonly PopiDbContext _context;

		public DataStoreHelper( )
		{
			_context = new PopiDbContext( );
		}

		public List<DataStoreDto> GetDataStores( string query = null )
		{
			var dataStoresQuery = _context.Fields
				.Include( t => t.TableTab )
				.Select( d => new DataStoreDto
				{
					FieldName = d.Name,
					FieldRow = d.Row,
					TableTabName = d.TableTab.Name,
					DataStoreName = d.TableTab.DataStore.Name,
					DataStoreType = d.TableTab.DataStore.Type,
					DataStoreLocation = d.TableTab.DataStore.Location,
					Category = ( Categories )d.Category
				} );

			return dataStoresQuery.ToList( );
		}

		public CallResult InsertDataStoreFields( List<DataStoreDto> dataStoreDtos )
		{
			foreach ( var dataStoreDto in dataStoreDtos )
			{
				DataStore dataStore = _context.DataStores
					                      .FirstOrDefault( x =>
						                      x.Name == dataStoreDto.DataStoreName &&
						                      x.Type == dataStoreDto.DataStoreType &&
						                      x.Location == dataStoreDto.DataStoreLocation )
				                      ?? // If not found, create new
				                      new DataStore
				                      {
					                      Name = dataStoreDto.DataStoreName,
					                      Type = dataStoreDto.DataStoreType,
					                      Location = dataStoreDto.DataStoreLocation
				                      };

				TableTab tableTab = _context.TableTabs
					                    .FirstOrDefault( x =>
						                    x.Name == dataStoreDto.TableTabName &&
						                    x.DataStoreId == dataStore.Id )
				                    ?? // If not found, create new
				                    new TableTab
				                    {
					                    Name = dataStoreDto.TableTabName,
					                    DataStoreId = dataStore.Id
				                    };

				Field field = _context.Fields
					              .FirstOrDefault( x =>
						              x.Name == dataStoreDto.FieldName &&
						              x.Row == dataStoreDto.FieldRow &&
						              x.TableTabId == tableTab.Id )
				              ?? // If not found, create new
				              new Field
				              {
					              Name = dataStoreDto.FieldName,
					              Row = dataStoreDto.FieldRow,
					              Category = ( int )dataStoreDto.Category,
					              TableTabId = tableTab.Id
				              };

				if ( !field.Category.Equals( ( int )dataStoreDto.Category ) )
					field.Category = ( int )dataStoreDto.Category;

				if ( dataStore.Id == 0 )
					_context.DataStores.Add( dataStore );

				if ( tableTab.Id == 0 )
					_context.TableTabs.Add( tableTab );

				if ( field.Id == 0 )
					_context.Fields.Add( field );

				_context.SaveChanges( );
			}

			return new CallResult { Result = "Success" };
		}
	}
}