using CandyShop.Dal;
using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//If Session["Manager"] is not null it means we have connected from a manager
//TempData["Result"] is for the popup which appears in some cases, mostly in errors 
//if the result is true than the popup will show
namespace CandyShop.Controllers
{
    public class ManagerController : Controller
    {

        /// <summary>
        /// In this function we get all of our orders and show then in our view.
        /// </summary>
        public ActionResult Orders()
        {

            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            OrderDal dal = new OrderDal();
            OrderVM cvm = new OrderVM();
            cvm.Order = new Order();
            cvm.Orders = dal.Orders.ToList<Order>();
            return View(cvm);

        }

  /// <summary>
  /// In this function we open a connection to the database and the function returns the NewOrder view
  /// </summary>
        public ActionResult NewOrder()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            OrderDal dal = new OrderDal();
            OrderVM cvm = new OrderVM();
            cvm.Order = new Order();
            cvm.Orders = dal.Orders.ToList<Order>();
            return View(cvm);
        }

    /// <summary>
    /// this function we open a connection and gets its info from the form
    /// she then checks the validity of the data and inserts it to the database
    /// </summary>
        public ActionResult AddOrder()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            OrderVM cvm = new OrderVM();
            Order objOrder = new Order();
            OrderDal dal = new OrderDal();

            objOrder.orderAmount = Request.Form["Order.orderAmount"];
            objOrder.OrderName = Request.Form["Order.OrderName"];

            if (ModelState.IsValid)
            {
                dal.Orders.Add(objOrder);
                dal.SaveChanges();
                cvm.Order = new Order();
            }
            else
            {
                ViewBag.TheResult = true;
                return View("NewOrder");
            }

            cvm.Orders = dal.Orders.ToList<Order>();

