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
        Task<NewsDto> FindById(int id);
        Task<IEnumerable<News>> GetAllDailyNews();
        Task<IEnumerable<News>> GetAllImportantNews(); 
        void Add(News news);                    
        void Update(News news);
        void Delete(int id);
    }
}
