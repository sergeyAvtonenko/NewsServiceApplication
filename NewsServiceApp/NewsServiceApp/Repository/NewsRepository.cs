using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServiceApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace NewsServiceApp.Repository
{
    public class NewsRepository : INewsRepository
    {

        private IConfiguration configuration;

        public NewsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<News> GetAll()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News>("SELECT * FROM news");
            }
        }

        public News FindById(int id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News>("SELECT * FROM news WHERE id = @Id", new {Id = id}).FirstOrDefault();
            }
        }

        public IQueryable<News> GetAllDailyNews()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News, Category, News>(@"SELECT n.id, n.news_heading, n.news_text, n.date_create, n.date_update,
                                                          c.id, c.name
                                                          FROM news n 
                                                          INNER JOIN category c 
                                                          ON c.id = n.news_category_id 
                                                          WHERE c.name = 'daily'", (n, c) =>
                                                          {
                                                              n.Category = c;
                                                              return n;
                                                          },
                                                          splitOn: "id").AsQueryable();
            }
        }


        public News Add(News news)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public News Update(News news)
        {
            throw new NotImplementedException();
        }
    }
}
