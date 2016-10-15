using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess;
    using DataAccess.BLL;
    using Models;
    using Utils;

    public class PostController : Controller
    {
        [HttpPost]
        public PartialViewResult UpdatePost(string text, string postid)
        {
            var dal = new DataAccessBL();
            int postId = SiteUtils.ParseInt(postid, 0);
            var post = dal.GetPost(postId);
            int childId = GetParentChildId(post);

            post.Text = text;
            post.LastEdit = DateTime.Now;

            dal.UpdatePost(post);

            var view = dal.ViewChild(childId);
            return PartialView("_PostItems", new ChildVisualDetailed(view, new List<ChildHideFile>(), null, dal.GetMyPosts(view.Id)));
        }

        [HttpPost]
        public PartialViewResult NewPost(string text, string answerForPost)
        {
            var dal = new DataAccessBL();
            var view = dal.ViewChild(User.Identity.Name);
            var post = new Post()
            {
                ChildId = view.Id,
                CopyOwnerLocation = string.Format("{0}{1}{2}", view.CountryName, (string.IsNullOrEmpty(view.Region) ? "" : ", "), view.Region),
                CopyOwnerName = view.ContactName,
                ParentPostId = null,
                GuidId = Guid.NewGuid(),
                OwnerUser = User.Identity.Name,
                CreateDate = DateTime.Now,
                LastEdit = DateTime.Now,
                Subject = string.Empty,
                Text = text
            };

            int postForId = SiteUtils.ParseInt(answerForPost, 0);
            if (postForId != 0)
            {
                var postFor = dal.GetPost(postForId);
                post.AnserToUser = postFor.OwnerUser;
                post.ParentPostId = postFor.Id;
            }

            dal.InsertPost(post);
            int childId = GetParentChildId(post);
            view = dal.ViewChild(childId);

            return PartialView("_PostItems", new ChildVisualDetailed(view, new List<ChildHideFile>(), null, dal.GetMyPosts(childId)));
        }

        [HttpPost]
        public PartialViewResult DeletePost(int post)
        {
            int postId = SiteUtils.ParseInt(post, 0);
            var dal = new DataAccessBL();
            var delPost = dal.GetPost(postId);
            int childId = GetParentChildId(delPost);

            dal.DeletePost(delPost);

            return PartialView("_PostItems",
                new ChildVisualDetailed(dal.ViewChild(childId), dal.GetChildHideFiles(childId), dal.GetChildAvaFile(childId), dal.GetMyPosts(childId)));
        }

        private int GetParentChildId(Post post)
        {
            int ret = post.ChildId;
            Post mainPost = post.Post2;
            while (mainPost != null)
            {
                ret = mainPost.ChildId;
                mainPost = mainPost.Post2;
            }
            return ret;
        }

        //[HttpPost]
        //public PartialViewResult More(string filter, string skiprecords)
        //{
        //    var dal = new DataAccessBL();
        //    int filterId = SiteUtils.ParseInt(filter, 0);
        //    int skipRecords = SiteUtils.ParseInt(skiprecords, 0);

        //    ViewBag.MensionsCount = dal.CountMensions(filterId);
        //    ViewBag.ViewMensions = dal.ViewMensions(filterId, skipRecords).ToList();

        //    return PartialView("_Mensions");
        //}
    }
}