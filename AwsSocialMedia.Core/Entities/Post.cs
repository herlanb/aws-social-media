namespace AwsSocialMedia.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public partial class Post
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}