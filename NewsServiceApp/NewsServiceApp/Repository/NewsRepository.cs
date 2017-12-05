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

        //Find by Id, return Dto, maybe need to change return
        public async Task<NewsDto> FindById(int id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = await db.QueryAsync<News>("SELECT * FROM news WHERE id = @Id", new { Id = id });

                var qrResult = qr.FirstOrDefault();
                
                return new NewsDto() { id = qrResult.id, date_create = qrResult.date_create, date_update = qrResult.date_update, news_heading = qrResult.news_heading, news_text = qrResult.news_text, news_category_id = qrResult.news_category_id };
            }
        }

        //Need to Return dto
        public async Task<IEnumerable<News>> GetAllDailyNews()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return await db.QueryAsync<News, Category, News>(@"SELECT n.*, c.id, c.name 
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
                                                        splitOn:"id");
            }
        }

        public async Task<IEnumerable<News>> GetAllImportantNews()
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return await db.QueryAsync<News, Category, News>(@"SELECT n.*, c.id, c.name 
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
                                                        splitOn: "id");
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
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = db.Execute(@"DELETE FROM news WHERE id = @Id", new { Id = id});
            }
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
