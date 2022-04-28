using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneAPI.Models.DTO
{
    public class CommentDTO
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public int? AccountId { get; set; }

        public int? Rating  { get; set; }

        public string Content { get; set; }

        public bool? IsDelete { get; set; }
      
        public CommentDTO()
        {

        }

        public CommentDTO(Comment comment)
        {
            Id = comment.Id;
            ProductId = comment.ProductId;
            AccountId = comment.AccountId;
            Rating = comment.Rating;
            Content = comment.Content;
            IsDelete = comment.IsDelete;
        }
    }
}