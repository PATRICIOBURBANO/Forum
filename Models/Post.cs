using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class Post
    {

        public string Title { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Description { get; set; }
    }
}
