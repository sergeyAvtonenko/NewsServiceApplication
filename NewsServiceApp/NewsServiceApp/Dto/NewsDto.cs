using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServiceApp.Dto
{
    public class NewsDto
    {
        public int id { get; set; }
        public string news_heading { get; set; }
        public string news_text { get; set; }
        public DateTime date_create { get; set; }
        public DateTime date_update { get; set; }
        public int news_category_id { get; set; }
    }
}
