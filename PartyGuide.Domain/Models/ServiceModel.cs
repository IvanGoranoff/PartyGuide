using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PartyGuide.Domain.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Title is to short")]
        public string Title { get; set; }

		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{5,}$", ErrorMessage = "Description is to short")]
		public string Description { get; set; }

        public byte[]? Image { get; set; }


		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{8,}$", ErrorMessage = "PhoneNumber is to short")]
		public string PhoneNumber { get; set; }

		[DisplayName("Start Price Range")]
		[Required(ErrorMessage = "Field Required")]
		[Range(10, 1000,
		ErrorMessage = "Value for {0} must be between {1} and {2}.")]
		public int? StartPriceRange { get; set; }

		[DisplayName("End Price Range")]
		[Required(ErrorMessage = "Field Required")]
		[Range(10, 1000,
		ErrorMessage = "Value for {0} must be between {1} and {2}.")]
		public int? EndPriceRange { get; set; }


		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{5,}$", ErrorMessage = "Location is to short")]
		public string Location { get; set; }
    }
}
