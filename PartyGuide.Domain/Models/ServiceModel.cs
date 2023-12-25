using PartyGuide.DataAccess.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PartyGuide.Domain.Models
{
	public class ServiceModel
	{
		public int Id { get; set; }

		public string Category { get; set; }

		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{5,40}$", ErrorMessage = "Title must be between 5 and 40 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{5,300}$", ErrorMessage = "Description must be between 5 and 300 characters")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{40,300}$", ErrorMessage = "Extended must be between 40 and 300 characters")]
		public string ExtendedDescription { get; set; }

		public byte[]? Image { get; set; }


		[Required(ErrorMessage = "Field Required")]
		[RegularExpression(@"^.{8,15}$", ErrorMessage = "PhoneNumber must be between 8 and 15 characters")]
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

		public string? Location { get; set; }

		public string? CreatedBy { get; set; }

		public ICollection<RatingTable>? Ratings { get; set; }
	}
}
