using System.ComponentModel.DataAnnotations;

namespace Employe.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt  { get; set; }
        public ICollection<Master>? Masters { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}