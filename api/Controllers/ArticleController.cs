using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
         // GET api/values
        [HttpGet]
        public string Get()
        {
            string tmp = string.Empty;
            int c = 0;

            using(var db = new LiteDatabase(@"NewsNumbers.db"))
            {
                // Get collection
                var col = db.GetCollection<Article>("articles");

                col.DropIndex("Title");
                
                // Create your new instance
                var article = new Article
                { 
                    Body = "This is the body",
                    Title = "The Title",
                    CategoryId = 0,
                    ImageURL = "http://image.com",
                    SourceURL = "http://source.som",
                    DateStamp = DateTime.Now,
                    Id = Guid.NewGuid()                   
                };


                col.EnsureIndex(x => x.Id, true);

                col.Insert(article);

                article.Title = "The New Title";
                
                col.Update(article);
                
                var results = col.Find(x => x.CategoryId  == 0);
                tmp = results.FirstOrDefault().Title;

                c = col.Count();
            }

            return c.ToString();
        }

    }

}