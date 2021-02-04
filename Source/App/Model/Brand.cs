using System.Collections.Generic;

namespace Project.Model
{
    public class Brand: EntityBase
    {
        public string Name { get; set; }

        public string Representive { get; set; }

        public string RepContact { get; set; }

        public string Note { get; set; }

//        public virtual ICollection<Product> Products { get; set; }

    }
}