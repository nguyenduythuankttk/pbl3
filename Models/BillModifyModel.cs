using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum ModifySize
    {
        S,
        M,
        L,
        XL
    }

    public enum ModifyType
    {
        Size,
        Combo,
        Addon
    }

    public class Addon
    {
        [Key]
        public int AddonID { get; set; }

        public string AddonName { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public class BillModify
    {
        [Key]
        public Guid BillModifyID { get; set; }

        public Guid BillID { get; set; }
        public int ProductID { get; set; }
        public virtual BillDetail BillDetail { get; set; } = null!;

        public ModifyType ModifyType { get; set; }
        public ModifySize Size { get; set; }
    }
}