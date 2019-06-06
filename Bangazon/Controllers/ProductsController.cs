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
using Bangazon.Models.OrderViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

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
            ViewBag.SearchString = false;
            // if user has entered search term into search bar, list of products containing the search string will be returned
            if (SearchString != null)
            {

                var applicationDbContext = _context.Product.Include(p => p.ProductType)
                   .Include(p => p.User)

                   .Where(p => p.Title.Contains(SearchString) || p.City.Contains(SearchString))

                   .OrderByDescending(p => p.DateCreated);
                ViewBag.SearchString = true;
                return View(await applicationDbContext.ToListAsync());
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
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            // orders the products added by the user by date created and then only selects the 20 latest with the .Take() method
            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            var isOpined = _context.UserProductLike
                .Where(upl => upl.UserId == user.Id)
                .Where(upl=> upl.ProductId == id)
                .ToList()
                .Count()
                ;

            var placeholder = false;
            var isLiked = false;

            if (isOpined == 1)
            {
                placeholder = true;
                isLiked = _context.UserProductLike
                    .Where(upl => upl.UserId == user.Id)
                    .Where(upl => upl.ProductId == id)
                    .First()
                    .IsLiked;
            }

            ProductDetailsModel model = new ProductDetailsModel
            {
                Product = product,
                UserHasOpinion = placeholder,
                UserLikes = isLiked
            };
            if (product == null)
            {
                return NotFound();
            }

            return View(model);
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            UploadPictureViewModel model = new UploadPictureViewModel();
            model.Product = new Product();
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UploadPictureViewModel model)
        {

            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("UserId");
            ModelState.Remove("DateCreated");

            var user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                model.Product.DateCreated = DateTime.Now;
                model.Product.User = user;
                model.Product.UserId = user.Id;

                if (model.ImageFile != null)
                {
                    var fileName = Path.GetFileName(model.ImageFile.FileName);
                    Path.GetTempFileName();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    // var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                        // validate file, then move to CDN or public folder
                    }
                    model.Product.ImagePath = model.ImageFile.FileName;
                }


                _context.Add(model.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductType"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", model.Product.ProductType);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", model.Product.UserId);
            return View(model);
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
                .FirstOrDefaultAsync(m => m.ProductId == id)
                ;
            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(GetUserProducts));
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

                var ratingsForCurrentProduct = _context.UserProductRating
                .Where(upr => upr.ProductId == p.ProductId)
                .ToList()
                ;

                double averageRating = 0;

                if (ratingsForCurrentProduct.Count() != 0)
                {
                    averageRating = ratingsForCurrentProduct.Select(upr => upr.Rating).Average();
                }


                //create a UserProductStatusModel for the current product and add it to the list that will be passed into the view
                UserProductStatusModel productStatusModel = new UserProductStatusModel
                {
                    Product = p,
                    NumberSold = numberSold.NumberSold,
                    AverageRating = averageRating
                };

                UsersProductStatusModels.Add(productStatusModel);
            });

            return View(UsersProductStatusModels);
        }



        //
        [Authorize]
        public async Task<IActionResult> AddRating(int id, [FromForm] int Rating, [FromForm] int OrderId)
        {
            // Get current User
            var user = await GetCurrentUserAsync();

            // Create new UserProductRating
            UserProductRating upr = new UserProductRating
            {
                ProductId = id,
                UserId = user.Id,
                Rating = Rating
            };

            // Send user product rating to DB
            _context.UserProductRating.Update(upr);
            _context.SaveChanges();

            return RedirectToAction("Details", "Orders", new { id = OrderId });
        }

        [Authorize]
        public async Task<IActionResult> CreateLike(int id, [FromForm] bool ButtonValue)
        {
            // Get User
            var user = await GetCurrentUserAsync();

            //Create new UserProductLike
            UserProductLike upl = new UserProductLike
            {
                ProductId = id,
                UserId = user.Id,
                IsLiked = ButtonValue
            };

            //Send user like to DB
            _context.UserProductLike.Update(upl);
            _context.SaveChanges();

            return RedirectToAction("Details", "Products", new { id = id });
        }
    }
}
