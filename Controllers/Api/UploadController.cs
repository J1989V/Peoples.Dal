using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Peoples.Dal.Dtos;
using Peoples.Dal.Helpers;

namespace Peoples.Dal.Controllers.Api
{
	public class UploadController : ApiController
	{
		[HttpPost]
		public async Task<DataSourceData> UploadFile( )
		{
			DataSourceData dataSourceData = new DataSourceData( );

			var provider = new MultipartMemoryStreamProvider( );
			await Request.Content.ReadAsMultipartAsync( provider );

			var file = provider.Contents.First( );
			var fileName = file.Headers.ContentDisposition.FileName.Trim( '\"' );
			var buffer = await file.ReadAsByteArrayAsync( );
			var stream = new MemoryStream( buffer );

			string fileExtention = Path.GetExtension( fileName );
			
			dataSourceData.DataStoreName = fileName;

			if ( fileExtention.Equals( ".txt" ) || fileExtention.Equals( ".csv" ) )
			{
				FileProcessingHelper.ProcessTextFile( dataSourceData, stream );
			}
			else if ( fileExtention.Equals( ".xlsx" ) )
			{
				FileProcessingHelper.ProcessExcelFile( dataSourceData, stream );
			}
			else if ( fileExtention.Equals( ".json" ) )
			{
				FileProcessingHelper.ProcessJsonFile( dataSourceData, stream );
			}

			return dataSourceData;
		}
	}
}