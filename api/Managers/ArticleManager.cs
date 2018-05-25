using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using api.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace api.Managers
{

    public class ArticleManager : BaseManager
    {
        private IMemoryCache _cache;
        public ArticleManager(IMemoryCache memoryCache, IOptions<ApiSettings> settings) 
                                    : base(memoryCache, settings)
        {
            _cache = memoryCache;
        }

        public List<Article> GetCategory(int categoryid)
         {
            List<Article> articles = new  List<Article> ();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var col = db.GetCollection<Article>("articles");

                articles = col.Find(a => a.CategoryId == categoryid 
                                    && a.DateStamp > DateTime.Now.AddYears(-1)).ToList();
            }

            return articles;
         }

        public List<Article> GetRecent()
         {
            List<Article> articles = new  List<Article> ();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var col = db.GetCollection<Article>("articles");

                articles = col.Find(a => a.DateStamp > DateTime.Now.AddDays(-7)).ToList();
            }

            return articles;
         }


        public Article GetOne(Guid guid)
         {
            Article article = new  Article();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var col = db.GetCollection<Article>("articles");

                article = col.Find(a => a.Id == guid 
                                    && a.DateStamp > DateTime.Now.AddYears(-1)).FirstOrDefault();
            }

            return article;
         }
    }
}