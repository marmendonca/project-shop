using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