            return View("NewOrder", cvm);

        }

        /// <summary>
        /// The function searches a delivery by the id or by name according to the user's choice
        /// </summary>

        public ActionResult SearchDelievery()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            DelieveryDal dal = new DelieveryDal();
            string searchValue;
            int n;
            DelieveryVM cvm = new DelieveryVM();
            searchValue = Request.Form["DelieveryNameOrID"];
            string radio = Request.Form["optradio"];
            if (searchValue != null)
            {
                if (radio != null)
                {
                    if (radio.Equals("delieveryID"))
                    {
                        if (searchValue != "" && int.TryParse(searchValue, out n))
                        {
                            int id = int.Parse(searchValue);
                            Delievery objDelieveries =
                                    (from x in dal.Delieveries
                                     where x.delieveryID.Equals(id)
                                     select x).FirstOrDefault();
                            if (objDelieveries != null)
                            {
                                cvm.Delievery = objDelieveries;

                                return View("SearchDelievery", cvm);

                            }
                        }
                    }
                    else if (radio.Equals("delieveryName"))
                    {
                        if (searchValue != "")
                        {
                            Delievery objDelieveries =
                                    (from x in dal.Delieveries
                                     where x.delieveryName.Equals(searchValue)
                                     select x).FirstOrDefault();
                            if (objDelieveries != null)
                            {
                                cvm.Delievery = objDelieveries;

                                return View("SearchDelievery", cvm);

                            }
                        }
                    }
                }
                ViewBag.TheResult = true;
            }            
            return View("SearchDelievery");
    }

        /// <summary>
        /// The function deletes a delivery by the id that the user entered
        /// </summary>
        public ActionResult DeleteDelievery()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            DelieveryDal dal = new DelieveryDal();
            DelieveryVM cvm = new DelieveryVM();
            string value = Request.Form["DelieveryID"];
            if (value != null)
            {
                if (value != "")
                {
                    int id = int.Parse(value);
                    Delievery objDelieveries =
                        (from x in dal.Delieveries
                         where x.delieveryID.Equals(id)
                         select x).FirstOrDefault();
                    if (objDelieveries == null)
                    {
                        ViewBag.TheResult = true;
                        return View("DeleteDelievery");
                    }
                    dal.Delieveries.Remove(objDelieveries);
                    dal.SaveChanges();
                }
            }
            return View("DeleteDelievery");
        }

        /// <summary>
        /// The function searches a users by their name according to the name that the user entered
        /// </summary>

        public ActionResult SearchUsers()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            UserDal dal = new UserDal();
            string searchValue = Request.Form["FirstName"];
            List<User> objUsers =
                (from x in dal.Users
                 where x.FirstName.Contains(searchValue)
                 select x).ToList<User>();
            UserVM cvm = new UserVM();
            cvm.User = new User();
            cvm.Users = objUsers;
            return View("SearchUsers", cvm);
        }

        /// <summary>
        /// This function shows the inventory of the candies
        /// </summary>
        /// <returns>the DisplayCandies view</returns>

        public ActionResult CandyInventory()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
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
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
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
        /// This function adds a candy asynchronically, thus showing us the candy inventory in realtime 
        /// </summary>
        /// <returns>A json object</returns>
        public ActionResult AddCandy()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            Candy objCandy = new Candy();
            CandyDal dal = new CandyDal();
            List<Candy> objCandies = dal.Candies.ToList<Candy>();
            objCandy.CandyName = Request.Form["Candy.CandyName"];
            objCandy.CandyType = Request.Form["Candy.CandyType"];
            objCandy.CandyColor = Request.Form["Candy.CandyColor"];

            if (ModelState.IsValid && checkName(objCandy.CandyName) == 1)
            {

                dal.Candies.Add(objCandy);
                dal.SaveChanges();
            }
            else
            {               
                ViewBag.TheResult = true;            
            }
            return Json(objCandies, JsonRequestBehavior.AllowGet);
        }
      
        /// <summary>
        /// This function opens a coonection to the database and calls the NewCandy view
        /// </summary>
       
        public ActionResult NewCandy()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            CandyDal dal = new CandyDal();
            CandyVM cvm = new CandyVM();
            cvm.Candy = new Candy();
            cvm.Candies = dal.Candies.ToList<Candy>();
            return View(cvm);
        }

        /// <summary>
        /// This function return a json object of candies
        /// </summary>
        /// <returns></returns>
        public ActionResult getCandiesJson()
        {
            CandyDal dal = new CandyDal();
            List<Candy> objCandies = dal.Candies.ToList<Candy>();
            return Json(objCandies, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// This fumction checks if the name exists in the candies database 
        /// </summary>
        /// <param> the function gets a name   </param>
        /// <returns>the function returns 0 if it exists and 1 if it doesn't</returns>
        public int checkName(string str)
        {
            CandyDal dal = new CandyDal();
            Candy objCandy =
                (from x in dal.Candies
                 where x.CandyName.Equals(str)
                 select x).FirstOrDefault();
            if (objCandy == null)
                return 1;
            return 0;
        }

        /// <summary>
        /// This function checks if a delivery with this id exists in the database 
        /// </summary>
        /// <param >the function gets the delivery id</param>
        /// <returns> the function returns 0 if it exists and 1 otherwise </returns>
        public int checkdelieveryID(string str)
        {
            DelieveryDal dal = new DelieveryDal();
            Delievery objDelievery =
                (from x in dal.Delieveries
                 where x.delieveryID.Equals(str)
                 select x).FirstOrDefault();
            if (objDelievery == null)
                return 1;
            return 0;
        }

        /// <summary>
        ///  the function for showing the homepage
        /// </summary>
        public ActionResult ShowHomePage()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            return View();
        }

        /// <summary>
        /// the function which logsout the user
        /// </summary>
        public ActionResult Logout()
        {
            if (Session["Manager"] == null)
            {
                TempData["Result"] = true;
                return RedirectToAction("", "");
            }
            Session["Manager"] = null;
            return RedirectToAction("", "");

        }

    }
}