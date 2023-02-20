using H3WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace H3WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Product> GetProducts(string productname, float price)
        {
            //Gets current products from session
            List<Product> products = HttpContext.Session.GetObjectFromJson<List<Product>>("test");
            if (products != null)
            {
                //Add new product and sets session to updated list of products
                products.Add(new Product(productname, price));
                HttpContext.Session.SetObjectAsJson("test", products);
                return products;
            }
            else
            {
                //Creates a list and adds product
                products = new List<Product>();
                products.Add(new Product(productname, price));
                //Sets session with list of products
                HttpContext.Session.SetObjectAsJson("test", products);
                return products;
            }
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult DeleteProduct(string productName)
        {
            //Gets current products from session
            List<Product> products = HttpContext.Session.GetObjectFromJson<List<Product>>("test");
            if (products != null)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    //Finds item in list
                    if (products[i].Name.ToLower() == productName.ToLower())
                    {
                        //Removes item from list and updates session
                        products.RemoveAt(i);
                        HttpContext.Session.SetObjectAsJson("test", products);
                        return Ok($"Deleted {productName}");
                    }
                }
            }
            return Problem("No products");
        }
    }
}
