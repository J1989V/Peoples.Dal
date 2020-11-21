using System.Collections.Generic;

namespace Peoples.Dal.Models
{
	public class IdentificationNumber
	{
		public int Id { get; set; }

		public string SaltedIdentificationNumber { get; set; }

		public virtual ICollection<IdentificationNumberPopiMetadataMap> IdentificationNumberPopiMetadataMaps { get; set; }
	}
}