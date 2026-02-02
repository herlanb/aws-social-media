namespace AwsSocialMedia.Core.Entities
{
    using System;
    using System.Text.Json.Serialization;

    public partial class Comment : BaseEntity
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}