﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public NewsPage GetNewsPage(int id)
        {
            return context.NewsPages.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<NewsPage> GetNewsPages()
        {
            return context.NewsPages.OrderBy(o => o.Name).ToList();
        }

        public NewsPage GetNewsPage(string name)
        {
            return context.NewsPages.FirstOrDefault(o => o.Name == name);
        }

        public void InsertNewsPage(NewsPage newsPage)
        {
            try
            {
                context.NewsPages.AddObject(newsPage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNewsPage(NewsPage newsPage)
        {
            try
            {
                context.NewsPages.Attach(newsPage);
                context.NewsPages.DeleteObject(newsPage);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNewsPage(NewsPage newsPage)
        {
            try
            {
                var origNewsPage = GetNewsPage(newsPage.Name);
                origNewsPage.Alias = newsPage.Alias;
                origNewsPage.CreateDate = newsPage.CreateDate;
                origNewsPage.CreatorName = newsPage.CreatorName;
                origNewsPage.Date = newsPage.Date;
                origNewsPage.EditDate = newsPage.EditDate;
                origNewsPage.GuidId = newsPage.GuidId;
                origNewsPage.LastUser = newsPage.LastUser;
                origNewsPage.Name = newsPage.Name;
                origNewsPage.Settings = newsPage.Settings;
                origNewsPage.Subject = newsPage.Subject;
                origNewsPage.Text = newsPage.Text;
                origNewsPage.IsActive = newsPage.IsActive;
                origNewsPage.Sort = newsPage.Sort;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}