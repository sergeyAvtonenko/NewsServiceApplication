using NewsServiceApp.Dto;
using NewsServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Repository
{
    public interface INewsRepository
    {
        //Task<IEnumerable<News>> GetAllDailyNews();
        //Task<IEnumerable<News>> GetAllImportantNews();
        //Task<IEnumerable<News>> GetNewsByCategory(string _category);

        Task<News> FindById(int id);
        Task<IEnumerable<News>> GetNewsPageAsync(int skip, int take, int news_category_id);
        Task<News> Add(News news);
        Task<bool> Update(News news);
        Task<bool> Delete(int id);
    }
}
