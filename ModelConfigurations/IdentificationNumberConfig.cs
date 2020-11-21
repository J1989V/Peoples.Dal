using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class IdentificationNumberConfig : EntityTypeConfiguration<IdentificationNumber>
	{
		public IdentificationNumberConfig( )
		{
			HasKey( x => x.Id );
		}
	}
}