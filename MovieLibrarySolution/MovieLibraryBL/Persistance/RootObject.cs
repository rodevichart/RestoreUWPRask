﻿using System.Runtime.Serialization;

namespace MovieLibraryBL.Persistance
{
	[DataContract]
	public class RootObject
	{
		[DataMember(Name = "unit")]
		public int Unit { get; set; }


		[DataMember(Name = "show_id")]
		public int ShowId { get; set; }


		[DataMember(Name = "show_title")]
		public string Title { get; set; }
		

		[DataMember(Name = "release_year")]
		public string ReleaseYear { get; set; }


		[DataMember(Name = "rating")]
		public string Rating { get; set; }


		[DataMember(Name = "category")]
		public string Category { get; set; }


		[DataMember(Name = "show_cast")]
		public string ShowCast { get; set; }


		[DataMember(Name = "director")]
		public string Director { get; set; }


		[DataMember(Name = "summary")]
		public string Summary { get; set; }


		[DataMember(Name = "poster")]
		public string Poster { get; set; }


		[DataMember(Name = "mediatype")]
		public int MediaType { get; set; }

		[DataMember(Name = "runtime")]
		public string Duration { get; set; }


	}
}