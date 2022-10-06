namespace BasicForumApp.Data.Entities
{
    using BasicForumApp.Data.DataConstants;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        private const int titleMax = PostConstants.TitleMaxLength;

        private const int contentMax = PostConstants.ContentMaxLength;

        [Key]
        public int Id { get; init; }

        [Required]
        [StringLength(titleMax)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(contentMax)]
        public string Content { get; set; } = null!;
    }
}
