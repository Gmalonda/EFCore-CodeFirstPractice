using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore_CodeFirstPracticePersistenceLayer.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string StatusDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Product> Products {get;set;}
    }
}
