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

        public News Find(int id)
        {
            using (IDbConnection db = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                return db.Query<News>("SELECT * FROM news WHERE id = @Id", new { Id = id}).FirstOrDefault();
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
