using cheburechnaya_core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models {
    public class Post : BaseEntity {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        public PostType Type { get; set; }
        public  List<User> LikedUsers { get; set; }
        public  User? Author { get; set; }
        public bool IsEdited { get; set; }
    }

    public enum PostType {
        Verse = 1,
        Story = 2,
        Other = 3
    }
}
