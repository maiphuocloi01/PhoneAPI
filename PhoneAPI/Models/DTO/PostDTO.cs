using PhoneAPI.Assets.Contain;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class PostDTO
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public PostDTO()
        {

        }

        public PostDTO(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Link = post.Link;
            Image = Const.PostImagePath + post.Image;
        }
    }
}