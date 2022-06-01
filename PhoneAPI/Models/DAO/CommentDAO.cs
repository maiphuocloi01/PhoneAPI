using PhoneAPI.Models.DTO;
using PhoneAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhoneAPI.Models.DAO
{
    public class CommentDAO
    {
        private static CommentDAO instance;

        public static CommentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();

        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();

        public async Task<List<CommentDTO>> GetAllComment()
        {
            var resultList = (await db.Comments
                        .ToListAsync())
                        .Select(comment => new CommentDTO(comment))
                        .ToList();

            return resultList;
        }

        public async Task<int> AddComment(CommentDTO commentDTO)
        {
            var comment = new Comment()
            {
                ProductId = commentDTO.ProductId,
                AccountId = commentDTO.AccountId,
                FullName = commentDTO.FullName,
                TypeProduct = commentDTO.TypeProduct,
                CreateAt = commentDTO.CreateAt,
                Content = commentDTO.Content,
                Rating = commentDTO.Rating,
                IsDelete = false
            };

            Account account = db.Accounts.SingleOrDefault(c => c.Id == comment.AccountId);
            comment.Account = account;

            Product product = db.Products.SingleOrDefault(p => p.Id == comment.ProductId);
            product.Comments.Add(comment);
            db.Entry(product).State = EntityState.Modified;
            

            db.Entry(comment).State = EntityState.Added;

            try
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return comment.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

       /* public async Task<bool> UpdateComment(CommentDTO commentDTO)
        {
            var result = db.Comments.SingleOrDefault(c => c.Id == commentDTO.Id);

            db.Entry(result).State = EntityState.Modified;

            try
            {
                result.Content = commentDTO.Content;
                result.Rating = commentDTO.Rating;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }*/

        public async Task<bool> DeleteComment(int Id)
        {
            var result = db.Comments.SingleOrDefault(c => c.Id == Id);

            db.Entry(result).State = EntityState.Deleted;

            try
            {
                db.Comments.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<List<CommentDTO>> GetCommentByProductID(int Id)
        {
            var resultList = (await db.Comments
                .ToListAsync())
                .Select(c => new CommentDTO(c))
                .OrderByDescending(s => s.Id)
                .ToList();
            resultList = resultList.FindAll(c => c.ProductId == Id);
            return resultList;
        }

        public async Task<List<CommentDTO>> GetCommentByAccountID(int Id)
        {
            var resultList = (await db.Comments
                .ToListAsync())
                .Select(c => new CommentDTO(c))
                .ToList();
            resultList = resultList.FindAll(c => c.AccountId == Id);
            return resultList;
        }
    }
}