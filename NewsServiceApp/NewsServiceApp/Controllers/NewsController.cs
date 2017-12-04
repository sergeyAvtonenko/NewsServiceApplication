﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsServiceApp.Repository;
using NewsServiceApp.Models;

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

        // GET api/news
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

        // GET api/news/5
        [HttpGet("{id}")]
        public News Get(int id)
        {
            return newsrepository.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
