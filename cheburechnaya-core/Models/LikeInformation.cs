using cheburechnaya_core.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models{
    public class LikeInformation{
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
