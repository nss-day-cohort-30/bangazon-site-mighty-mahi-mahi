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
            List<OrderLineItem> shoppingCartLinteItems = new List<OrderLineItem>();

            //Take each product, createa a new OrderLineItem object and place in placeholder array shoppingCartLineItems
            productsInCart.ForEach(p =>
            {
                Product product =  _context.Product.SingleOrDefault(cp => cp.ProductId == p.key);

                OrderLineItem newLineItem = new OrderLineItem
                {
                    Product = product,
                    Units = p.count,
                    Cost = (p.count * product.Price)
                };

                shoppingCartLinteItems.Add(newLineItem);
            });

            //Create OrderDetailViewModel using current users open order and shoppingCartLineItems
            OrderDetailViewModel model = new OrderDetailViewModel
            {
                Order = openOrder,
                LineItems = shoppingCartLinteItems,
                PaymentTypes = paymentTypes
            };

            return View(model);
        }


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.PaymentType).Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // Get the current user
                var user = await GetCurrentUserAsync();

                // See if the user has an open order
                var openOrder = await _context.Order.SingleOrDefaultAsync(o => o.User == user && o.PaymentTypeId == null);

                return View(openOrder);
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

        //// GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
        //    ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletePurchase([Bind("PaymentTypeId,OrderId,UserId," +
            "DateCreated,PaymentType,OrderProducts")] Order order)
        {
            //If you want to check errors in model state use the code below:
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            order.DateCompleted = DateTime.Now;
            ModelState.Remove("order.User");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
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
            return  RedirectToAction(nameof(ShoppingCart));
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
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
