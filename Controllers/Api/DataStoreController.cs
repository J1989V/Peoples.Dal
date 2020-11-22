using System.Collections.Generic;
using System.Web.Http;
using Peoples.Dal.Dtos;
using Peoples.Dal.Helpers;

namespace Peoples.Dal.Controllers.Api
{
	public class DataStoreController : ApiController
	{
		private DataStoreHelper _dataStoresHelper;

		public DataStoreController( )
		{
			_dataStoresHelper = new DataStoreHelper( );
		}

		[HttpGet]
		public List<DataStoreDto> GetPopiMetadata( )
		{
			var result = _dataStoresHelper.GetDataStores( );

			return result;
		}

		// POST
		public IHttpActionResult InsertPopiMetadata( List<DataStoreDto> dataStoreDtos )
		{
			var result = _dataStoresHelper.InsertDataStoreFields( dataStoreDtos );

			return Ok( result );
		}
	}
}