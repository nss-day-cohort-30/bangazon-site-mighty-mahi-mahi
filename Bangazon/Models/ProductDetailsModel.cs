using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }

        public bool UserHasOpinion { get; set; }

        public bool UserLikes { get; set; }

        public bool ButtonValue { get; set; }
    }
}
