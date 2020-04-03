using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD1.Entities
{
    public class Article
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
    }
}
