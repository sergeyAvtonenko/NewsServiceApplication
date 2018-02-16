using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServiceApp.Dto;
using NewsServiceApp.Models;
using NewsServiceApp.Repository;

namespace NewsServiceApp.Service
{
    public class NewsService : INewsService
    {

        private INewsRepository newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<NewsDto> FindById(int id)
        {
            var dbResult = await newsRepository.FindById(id);
            return new NewsDto(dbResult.id, dbResult.news_heading, dbResult.news_text, dbResult.date_create, dbResult.date_update, dbResult.news_category_id);
        }

        public async Task<News> Add(News news)
        {
            return await newsRepository.Add(news);
        }

        public async Task<bool> Update(News news)
        {
            return await newsRepository.Update(news);
        }

        public async Task<bool> Delete(int id)
        {
            return await newsRepository.Delete(id);
        }

        public async Task<List<NewsDto>> GetNewsPageAsync(int skip, int take, int news_category_id)
        {
            var dbResult = await newsRepository.GetNewsPageAsync(skip, take, news_category_id);
            dbResult.ToList();
            List<NewsDto> newsDtoList = new List<NewsDto>();
            foreach (var item in dbResult)
            {
                newsDtoList.Add(new NewsDto(item.id, item.news_heading, item.news_text, item.date_create, item.date_update, item.news_category_id));
            }
            return newsDtoList;
        }

        //public async Task<List<NewsDto>> GetAllDailyNews()
        //{
        //    var dbResult = await newsRepository.GetAllDailyNews();
        //    dbResult.ToList();
        //    List<NewsDto> newsDtoList = new List<NewsDto>();
        //    foreach(var item in dbResult)
        //    {
        //        newsDtoList.Add(new NewsDto(item.id, item.news_heading, item.news_text, item.date_create, item.date_update, item.news_category_id));                
        //    }
        //    return newsDtoList;
        //}

        //public async Task<List<NewsDto>> GetAllImportantNews()
        //{
        //    var dbResult = await newsRepository.GetAllImportantNews();
        //    dbResult.ToList();
        //    List<NewsDto> newsDtoList = new List<NewsDto>();
        //    foreach (var item in dbResult)
        //    {
        //        newsDtoList.Add(new NewsDto(item.id, item.news_heading, item.news_text, item.date_create, item.date_update, item.news_category_id));
        //    }
        //    return newsDtoList;
        //}              
    }
}
