using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Dto
{
    public class NewsDto
    {
        public NewsDto(int id, string news_heading, string news_text, DateTime date_create, DateTime date_update, int news_category_id)
        {
            this.id = id;
            this.news_heading = news_heading;
            this.news_text = news_text;
            this.date_create = date_create;
            this.date_update = date_update;
            this.news_category_id = news_category_id;
        }
        public int id { get; set; }
        public string news_heading { get; set; }
        public string news_text { get; set; }
        public DateTime date_create { get; set; }
        public DateTime date_update { get; set; }
        public int news_category_id { get; set; }
    }
}
