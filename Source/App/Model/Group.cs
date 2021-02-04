using System.Collections.Generic;

namespace Project.Model
{
    public class Group : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

//        public virtual ICollection<Product> Products { get; set; }
    }
}