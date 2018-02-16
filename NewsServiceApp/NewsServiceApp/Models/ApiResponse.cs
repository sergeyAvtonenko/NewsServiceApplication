using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NewsServiceApp.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public News News { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
