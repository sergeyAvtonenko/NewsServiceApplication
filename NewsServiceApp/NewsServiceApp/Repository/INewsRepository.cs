using NewsServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Repository
{
    public interface INewsRepository
    {
        News Find(int id);
        IEnumerable<News> GetAll();

        News Add(News news);
        News Update(News news);
        void Delete(int id);
    }
}
