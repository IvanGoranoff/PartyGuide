using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyGuide.DataAccess.Data
{
    [Table("ServiceTable")]
    public class ServiceTable
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CATEGORY")]
        public string Category { get; set; }

        [Column("TITLE")]
        public string Title { get; set; }

		[Column("DESCRIPTION")]
		public string Description { get; set; }

		[Column("EXTENDED_DESCRIPTION")]
		public string ExtendedDescription { get; set; }

		[Column("IMAGE")]
        public byte[] Image { get; set; }

        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [Column("START_PRICE_RANGE")]
        public int? StartPriceRange { get; set; }

        [Column("END_PRICE_RANGE")]
        public int? EndPriceRange { get; set; }

        [Column("LOCATION")]
        public string Location { get; set; }

		[Column("CREATED_BY")]
		public string CreatedBy { get; set; }

		public ICollection<RatingTable> Ratings { get; } = new List<RatingTable>();
	}
}
