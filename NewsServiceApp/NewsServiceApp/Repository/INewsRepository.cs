using NewsServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Repository
{
    public interface INewsRepository
    {
        News FindById(int id);
        IEnumerable<News> GetAll();

        IQueryable<News> GetAllDailyNews();

        News Add(News news);
        News Update(News news);
        void Delete(int id);
    }
}
