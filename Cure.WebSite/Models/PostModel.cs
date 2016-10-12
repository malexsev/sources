using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.IO;
    using DataAccess;
    using Utils;

    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public int? ParentPostId { get; set; }
        public Guid GuidId { get; set; }
        public Guid ChildId { get; set; }
        public string OwnerUser { get; set; }
        public string AnserToUser { get; set; }
        public string CopyOwnerName { get; set; }
        public string CopyOwnerLocation { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEdit { get; set; }
        public int CommentCount { get; set; }
        public List<PostModel> Comments { get; set; }
        public string ChildAvaUrl { get; set; }
        public string UserpicUrl { get; set; }
        public string ChildName { get; set; }

        public PostModel(Post post)
        {
            this.Comments = new List<PostModel>();
            this.Id = post.Id;
            this.ParentPostId = post.ParentPostId;
            this.GuidId = post.GuidId;
            this.OwnerUser = post.OwnerUser;
            this.AnserToUser = post.AnserToUser;
            this.CopyOwnerName = post.CopyOwnerName;
            this.CopyOwnerLocation = post.CopyOwnerLocation;
            this.Subject = post.Subject;
            this.Text = post.Text;
            this.CreateDate = post.CreateDate;
            this.LastEdit = post.LastEdit;
            this.ChildName = post.Child.Name;
            post.Post1.ToList().ForEach(x => this.Comments.Add(new PostModel(x)));
            this.UserpicUrl =
                SiteUtils.GetUserUserpic(
                    Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], post.Child.GuidId.ToString())
                        .Replace("Upload", "Userpics"),
                    post.Child.GuidId,
                    post.Child.OwnerUserPic);

            string avaFile = (post.Child.ChildAvaFiles.FirstOrDefault() == null || post.Child.ChildAvaFiles.FirstOrDefault().Id == 0)
                    ? string.Empty
                    : post.Child.ChildAvaFiles.FirstOrDefault().FileName;
            if (!string.IsNullOrEmpty(avaFile))
            {
                this.ChildAvaUrl = Path.Combine(Path.Combine(Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], post.Child.GuidId.ToString()), "Thumb"), avaFile);
            }
            else
            {
                this.ChildAvaUrl = string.Format("/Content/img/userpics/no_photo_{0}.jpg",
                    SiteUtils.GetRandom(post.Child.Id, 3));
            }

        }
    }
}