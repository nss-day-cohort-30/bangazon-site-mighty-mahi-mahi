using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; } = new Product();

        public List<SelectListItem> ProductTypes { get; set; }

        public SqlConnection Connection;

        public ProductCreateViewModel()
        { }

        public ProductCreateViewModel(SqlConnection connection)
        {
            Connection = connection;
            GetAllProducts();
        }

        public void GetAllProducts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.ProductTypeId, p.Label
                                        FROM ProductTypes p";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<ProductType> productTypes = new List<ProductType>();

                    while(reader.Read())
                    {
                        ProductType productType = new ProductType
                        {
                            ProductTypeId = reader.GetInt32(reader.GetOrdinal("ProductTypeId")),
                            Label = reader.GetString(reader.GetOrdinal("Label"))
                        };
                        productTypes.Add(productType);
                    }
                    ProductTypes = productTypes.Select(li => new SelectListItem
                    {
                        Text = li.Label,
                        Value = li.ProductTypeId.ToString()
                    }
                    ).ToList();

                    ProductTypes.Insert(0, new SelectListItem
                    {
                        Text = "Choose a Product Type",
                        Value = "0"
                    });
                }
            }
        }
    }
}
