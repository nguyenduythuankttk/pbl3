namespace Backend.Models.DTOs.Reponse{
    public class BillReponse{
        public List<StoreReponse> Store = new List<StoreReponse>();
        public List<
    }
    public class BillDetailReponse{
        public sting ProductName{get; set;}
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal InlineTotal { get; set; }
    }
}