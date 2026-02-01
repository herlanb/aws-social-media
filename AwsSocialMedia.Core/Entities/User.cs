namespace AwsSocialMedia.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public partial class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}