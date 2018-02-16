using NewsServiceApp.Dto;
using NewsServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Service
{
    public interface INewsService
    {        
        //Task<List<NewsDto>> GetAllDailyNews();
        //Task<List<NewsDto>> GetAllImportantNews();
        Task<List<NewsDto>> GetNewsPageAsync(int skip, int take, int news_category_id);
        Task<NewsDto> FindById(int id);
        Task<News> Add(News news);
        Task<bool> Update(News news);
        Task<bool> Delete(int id);
    }
}
