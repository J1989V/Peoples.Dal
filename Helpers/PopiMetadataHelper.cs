using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
			var popiMetadatasQuery = _context.PopiMetadatas.AsQueryable( );

			if ( !String.IsNullOrWhiteSpace( query ) )
			{
				// ToDo : Figure out what actually needs to be searched against???
				// popiMetadatasQuery = popiMetadatasQuery
				// 	.Where( p => p.DatastoreName.Contains( query ) );
			}

			return popiMetadatasQuery.ToList( );
		}

		public CallResult InsertIdentificationMetadata( IdentificationNumberViewModel identificationNumberViewModel )
		{
			IdentificationNumber identificationNumber = _context.IdentificationNumbers
				                                            .First( x => x
					                                            .SaltedIdentificationNumber == identificationNumberViewModel.SaltedIdentificationNumber )
			                                            ?? // If not found, create new
			                                            new IdentificationNumber
			                                            {
				                                            SaltedIdentificationNumber = identificationNumberViewModel.SaltedIdentificationNumber
			                                            };

			PopiMetadata popiMetadata = _context.PopiMetadatas
				                            .First( x => x
					                                         .Category == identificationNumberViewModel.Category
				                                         && x.DatastoreName == identificationNumberViewModel.DatastoreName
				                                         && x.DatastoreType == identificationNumberViewModel.DatastoreType
				                                         && x.FieldType == identificationNumberViewModel.FieldType )
			                            ?? // If not found, create new
			                            new PopiMetadata
			                            {
				                            DatastoreName = identificationNumberViewModel.DatastoreName,
				                            DatastoreType = identificationNumberViewModel.DatastoreType,
				                            FieldType = identificationNumberViewModel.FieldType,
				                            Category = identificationNumberViewModel.Category
			                            };

			IdentificationNumberPopiMetadataMap identificationNumberPopiMetadataMap = new IdentificationNumberPopiMetadataMap
			{
				IdentificationNumberId = identificationNumber.Id,
				PopiMetadataId = popiMetadata.Id
			};

			_context.IdentificationNumbers.Add( identificationNumber );
			_context.PopiMetadatas.Add( popiMetadata );
			_context.IdentificationNumberPopiMetadataMaps.Add( identificationNumberPopiMetadataMap );

			_context.SaveChanges( );

			return new CallResult { Result = "Success" };
		}
	}
}