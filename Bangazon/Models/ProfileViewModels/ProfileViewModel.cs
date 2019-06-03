using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProfileViewModels
{
    public class ProfileViewModel
    {
        //public ProfileViewModel(ApplicationUser user, List<PaymentType> paymentTypes)
        //{
        //    User = user;
        //    PaymentTypes = paymentTypes;
        //}
        public ApplicationUser User { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }

    }
}
