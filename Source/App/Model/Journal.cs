using Project.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Journal : EntityBase
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public JournalStatus Status { get; set; }
        public string ApprovedBy { get; set; }
        public string JournalTypeId { get; set; }
        [ForeignKey("JournalTypeId")]
        public virtual JournalType JournalType { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User SubmittedBy { get; set; }
    }
}
