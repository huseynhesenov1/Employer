using Employe.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employe.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad mütləqdir.")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Soyad mütləqdir.")]
        public string ClientSurname { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi mütləqdir.")]
        public string ClientPhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Email düzgün formatda deyil.")]
        public string ClientEmail { get; set; }

        [Required(ErrorMessage = "Xidmət seçilməyib.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Master seçilməyib.")]
        public int MasterId { get; set; }

        public string Problem { get; set; }

        //public List<IFormFile>? Images { get; set; }
        //public string ImgPath { get; set; }

        public IFormFile Img { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
