using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Bangazon.Models.OrderViewModels;
using Bangazon.Models.ReportViewModels;

namespace Bangazon.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Orders/AddProduct
        [Authorize]
        public async Task<IActionResult> AddProduct([FromRoute]int? id)
        {
            // Find the product requested
            Product productToAdd = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == id);

            // Get the current user
            var user = await GetCurrentUserAsync();

            // See if the user has an open order
            var openOrder = await _context.Order.SingleOrDefaultAsync(o => o.User == user && o.PaymentTypeId == null);

            // If no order, create one, else add to existing order
            if (openOrder == null)
            {
                Order newOrder = new Order
                {
                    DateCompleted = null,
                    UserId = user.Id,
                    PaymentTypeId = null
                };
                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();
                openOrder = newOrder;
            }

            OrderProduct newOrderProduct = new OrderProduct
            {
                OrderId = openOrder.OrderId,
                ProductId = productToAdd.ProductId
            };
            _context.Add(newOrderProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }



        //GET: Orders/ShoppingCart
        [Authorize]
        public async Task<IActionResult> ShoppingCart()
        {
            // Get the current user
            var user = await GetCurrentUserAsync();

            // See if the user has an open order
            var openOrder = await _context.Order.SingleOrDefaultAsync(o => o.User == user && o.PaymentTypeId == null);

            //Get user's payment types
            List<SelectListItem> paymentTypes = _context.PaymentType
                               .Where(pt => pt.UserId == user.Id)
                               .Select(li => new SelectListItem
                               {
                                   Text = li.Description,
                                   Value = li.PaymentTypeId.ToString()
                               }).ToList();
            paymentTypes.Insert(0, new SelectListItem
            {
                Text = "Choose Payment Type...",
                Value = "0"
            });

            // Get all products associated with users open order, group them in anonymous typed object with Key, Count, Title
            var productsInCart = _context.OrderProduct
                                          .Where(op => op.OrderId == openOrder.OrderId)
                                          .Select(op => op.Product)
                                          .GroupBy(p => p.ProductId,
                                            p => p.Title,
                                            (key, Title) => new
                                            {
                                                key = key,
                                                count = Title.Count(),
                                                title = Title
                                            })
                                          .ToList()
                                          ;

            // create empty array to use as OrderDetailViewModel LineItem property
            List<OrderLineItem> shoppingCartLineItems = new List<OrderLineItem>();

            //Take each product, createa a new OrderLineItem object and place in placeholder array shoppingCartLineItems
            productsInCart.ForEach(p =>
            {
                Product product = _context.Product.SingleOrDefault(cp => cp.ProductId == p.key);

                OrderLineItem newLineItem = new OrderLineItem
                {
                    Product = product,
                    Units = p.count,
                    Total = (p.count * product.Price)
                };

                shoppingCartLineItems.Add(newLineItem);
            });

            //Create OrderDetailViewModel using current users open order and shoppingCartLineItems
            OrderDetailViewModel model = new OrderDetailViewModel
            {
                Order = openOrder,
                LineItems = shoppingCartLineItems,
                PaymentTypes = paymentTypes
            };

            return View(model);
        }


        //GET: Orders/Reports
        [Authorize]
        public async Task<IActionResult> Reports()
        {
            return View();
        }

        //GET: Orders/MultipleOrders
        public async Task<IActionResult> MultipleOrders()
        {
            // create list to hold users that have more than one open order
            List<ApplicationUser> usersWithMultipleOrders = new List<ApplicationUser>();

            // gets list of users that have open orders
            var usersWithNullOrders = _context.ApplicationUsers
                .Include(u => u.Orders)
                .Where(u => u.Orders.Any(o => o.DateCompleted == null))
                .ToList()
                ;

            // gets only users who have 1 or more open ordersgi
            var usersWithMultipleNullOrders = usersWithNullOrders
                .Where(u => u.Orders
                    .Where(o => o.DateCompleted == null)
                    .Count() >= 2)
                .ToList()
                .OrderByDescending(u => u.Orders.Where(o => o.DateCompleted == null).Count())
                .ToList()
                ;

            return View(usersWithMultipleNullOrders);
        }

        //GET: Orders/AbandonedProducts
        public async Task<IActionResult> AbandonedProducts()
        {
            // get a list of products that are in open orders
            var productsInOpenOrders = _context.Product
                .Include(p => p.OrderProducts)
                .ThenInclude(op => op.Order)
                .Where(p => p.OrderProducts.Any(op => op.Order.DateCompleted == null))
                .Include(p => p.ProductType)
                .ToList()
                ;

            var abandonedProductTypes = productsInOpenOrders
                .GroupBy(p => p.ProductTypeId,
                p => p.ProductType,
                (id, type) => new AbandonedProductTypes
                {
                    ProductType = type.First(),
                    Count = type.Count()
                })
                .ToList()
                .OrderByDescending(apt => apt.Count)
                .ToList()
                .Take(5)
                .ToList()
                ;

            return View(abandonedProductTypes);
        }

        //GET: Orders/IncompleteOrders
        public async Task<IActionResult> IncompleteOrders()
        {
            // get a list of users with orders
            var usersOpenOrders = _context.ApplicationUsers
                .Include(au => au.Orders)
                .ThenInclude(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .OrderBy(au => au.LastName)
                .ToList()
                ;

            return View(usersOpenOrders);
        }

        //GET: Orders/OrderHistory: 
        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            // Get the current user
            var user = await GetCurrentUserAsync();

            // Get List of Completed Orders
            var usersPastOrders = _context.Order
                .Include(o => o.User)
                .Where(o => o.User == user)
                .Where(o => o.DateCompleted != null)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .OrderByDescending(o => o.DateCompleted)
                .ToList()
                ;

            return View(usersPastOrders);
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.PaymentType).Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            // Get the current user
            var user = await GetCurrentUserAsync();

            // Get all products associated with completed order, group them in anonymous typed object with Key, Count, Title
            var productsInOrder = _context.OrderProduct
                                          .Where(op => op.OrderId == id)
                                          .Select(op => op.Product)
                                          .GroupBy(p => p.ProductId,
                                            p => p.Title,
                                            (key, Title) => new
                                            {
                                                key = key,
                                                count = Title.Count(),
                                                title = Title
                                            })
                                          .ToList()
                                          ;

            // create empty array to use as OrderDetailViewModel LineItem property
            List<OrderLineItem> completedOrderLineItems = new List<OrderLineItem>();

            //Take each product, createa a new OrderLineItem object and place in placeholder array shoppingCartLineItems
            productsInOrder.ForEach(p =>
            {
                Product product = _context.Product.SingleOrDefault(cp => cp.ProductId == p.key);

                OrderLineItem newLineItem = new OrderLineItem
                {
                    Product = product,
                    Units = p.count,
                    Total = (p.count * product.Price)
                };

                completedOrderLineItems.Add(newLineItem);
            });

            //Create OrderDetailViewModel using current users open order and shoppingCartLineItems
            OrderDetailViewModel model = new OrderDetailViewModel
            {
                Order = _context.Order.FirstOrDefault(o => o.OrderId == id),
                LineItems = completedOrderLineItems,
                PaymentTypes = null
            };

            return View(model);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,DateCreated,DateCompleted,UserId,PaymentTypeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletePurchase([Bind("PaymentTypeId,OrderId,UserId," +
            "DateCreated,PaymentType,OrderProducts")] Order order)
        {
            //using same code from ShoppingCart method to get list of line items within the order that is being completed.
            //Line items cannot be passed to this method from the shopping cart view. 
            // Get the current user
            var user = await GetCurrentUserAsync();
            // Get all products associated with users open order, group them in anonymous typed object with Key, Count, Title
            var productsInCart = _context.OrderProduct
                                          .Where(op => op.OrderId == order.OrderId)
                                          .Select(op => op.Product)
                                          .GroupBy(p => p.ProductId,
                                            p => p.Title,
                                            (key, Title) => new
                                            {
                                                key = key,
                                                count = Title.Count(),
                                                title = Title
                                            })
                                          .ToList()
                                          ;

            // create empty array to use as OrderDetailViewModel LineItem property
            List<OrderLineItem> shoppingCartLineItems = new List<OrderLineItem>();

            //Take each product, createa a new OrderLineItem object and place in placeholder array shoppingCartLineItems
            productsInCart.ForEach(p =>
            {
                Product product = _context.Product.SingleOrDefault(cp => cp.ProductId == p.key);

                OrderLineItem newLineItem = new OrderLineItem
                {
                    Product = product,
                    Units = p.count,
                    Total = (p.count * product.Price)
                };

                shoppingCartLineItems.Add(newLineItem);
            });


            //If you want to check errors in model state use the code below:
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            order.DateCompleted = DateTime.Now;
            ModelState.Remove("order.User");

            if (ModelState.IsValid && order.PaymentTypeId != 0)
            {
                try
                {
                    _context.Update(order);
                    //await _context.SaveChangesAsync();
                    shoppingCartLineItems.ForEach(li =>
                    {
                        //parameter 0
                        int newQty = li.Product.Quantity - li.Units;
                        //parameter 1
                        int productId = li.Product.ProductId;
                        //create new instance of product with updated quantity
                        Product product = li.Product;
                        product.Quantity = li.Product.Quantity - li.Units;
                        _context.Update(product);
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        order.PaymentTypeId = null;
                        order.DateCompleted = null;
                        throw;
                    }
                }
                return RedirectToAction("Index", "Products");
            }
            else
            {
                order.PaymentTypeId = null;
                order.DateCompleted = null;
            }
            //ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
            return RedirectToAction(nameof(ShoppingCart));
        }

        // POST: Orders/DeleteProduct
        [Authorize]
        public async Task<IActionResult> DeleteProduct([FromRoute]int? id)
        {
            var user = await GetCurrentUserAsync();

            var openOrder = await _context.Order.SingleOrDefaultAsync(o => o.User == user && o.PaymentTypeId == null);


            // Find the product requested
            List<OrderProduct> productToDelete = await _context.OrderProduct
                .Where(op => op.OrderId == openOrder.OrderId && op.ProductId == id).ToListAsync();


            foreach (var product in productToDelete)
            {
                _context.Remove(product);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("ShoppingCart", "Orders");
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.PaymentType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            var orderProducts =  _context.OrderProduct.Where(op => op.OrderId == id);
            foreach(var item in orderProducts)
            {
                _context.OrderProduct.Remove(item);
            }
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Products");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
