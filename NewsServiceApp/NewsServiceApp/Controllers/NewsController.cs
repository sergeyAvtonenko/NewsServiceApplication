using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsServiceApp.Repository;
using NewsServiceApp.Models;
using NewsServiceApp.Service;
using System.Collections.Generic;
using System;

namespace NewsServiceApp.Controllers
{
    [Route("api/[controller]")]
    public class  NewsController : Controller
    {
        private INewsService newsService;

        public NewsController(INewsService _newsService)
        {
            this.newsService = _newsService;
        }

        // GET api/news/{id}
        [HttpGet("{id}", Name = "GetNewsRoute")]
        [ProducesResponseType(typeof(News), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Get(int id)
        {            
            try
            {
                var result = await newsService.FindById(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        // POST api/news
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Add([FromBody]News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState});
            }

            try
            {
                var addedNews = await newsService.Add(news);
                if (news == null)
                {
                    return BadRequest(new ApiResponse { Status = false , ModelState = ModelState});
                }
                return CreatedAtRoute("GetNewsRoute", new { id = news.id },
                        new ApiResponse { Status = true, News = news });
            }
            catch 
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        // PUT api/news/{id}
        [HttpPut("{id}")]

        public async Task<ActionResult> Update(int id, [FromBody]News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var status = await newsService.Update(news);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, News = news });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        // DELETE api/news/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Delete(int id)
        {            
            try
            {
                var status = await newsService.Delete(id); ;
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        // GET api/news/page/{skip}/{take}/{news_category_id}
        [HttpGet("page/{skip}/{take}/{news_category_id}")]
        [ProducesResponseType(typeof(List<News>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> NewsPage(int skip, int take, int news_category_id)
        {
            try
            {
                var pagingResult = await newsService.GetNewsPageAsync(skip, take, news_category_id);
                return Ok(pagingResult);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}
