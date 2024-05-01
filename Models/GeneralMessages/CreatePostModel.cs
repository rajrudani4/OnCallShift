using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.GeneralMessages
{
    public class CreatePostModel
    {
            public int PostId { get; set; }

            [Required]
            public string Role { get; set; }

            [Required]
            public string Desc { get; set; }

            public int PayPerHour { get; set; }

            public int AreaCode { get; set; }

            public bool IsMyPosts { get; set; } = false;
    }
}
