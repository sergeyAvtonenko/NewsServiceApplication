using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServiceApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NewsServiceApp.Dto;

namespace NewsServiceApp.Repository
{
    public class NewsRepository : INewsRepository
    {

        private IConfiguration configuration;

        public NewsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //Need to delete this method 
        public IEnumerable<News> GetAll()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News>("SELECT * FROM news");
            }
        }
        //Find by Id, return Dto, maybe need to change return
        public NewsDto FindById(int id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = db.Query<News>("SELECT * FROM news WHERE id = @Id", new { Id = id }).FirstOrDefault();
                
                return new NewsDto() { id = qr.id, date_create = qr.date_create, date_update = qr.date_update, news_heading = qr.news_heading, news_text = qr.news_text, news_category_id = qr.news_category_id };
            }
        }

        //Need to Return dto
        public IQueryable<News> GetAllDailyNews()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News, Category, News>(@"SELECT n.news_heading, n.news_text, n.date_create, n.date_update, c.id, c.name 
                                                        FROM news n 
                                                        INNER JOIN category c 
                                                        ON c.id = n.news_category_id 
                                                        WHERE c.name = 'daily'
                                                        ORDER BY n.date_create DESC", 
                                                        (n, c) =>
                                                          {
                                                              n.Category = c;
                                                              return n;
                                                          },
                                                        splitOn:"id").AsQueryable();
            }
        }

        public IQueryable<News> GetAllImportantNews()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News, Category, News>(@"SELECT n.news_heading, n.news_text, n.date_create, n.date_update, c.id, c.name 
                                                        FROM news n 
                                                        INNER JOIN category c 
                                                        ON c.id = n.news_category_id 
                                                        WHERE c.name = 'important'
                                                        ORDER BY n.date_create DESC",
                                                        (n, c) =>
                                                        {
                                                            n.Category = c;
                                                            return n;
                                                        },
                                                        splitOn: "id").AsQueryable();
            }

        }


        //ok
        public void Add(News news)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = db.Execute(@"INSERT INTO news (news_heading, news_text, date_create, date_update, news_category_id)
                                      VALUES (@news_heading, @news_text, @date_create, @date_update, @news_category_id)", news);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public void Update(News news)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = db.Query(@"UPDATE news SET news_heading = @news_heading, news_text = @news_text, date_create = @date_create, date_update = @date_update, news_category_id = @news_category_id
                                    WHERE id = @id", news);
            }
        }
    }
}
