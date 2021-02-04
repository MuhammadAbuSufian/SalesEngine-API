using Project.Model;
using Project.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class JournalViewModel : BaseViewModel<Journal>
    {
        public JournalViewModel(Journal model) : base(model)
        {
            Amount = model.Amount;
            Note = model.Note;
            Status = model.Status;
            ApprovedBy = model.ApprovedBy;
            JournalType = new JournalTypeViewModel(model.JournalType);
            JournalTypeId = model.JournalTypeId;
            SubmittedBy = new UserViewModel(model.SubmittedBy);
        }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public JournalStatus Status { get; set; }
        public string ApprovedBy { get; set; }
        public string JournalTypeId { get; set; }
        public virtual JournalTypeViewModel JournalType { get; set; }
        public virtual UserViewModel SubmittedBy { get; set; }
    }
}
