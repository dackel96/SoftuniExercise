using BasicForumApp.Data.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace BasicForumApp.Models
{
    public class PostFormModel
    {
        private const int titleMin = PostConstants.TitleMinLength;

        private const int titleMax = PostConstants.TitleMaxLength;

        private const int contentMin = PostConstants.ContentMinLength;

        private const int contentMax = PostConstants.ContentMaxLength;

        [Required]
        [StringLength(titleMax, MinimumLength = titleMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(contentMax, MinimumLength = contentMin)]
        public string Content { get; set; } = null!;
    }
}
