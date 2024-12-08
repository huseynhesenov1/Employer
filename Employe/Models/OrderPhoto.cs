namespace Employe.Models
{
    public class OrderPhoto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ImageUrl {  get; set; }
        //public bool IsCover { get; set; }

    }
}
