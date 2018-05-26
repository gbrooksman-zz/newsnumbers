using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using api.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using api.Managers;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
         private IMemoryCache _cache;
         private ArticleManager articleMgr;

        public ArticleController(IMemoryCache memoryCache, IOptions<ApiSettings> settings)
        {
            _cache = memoryCache;
            articleMgr = new ArticleManager(_cache,settings);
        }

         [HttpGet]
         public List<Article> GetByCategory(int categoryid)
         {
            return articleMgr.GetCategory(categoryid);
         }

 
        [HttpGet]
        public List<Article> GetLatest()
        {            
           return articleMgr.GetLatest();
        }

        // [HttpGet]
        // public List<Article> GetRecent()
        // {
        //    return articleMgr.GetRecent();
        // }

    }

}