using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Company: EntityBase
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(128)]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public decimal? Balence { get; set; }

        public string Note { get; set; }

        public int? BusinessType { get; set; }

        public DateTime ValidTill { get; set; }
    }
}