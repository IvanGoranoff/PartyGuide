using PartyGuide.DataAccess.Data;
using PartyGuide.Domain.Models;

namespace PartyGuide.Domain.Adapters
{
	public class AdapterDomain
	{
		public ServiceTable TransformModelToTable(ServiceModel model)
		{
			return new ServiceTable
			{
				Id = model.Id,
				Category = model.Category,
				Description = model.Description,
				ExtendedDescription = model.ExtendedDescription,
				EndPriceRange = model.EndPriceRange,
				Image = model.Image,
				Location = model.Location,
				PhoneNumber = model.PhoneNumber,
				StartPriceRange = model.StartPriceRange,
				Title = model.Title,
				CreatedBy = model.CreatedBy
			};
		}

		public ServiceModel TransformTableToModel(ServiceTable table)
		{
			return new ServiceModel
			{
				Id = table.Id,
				Category = table.Category,
				Description = table.Description,
				ExtendedDescription = table.ExtendedDescription,
				EndPriceRange = table.EndPriceRange,
				Image = table.Image,
				Location = table.Location,
				PhoneNumber = table.PhoneNumber,
				StartPriceRange = table.StartPriceRange,
				Title = table.Title,
				CreatedBy = table.CreatedBy,
				Ratings = table.Ratings
			};
		}

		public List<ServiceModel> TransformTablesToModelsList(List<ServiceTable> tables)
		{
			var models = tables.Select(table => new ServiceModel()
			{

				Id = table.Id,
				Category = table.Category,
				Description = table.Description,
				ExtendedDescription = table.ExtendedDescription,
				EndPriceRange = table.EndPriceRange,
				Image = table.Image,
				Location = table.Location,
				PhoneNumber = table.PhoneNumber,
				StartPriceRange = table.StartPriceRange,
				Title = table.Title,
				CreatedBy = table.CreatedBy,
				Ratings = table.Ratings
			});

			return models.ToList();
		}

	}
}
