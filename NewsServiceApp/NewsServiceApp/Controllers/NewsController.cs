using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsServiceApp.Repository;
using NewsServiceApp.Models;
using NewsServiceApp.Dto;

namespace NewsServiceApp.Controllers
{
    [Route("api/[controller]")]
    public class  NewsController : Controller
    {

        private INewsRepository newsrepository;

        public NewsController(INewsRepository _newsrepository)
        {
            newsrepository = _newsrepository;
        }

        //Need to delete
        [HttpGet]
        public IEnumerable<News> GetAll()
        {
            return newsrepository.GetAll();
        }

        [HttpGet("daily")]
        public IQueryable<News> GetAllDailyNews()
        {
            return newsrepository.GetAllDailyNews();
        }

        [HttpGet("important")]
        public IQueryable<News> GetAllImportantNews()
        {
            return newsrepository.GetAllImportantNews();
        }

        [HttpGet("{id}")]
        public NewsDto Get(int id)
        {
            return newsrepository.FindById(id);
        }

        [HttpPost]
        public void Post([FromBody]News news)
        {
            newsrepository.Add(news);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]News news)
        {
            news.id = id;
            if (ModelState.IsValid)
                newsrepository.Update(news);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
