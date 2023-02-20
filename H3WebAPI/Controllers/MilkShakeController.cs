using Microsoft.AspNetCore.Mvc;

namespace H3WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MilkShakeController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFavoriteMilkshake(string favoritedrink)
        {
            try
            {
                //Creates cookie options
                CookieOptions cookieoptions = new CookieOptions();
                cookieoptions.Expires = DateTime.Now.AddMinutes(5);
                cookieoptions.HttpOnly = true;
                //Sets cookie
                Response.Cookies.Append("FavouriteMilkShake", favoritedrink, cookieoptions);
                return Ok($"Your favourite drink is : {favoritedrink}");
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFavouriteDrinkFromCookie()
        {
            try
            {
                //Gets cookie if exist
                if (Request.Cookies["FavouriteMilkShake"] != null)
                {
                    //Prints out cookie information
                    string drink = Request.Cookies["FavouriteMilkShake"];
                    return Ok($"Favorite drink by cookie {drink}");
                }
                return Problem("Unknown");
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }
    }
}
