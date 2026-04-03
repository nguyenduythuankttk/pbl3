namespace Backend.Models.DTOs.Reponse{
    public class BillReponse{
        public StoreReponse Store {get; set;}
        public List<BillDetailReponse> Detail = new List<BillDetailReponse>();
        public decimal TotalPrice {get; set;}
    }
    public class BillDetailReponse{
        public sting ProductName{get; set;}
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal InlineTotal { get; set; }
    }
}