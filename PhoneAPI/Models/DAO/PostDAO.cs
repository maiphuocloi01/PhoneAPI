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
    public class PostDAO
    {
        private static PostDAO instance;

        public static PostDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PostDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        //PhoneStoreEntities db = new PhoneStoreEntities();
        //PhoneStoreEntities1 db = new PhoneStoreEntities1();
        PhoneStoreEntities3 db = new PhoneStoreEntities3();

        public async Task<List<PostDTO>> GetAllPost()
        {
            return (await db.Posts
                        .ToListAsync())
                        .Select(advertisement => new PostDTO(advertisement))
                        .ToList();
        }

        public async Task<int> AddPost(PostDTO postDTO)
        {
            var post = new Post()
            {
                Title = postDTO.Title,
                Link = postDTO.Link,
                Image = postDTO.Image
            };

            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return post.Id;
        }

        public async Task<bool> UpdatePost(PostDTO postDTO)
        {
            var result = db.Posts.SingleOrDefault(p => p.Id == postDTO.Id);

            try
            {
                result.Title = postDTO.Title;
                result.Image = postDTO.Image;
                result.Link = postDTO.Link;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeletePost(int Id)
        {
            var result = db.Posts.SingleOrDefault(p => p.Id == Id);

            try
            {
                db.Posts.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}