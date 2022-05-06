using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models {
    public class Post {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        public virtual List<LikeInformation> UserLiked { get; set; }
    }
}
