using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Post> GetMyPosts(int childId)
        {
            return context.Posts.Where(x => x.ChildId == childId).OrderByDescending(x => x.CreateDate);
        }

        public IEnumerable<Post> GetPosts()
        {
            return context.Posts.OrderByDescending(x => x.CreateDate);
        }

        public Post GetPost(int postId)
        {
            return context.Posts.FirstOrDefault(o => o.Id == postId);
        }

        public void InsertPost(Post post)
        {
            try
            {
                post.GuidId = Guid.NewGuid();
                context.Posts.AddObject(post);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePost(Post post)
        {
            try
            {
                context.Posts.Attach(post);
                context.Posts.DeleteObject(post);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePost(Post post)
        {
            try
            {
                var origPost = GetPost(post.Id);
                origPost.AnserToUser = post.AnserToUser;
                origPost.CopyOwnerLocation = post.CopyOwnerLocation;
                origPost.CopyOwnerName = post.CopyOwnerName;
                origPost.CreateDate = post.CreateDate;
                origPost.GuidId = post.GuidId;
                origPost.LastEdit = post.LastEdit;
                origPost.OwnerUser = post.OwnerUser;
                origPost.ParentPostId = post.ParentPostId;
                origPost.Subject = post.Subject;
                origPost.Text = post.Text;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
