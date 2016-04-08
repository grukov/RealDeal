namespace MvcTemplate.Web.ViewModels.RealEstate
{
    using AutoMapper;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class RealEstateViewModel : IMapFrom<MvcTemplate.Data.Models.RealEstate>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public double Size { get; set; }

        public double Price { get; set; }

        public string City { get; set; }

        public string Hood { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public EstateType EstateType { get; set; }

        public LettingType LettingType { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<RealEstate, RealEstateViewModel>()
                .ForMember(x => x.Likes, opt => opt.MapFrom(x => x.Likes.Count));

            configuration.CreateMap<RealEstate, RealEstateViewModel>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Count));
        }
    }
}