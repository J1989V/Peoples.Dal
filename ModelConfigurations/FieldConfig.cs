using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class FieldConfig : EntityTypeConfiguration<Field>
	{
		public FieldConfig( )
		{
			HasKey( x => x.Id );

			HasRequired( x => x.TableTab )
				.WithMany( x => x.Fields )
				.HasForeignKey( x => x.TableTabId );
		}
	}
}