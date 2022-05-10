using AutoMapper;
using cheburechnaya_core.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models{
    public class LikeInformation{
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int UserId { get; set; }
    }

    public abstract class LikeInformationEntity {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }
        public int UserId { get; set; }
    }
    public class LikeInformationProfile : Profile {
        public LikeInformationProfile() {

            CreateMap<LikeInformation, LikeInformationEntity>().ReverseMap();

        }

    }

}
