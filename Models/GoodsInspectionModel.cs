using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public enum InspectionStatus {
        Pending,
        Completed,
        Rejected
    }

    public class GoodsInspection {
        [Key]
        public Guid InspectionID { get; set; }
        public Guid ReceiptID { get; set; }
        [ForeignKey("ReceiptID")]
        public virtual Receipt Receipt { get; set; } = null!;
        public Guid InspectedBy { get; set; }
        [ForeignKey("InspectedBy")]
        public virtual Employee Employee { get; set; } = null!;
        public DateTime InspectedAt { get; set; }
        public InspectionStatus Status { get; set; } = InspectionStatus.Pending;
        public string? Note { get; set; }
        [JsonIgnore]
        public virtual ICollection<InspectionDetail> InspectionDetails { get; set; } = new List<InspectionDetail>();
    }
}
