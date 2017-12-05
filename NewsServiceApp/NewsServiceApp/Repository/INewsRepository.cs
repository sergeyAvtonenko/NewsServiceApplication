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
        IEnumerable<News> GetAll();             //Need to delete

        NewsDto FindById(int id);               //ok
        IQueryable<News> GetAllDailyNews();     //ok
        IQueryable<News> GetAllImportantNews(); //ok
        void Add(News news);                    //ok
        void Update(News news);

        void Delete(int id);
    }
}
