using Employe.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employe.ViewModels
{
    public class OrderVM
    {
       
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public int ServiceId { get; set; }
       
        public int MasterId { get; set; }
        

        public string Problem { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
