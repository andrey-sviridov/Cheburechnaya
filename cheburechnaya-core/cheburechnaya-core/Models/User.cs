namespace cheburechnaya_core.Models {
    public class User {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Post> PostLiked { get; set; }

    }
}
