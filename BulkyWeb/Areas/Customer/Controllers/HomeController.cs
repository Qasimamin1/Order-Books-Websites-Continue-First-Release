using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;   
        public HomeController(ILogger<HomeController> logger , IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

            _logger = logger;
        }

        public IActionResult Index()
        {
            //create product list
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            //pass it here
            return View(productList);
        }
        //based on ID we get all details
        public IActionResult Details( int productId)
        {
            // based on ID it will retrieve the data
            ShoppingCart cart = new()
            {

                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId,

            };
           
            //pass it here
            return View(cart);
        }
        [HttpPost]
        [Authorize] // for the authentication user is authorized
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = userId;
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
                u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                // Shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
              //  _unitOfWork.ShoppingCart.Update(cartFromDb); // Use cartFromDb here instead of shoppingCart
            }
            else
            {
                // Add cart record
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        

        //if (ModelState.IsValid)
        //{
        //    _unitOfWork.ShoppingCart.AddOrUpdate(cart);
        //    _unitOfWork.Save();
        //    return RedirectToAction("CartView");
        //}

        //cart.Product = _unitOfWork.Product.Get(u => u.Id == cart.ProductId, includeProperties: "Category");
        //return View(cart);
    }


    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}