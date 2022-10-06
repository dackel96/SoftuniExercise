using BasicForumApp.Data.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace BasicForumApp.Models
{
    public class PostViewModel
    {
        private const int titleMin = PostConstants.TitleMinLength;

        private const int contentMin = PostConstants.ContentMinLength;
        public int Id { get; init; }

        [Required]
        [MinLength(titleMin)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(contentMin)]
        public string Content { get; set; } = null!;
    }
}
