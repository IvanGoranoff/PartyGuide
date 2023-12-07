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

        [Column("IMAGE")]
        public byte[] Image { get; set; }

        [Column("PHONENUMBER")]
        public string PhoneNumber { get; set; }

        [Column("STARTPRICERANGE")]
        public int? StartPriceRange { get; set; }

        [Column("ENDPRICERANGE")]
        public int? EndPriceRange { get; set; }

        [Column("LOCATION")]
        public string Location { get; set; }
    }
}
