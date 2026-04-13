using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{
    public class POApprovalChange{
        public Guid POApprovalChangeID { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Comment { get; set; }
        public PO_Status BfrStatus { get; set; }
        public PO_Status AftStatus { get; set; }
    }
}