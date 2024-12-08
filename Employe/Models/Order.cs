using System.ComponentModel.DataAnnotations.Schema;

namespace Employe.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public int ServiceId { get; set; }
        [NotMapped]
        public Service Service { get; set; }
        public int MasterId { get; set; }
        [NotMapped]
        public Master Master { get; set; }

        public string Problem { get; set; }
        public string? ImgPath { get; set; }
        public bool IsActive { get; set; }
        public List<OrderPhoto>? orderPhotos { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
