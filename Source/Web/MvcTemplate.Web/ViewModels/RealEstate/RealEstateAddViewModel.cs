namespace MvcTemplate.Web.ViewModels.RealEstate
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class RealEstateAddViewModel : IMapTo<MvcTemplate.Data.Models.RealEstate>
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The lenght is too long")]
        public string Title { get; set; }

        [AllowHtml]
        [MinLength(10)]
        [MaxLength(2000)]
        [Required]
        public string Content { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Hood { get; set; }

        [Required]
        public double Size { get; set; }

        [Required]
        public double Price { get; set; }

        public string UserId { get; set; }

        [Required]
        [Range(1, 11, ErrorMessage = "Please choose the type of the real estate!")]
        public EstateType EstateType { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Please choose the letting type!")]
        public LettingType LettingType { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Please choose furnished state!")]
        public FurnishedState FurnishedState { get; set; }
    }
}