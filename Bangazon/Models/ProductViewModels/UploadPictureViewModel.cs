using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class UploadPictureViewModel
    {
        public Product Product { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}

