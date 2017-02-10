namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<Post> GetMyPosts(int childId)
        {
            return dataRepository.GetMyPosts(childId);
        }

        public IEnumerable<Post> GetPosts()
        {
            return dataRepository.GetPosts();
        }

        public IEnumerable<Post> GetPostsByOwner(string ownerUser)
        {
            return dataRepository.GetPostsByOwner(ownerUser);
        }

        public Post GetPost(int postId)
        {
            return dataRepository.GetPost(postId);
        }

        public void InsertPost(Post post)
        {
            try
            {
                dataRepository.InsertPost(post);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePost(Post post)
        {
            try
            {
                dataRepository.DeletePost(post);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePost(Post post)
        {
            try
            {
                dataRepository.UpdatePost(post);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
