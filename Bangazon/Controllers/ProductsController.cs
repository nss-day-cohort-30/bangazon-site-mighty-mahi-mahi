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
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Bangazon.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() =>
            _userManager.GetUserAsync(HttpContext.User);

        // GET: Products
        public async Task<IActionResult> Index(string SearchString)
        {
            // if user has entered search term into search bar, list of products containing the search string will be returned
            if (SearchString != null)
            {
                var applicationDbContext1 = _context.Product.Include(p => p.ProductType)
                   .Include(p => p.User)

                   .Where(p => p.Title.Contains(SearchString) || p.City.Contains(SearchString))

                   .OrderByDescending(p => p.DateCreated);
                return View(await applicationDbContext1.ToListAsync());
            }
            // if the search bar is blank the complete list of products will be returned to the user
            else
            {
                var applicationDbContext = _context.Product
                    .Include(p => p.ProductType)
                    .Include(p => p.User)
                    .OrderByDescending(a => a.DateCreated).Take(20);

                return View(await applicationDbContext.ToListAsync());
            }

        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // orders the products added by the user by date created and then only selects the 20 latest with the .Take() method
            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {

            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,DateCreated,Description,UserId,Title,Price,Quantity,City,ImagePath,ProductTypeId")] Product product)
        {

            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("UserId");
            ModelState.Remove("DateCreated");

            var user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                product.DateCreated = DateTime.Now;
                product.User = user;
                product.UserId = user.Id;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductType"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductType);
            // ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,ProductTypeId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Delete));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }

        // GET: Only User products
        [Authorize]
        public async Task<IActionResult> GetUserProducts()
        {
            // Get current User
            var user = await GetCurrentUserAsync();

            // Get all products the User has listed
            var usersListedProducts = _context.Product
                .Where(p => p.UserId == user.Id)
                .ToList();

            //Create list to hold all of user's product status models
            List<UserProductStatusModel> UsersProductStatusModels = new List<UserProductStatusModel>();

            // Create UserProductStatusModel for each of the user's listed products and add them to the list created above
            usersListedProducts.ForEach(p =>
            {
                //Create an annonymous object with the number sold for the current product
                var numberSold = _context.OrderProduct
                .Where(op => op.Order.DateCompleted != null)
                .Where(op => op.ProductId == p.ProductId)
                .Include(op => op.Product)
                .GroupBy(op => op.Product,
                op => op.ProductId,
                (key, productId) => new
                {
                    id = key.ProductId,
                    NumberSold = productId.Count()
                })
                .DefaultIfEmpty(new
                {
                    id = p.ProductId,
                    NumberSold = 0
                })
                .FirstOrDefault()
                ;                

                //create a UserProductStatusModel for the current product and add it to the list that will be passed into the view
                UserProductStatusModel productStatusModel = new UserProductStatusModel
                {
                    Product = p,
                    NumberSold = numberSold.NumberSold
                };

                UsersProductStatusModels.Add(productStatusModel);
            });

            return View(UsersProductStatusModels);
        }
    }
}
