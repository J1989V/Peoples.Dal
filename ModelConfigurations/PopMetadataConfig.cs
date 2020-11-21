using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class PopMetadataConfig : EntityTypeConfiguration<PopiMetadata>
	{
		public PopMetadataConfig( )
		{
			HasKey( p => p.Id );
		}
	}
}