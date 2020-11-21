namespace Peoples.Dal.Models
{
	public class IdentificationNumberPopiMetadataMap
	{
		public int Id { get; set; }
		
		public int IdentificationNumberId { get; set; }

		public int PopiMetadataId { get; set; }

		public virtual IdentificationNumber IdentificationNumber { get; set; }
		
		public virtual PopiMetadata PopiMetadata { get; set; }
	}
}