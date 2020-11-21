using System.Web.Http;
using Peoples.Dal.Helpers;
using Peoples.Dal.Models;

namespace Peoples.Dal.Controllers.Api
{
	public class PopiMetadataController : ApiController
	{
		private PopiMetadataHelper popiMetadatasHelper;

		public PopiMetadataController( )
		{
			popiMetadatasHelper = new PopiMetadataHelper( );
		}

		// GET
		public IHttpActionResult GetPopiMetadatas( string query = null )
		{
			var popiMetadatas = popiMetadatasHelper.GetPopiMetadatas( query );

			return Ok( popiMetadatas );
		}

		// POST
		public IHttpActionResult InsertPopiMetadatas( IdentificationNumberViewModel identificationNumberViewModel )
		{
			var result = popiMetadatasHelper.InsertIdentificationMetadata( identificationNumberViewModel );

			return Ok( result );
		}
	}
}