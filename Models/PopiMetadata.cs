﻿using System.Collections.Generic;

namespace Peoples.Dal.Models
{
	public class PopiMetadata
	{
		public int Id { get; set; }

		public string DatastoreName { get; set; }

		public string DatastoreType { get; set; }

		public string FieldType { get; set; }

		public string Category { get; set; }

		public virtual ICollection<IdentificationNumberPopiMetadataMap> IdentificationNumberPopiMetadataMaps { get; set; }
	}
}