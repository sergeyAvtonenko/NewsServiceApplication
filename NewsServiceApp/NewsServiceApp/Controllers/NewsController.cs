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

        [HttpGet("daily")]
        public async Task<ActionResult> GetAllDailyNews()
        {
            var result = await newsrepository.GetAllDailyNews();
            return Ok(result);
        }

        [HttpGet("important")]
        public async Task<ActionResult> GetAllImportantNews()
        {
            var result = await newsrepository.GetAllImportantNews();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await newsrepository.FindById(id);
            return Ok(result);
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

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            newsrepository.Delete(id);
        }
    }
}
