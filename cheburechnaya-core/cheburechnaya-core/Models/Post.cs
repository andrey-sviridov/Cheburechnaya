using System.ComponentModel.DataAnnotations;

namespace cheburechnaya_core.Models {
    public class Post {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        public bool Liked { get; set; }
    }
}
