using cheburechnaya_core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models {
    public class Post : BaseEntity {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        public  List<User> Users { get; set; }
        //public User Author { get; set; }
    }
}
