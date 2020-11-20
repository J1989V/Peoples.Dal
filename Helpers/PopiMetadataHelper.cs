using System;
using System.Collections.Generic;
using System.Linq;
using Peoples.Dal.Models;

namespace Peoples.Dal.Helpers
{
	public class PopiMetadataHelper
	{
		private readonly ApplicationDbContext _context;

		public PopiMetadataHelper( )
		{
			_context = new ApplicationDbContext( );
		}

		public List<PopiMetadata> GetPopiMetadatas( string query = null )
		{
			var popiMetadatasQuery = _context.PopiMetadata.AsQueryable( );

			if ( !String.IsNullOrWhiteSpace( query ) )
			{
				// ToDo : Figure out what actually needs to be searched against???
				popiMetadatasQuery = popiMetadatasQuery
					.Where( p => p.DatastoreName.Contains( query ) );
			}

			return popiMetadatasQuery.ToList( );
		}
	}
}