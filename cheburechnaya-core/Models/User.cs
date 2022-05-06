﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace cheburechnaya_core.Models {
    public class User {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual List<LikeInformation> PostLiked { get; set; }

    }
}
