using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public abstract class EntityBase
    {
        [Key]
        [Index("IX_Id", 1, IsUnique = true)]
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletionTime { get; set; }
        public string DeletedBy { get; set; }

        [Required]
        public string CreatedCompany { get; set; }

        [DefaultValue("True")]
        public bool Active { get; set; }
    }
}