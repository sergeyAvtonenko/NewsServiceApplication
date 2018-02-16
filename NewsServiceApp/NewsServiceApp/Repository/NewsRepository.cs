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

        public async Task<IEnumerable<News>> GetNewsPageAsync(int skip, int take, int news_category_id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"SELECT *
                                FROM news
                                WHERE news_category_id = @News_category_id
                                ORDER BY date_create DESC
                                LIMIT @Skip,@Take";
                var news = await db.QueryAsync<News>(query, new { Skip = skip, Take = take, News_category_id = news_category_id });

                return news;
            }
        }

        public async Task<News> FindById(int id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = await db.QueryAsync<News>("SELECT * FROM news WHERE id = @Id", new { Id = id });

                return qr.FirstOrDefault();
            }
        }        

        public async Task<News> Add(News news)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qr = await db.ExecuteAsync(@"INSERT INTO news (news_heading, news_text, date_create, date_update, news_category_id)
                                      VALUES (@news_heading, @news_text, @date_create, @date_update, @news_category_id)", news);
            }

            return news;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    await db.ExecuteAsync(@"DELETE FROM news WHERE id = @Id", new { Id = id });
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }


        public async Task<bool> Update(News news)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    await db.ExecuteAsync(@"UPDATE news SET news_heading = @news_heading, news_text = @news_text, date_create = @date_create, date_update = @date_update, news_category_id = @news_category_id
                                    WHERE id = @id", news);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        //public Task<IEnumerable<News>> GetNewsByCategory(string _category)
        //{
        //    using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        return db.QueryAsync<News, Category, News>(@"SELECT n.*, c.id, c.name 
        //                                                FROM news n 
        //                                                INNER JOIN category c 
        //                                                ON c.id = n.news_category_id 
        //                                                WHERE c.name = @category
        //                                                ORDER BY n.date_create DESC",
        //                                                (n, c) =>
        //                                                {                                                            
        //                                                    n.Category = c;
        //                                                    return n;
        //                                                }, new {category = _category},
        //                                                splitOn: "id");
        //    }
        //}


        //public Task<IEnumerable<News>> GetAllDailyNews()
        //{
        //    using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        return db.QueryAsync<News, Category, News>(@"SELECT n.*, c.id, c.name 
        //                                                FROM news n 
        //                                                INNER JOIN category c 
        //                                                ON c.id = n.news_category_id 
        //                                                WHERE c.name = 'daily'
        //                                                ORDER BY n.date_create DESC", 
        //                                                (n, c) =>
        //                                                  {
        //                                                      n.Category = c;
        //                                                      return n;
        //                                                  },
        //                                                splitOn:"id");
        //    }
        //}

        //public async Task<IEnumerable<News>> GetAllImportantNews()
        //{
        //    using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        return await db.QueryAsync<News, Category, News>(@"SELECT n.*, c.id, c.name 
        //                                                FROM news n 
        //                                                INNER JOIN category c 
        //                                                ON c.id = n.news_category_id 
        //                                                WHERE c.name = 'important'
        //                                                ORDER BY n.date_create DESC",
        //                                                (n, c) =>
        //                                                {
        //                                                    n.Category = c;
        //                                                    return n;
        //                                                },
        //                                                splitOn: "id");                
        //    }

        //}
    }
}
