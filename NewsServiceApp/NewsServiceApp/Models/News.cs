using FluentValidation;
using System;

namespace NewsServiceApp.Models
{
    public class News
    {
        public int id { get; set; }
        public string news_heading { get; set; }
        public string news_text { get; set; }
        public DateTime date_create { get; set; }
        public DateTime date_update { get; set; }
        public int news_category_id { get; set; }
        public Category Category { get; set; }
    }    

    public class CreateCustomerValidator : AbstractValidator<News>
    {
        public CreateCustomerValidator()
        {
            RuleFor(n => n.news_heading).NotEmpty().MaximumLength(200);
            RuleFor(n => n.news_text).NotEmpty();
            RuleFor(n => n.news_category_id).NotEmpty().GreaterThan(0).LessThan(3);
            RuleFor(n => n.date_create).NotEmpty();
        }
    }
}
