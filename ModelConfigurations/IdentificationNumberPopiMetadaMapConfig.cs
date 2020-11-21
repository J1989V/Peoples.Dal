using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class IdentificationNumberPopiMetadaMapConfig : EntityTypeConfiguration<IdentificationNumberPopiMetadataMap>
	{
		public IdentificationNumberPopiMetadaMapConfig( )
		{
			ToTable( "IdentificationNumberPopiMetadataMap" );

			HasKey( x => x.Id );

			HasRequired( x => x.IdentificationNumber )
				.WithMany( x => x.IdentificationNumberPopiMetadataMaps )
				.HasForeignKey( x => x.IdentificationNumberId );

			HasRequired( x => x.PopiMetadata )
				.WithMany( x => x.IdentificationNumberPopiMetadataMaps )
				.HasForeignKey( x => x.PopiMetadataId );
		}
	}
}