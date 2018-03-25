using CandyShop.Dal;
using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;




namespace CandyShop.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// In this function we open a connection to the database and the function returns the NewDelivery view
        /// </summary>
        public ActionResult NewDelievery()
        {
            if (Session["User"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            DelieveryDal dal = new DelieveryDal();
            DelieveryVM cvm = new DelieveryVM();
            cvm.Delievery = new Delievery();
            cvm.Delieveries = dal.Delieveries.ToList<Delievery>();
            return View(cvm);
        }

        /// <summary>
        /// the function searches a delivery by the name or id according to the user's choice
        /// </summary>
        public ActionResult SearchDelievery()
        {
            if (Session["User"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            DelieveryDal dal = new DelieveryDal();
            string searchValue;
            DelieveryVM cvm = new DelieveryVM();
                searchValue = Session["User"].ToString();
                List<Delievery> objDelieveries =
                        (from x in dal.Delieveries
                         where x.userName.Equals(searchValue)
                         select x).ToList<Delievery>();
                if (objDelieveries != null)
                {
                    cvm.Delieveries = objDelieveries;
                    return View("SearchDelievery", cvm);

                }
            ViewBag.TheResult = true;
            return View("SearchDelievery");
        }

        /// <summary>
        /// this function adds a new delivery to the database
        /// </summary>
        /// <returns>The NewDelivery view</returns>
        public ActionResult AddDelievery()
        {
            if (Session["User"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            DelieveryVM cvm = new DelieveryVM();
            Delievery objDelievery = new Delievery();
            DelieveryDal dal = new DelieveryDal();
                objDelievery.userName = Session["User"].ToString();
                objDelievery.address = Request.Form["Delievery.address"];
                objDelievery.phone = Request.Form["Delievery.phone"];
                objDelievery.delieveryName = Request.Form["Delievery.delieveryName"];
                if (ModelState.IsValid)
                {
                    dal.Delieveries.Add(objDelievery);
                    dal.SaveChanges();
                    cvm.Delievery = new Delievery();
                }
                else
                {
                    return View("NewDelievery");
                }
            cvm.Delieveries = dal.Delieveries.ToList<Delievery>();
            ViewBag.TheResult = true;
            return View("NewDelievery", cvm);

        }
        /// <summary>
        /// In this function we open a connection to the database and the function returns the NewUser view
        /// </summary>
        public ActionResult NewUser()
        {
            UserDal dal = new UserDal();
            UserVM cvm = new UserVM();
            cvm.User = new User();
            cvm.Users = dal.Users.ToList<User>();
            return View(cvm);
        }

        /// <summary>
        /// This function shows the inventory of the candies
        /// </summary>
        /// <returns>the DisplayCandies view</returns>
        public ActionResult CandyInventory()
        {
            CandyVM cvm = new CandyVM();
            cvm.Candy = new Candy();
            CandyDal dal = new CandyDal();
            cvm.Candies = new List<Candy>();
            cvm.Candies = dal.Candies.ToList<Candy>();
            return View("CandyInventory", cvm);
        } 

        /// <summary>
        /// This function searches a candy by its name 
        /// </summary>
        public ActionResult SearchCandies()
        {
            CandyDal dal = new CandyDal();
            string searchValue = Request.Form["CandyName"];
            List<Candy> objCandies;
            if (searchValue != null && searchValue.Equals(""))
            {
                objCandies = (from x in dal.Candies
                              where x.CandyName.Contains(" ")
                              select x).ToList<Candy>();
            }
            else
            {
                objCandies =
                    (from x in dal.Candies
                     where x.CandyName.Contains(searchValue)
                     select x).ToList<Candy>();
            }
            CandyVM cvm = new CandyVM();
            cvm.Candy = new Candy();
            cvm.Candies = objCandies;
            return View("SearchCandies", cvm);
        }


        /// <summary>
        /// This function adds a user to the database
        /// </summary>
        /// <returns>The NewUser view</returns>
        public ActionResult AddUser()
        {
            UserVM cvm = new UserVM();
            User objUser = new User();
            UserDal dal = new UserDal();

            objUser.FirstName = Request.Form["User.FirstName"];
            objUser.LastName = Request.Form["User.LastName"];
            objUser.ID = Request.Form["User.ID"];
            objUser.Password = Request.Form["User.Password"];

            if (ModelState.IsValid && checkID(objUser.ID) == 1)
            {
                dal.Users.Add(objUser);
                dal.SaveChanges();
                cvm.User = new User();
            }
            else
            {
                ViewBag.TheResult = true;
                return View("NewUser");
            }

            cvm.Users = dal.Users.ToList<User>();

            return View("NewUser", cvm);

        }

        /// <summary>
        /// This fumction checks if the id exists in the users database 
        /// </summary>
        /// <param> the function gets a name   </param>
        /// <returns>the function returns 0 if it exists and 1 if it doesn't</returns>
        public int checkID(string str)
        {
            UserDal dal = new UserDal();
            User objUser =
                (from x in dal.Users
                 where x.ID.Equals(str)
                 select x).FirstOrDefault();
            if (objUser == null)
                return 1;
            return 0;
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ShowHomePage()
        {

            return View();
        }

        public ActionResult Login()
        {

            return View();

        }


        /// <summary>
        /// The function which connects the user to the website by its ID and password
        /// </summary>
        /// <returns>The Login view</returns>
        public ActionResult Signin()
        {

            UserDal dal = new UserDal();
            string searchID = Request.Form["ID"];
            string searchPassword = Request.Form["Password"];
            User objUser =
                (from x in dal.Users
                 where x.ID.Equals(searchID) && x.Password.Equals(searchPassword)
                 select x).FirstOrDefault();
            if (objUser != null)
            { 
                if (objUser.Manager == 0)
                {
                    //TempData["Name"] is for remembering the name of the user for the login popup
                    TempData["Name"] = objUser.FirstName + " " + objUser.LastName;
                    Session["User"] = objUser.FirstName;
                    return View("ShowHomePage");
                }
                else
                {
                   
                    //If a mangaer connected we will change his session value accordingly
                    Session["Manager"] = "Yes";
                    TempData["Name"] = objUser.FirstName + " " + objUser.LastName;
                    return RedirectToAction("", "Manager");
                }
            }
            ViewBag.TheResult = true;
            return View("Login");
        }

        /// <summary>
        /// The function which logs the user out of the system
        /// </summary>
        /// <returns>The ShowHomePage view</returns>
        public ActionResult Logout()
        {
            //If Session["User"] is not null it means we have connected from a user
            if (Session["User"] != null)
            {
                Session["User"] = null;
                //TempData["LoggedOut"] is for the popup which appears when the user is logged out 
                //if the user is logged out than value will be true and the popup will show
                TempData["LoggedOut"] = true;
            }
            return View("ShowHomePage");

        }

    }
}