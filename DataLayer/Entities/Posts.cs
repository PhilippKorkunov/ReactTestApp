using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table(name: "Posts")]
    public class Post : Entity
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
