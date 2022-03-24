using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Diagnostics;
using TaskHotelProject.Models;
using TaskHotelProject.Repository.IRepository;

namespace TaskHotelProject.Controllers
{
    public class HomeController : Controller
    {
        private IHotelApi _harepo;
        // Setting Static Property to use state throughout the class
        public static List<Hotel>? HotelsData;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHotelApi harepo)
        {
            _logger = logger;
            _harepo = harepo;
        }

        public async Task<ActionResult> Index( int? refundable_id, int? rating_id, bool? available)
        {
            try
            {
                ViewBag.AvailableCheck = null;
                string ratingId = "";
                // If All Empty it will load the entire list by default
                if (!refundable_id.HasValue && !rating_id.HasValue && !available.HasValue || available == false)
                {
                    HotelsData = await GetHotelList();
                }
                // If Filtered with rating 
                else if (rating_id.HasValue)
                {
                    if(rating_id > 0 && rating_id < 6)
                    {
                        HotelsData = await GetHotelList_Ratings(Convert.ToInt32(rating_id));
                        ratingId = rating_id.ToString() + " Star";
                    }
                    
                }
                // If filtered with refundablity
                else if (refundable_id.HasValue)
                {
                    if(refundable_id == 0 || refundable_id == 1)
                    {
                        HotelsData = await GetHotelList_IsRefundable(Convert.ToInt32(refundable_id));
                    }
                   
                }
                // If filtered with availablity
                else if (available.HasValue && available == true)
                {
                    HotelsData = await GetHotelList_Available();
                    ViewBag.AvailableCheck = true;
                }
                
                ViewData["Refundable"] = new SelectList(GetRefundList, "Val", "Name", refundable_id);
                ViewData["Rating"] = new SelectList(GetRatingsList, null, null, ratingId);
                return View();
            }
            catch(Exception ex)
            {
                TempData["MSG"] = "An Error Occured";
                return View("Index");
            }
            
        }




        #region Hotel Lists Functions
        // Get Entire Hotels List Function
        public async Task<List<Hotel>> GetHotelList()
        {
            List<Hotel> listhotels = new List<Hotel>();
            //Get The Entire List and Map It To A Hotel Class for easier Rendering when calling from AJAX in View
            try
            {
                AvaliabilitiesResponse model = await _harepo.GetHotelsApi();
                listhotels = MapToHotelsList(model);


                return listhotels;
            }
            catch (Exception ex)
            {
                TempData["MSG"] = " Error Occured";
                return listhotels;
            }

        }
        // Get Hotels As Per Filtered Ratings
        public async Task<List<Hotel>> GetHotelList_Ratings(int rating)
        {
            List<Hotel> listhotels = new List<Hotel>();
            //Get The Filtered Hotel List For Ratings and Map It To A Hotel Class for easier Rendering when calling from AJAX in View
            try
            {
                // Check for invalid Rating
                if (rating < 0 && rating > 5)
                {
                    TempData["MSG2"] = "An Invalid Rating";
                    return listhotels;
                }
                AvaliabilitiesResponse model = await _harepo.GetHotelsApi_Ratings(rating);
                listhotels = MapToHotelsList(model);
                return listhotels;
            }
            catch (Exception ex)
            {
                TempData["MSG"] = " Error Occured";
                return listhotels;
            }

        }
        // Get Hotels As Per Filtered Refundablity
        public async Task<List<Hotel>> GetHotelList_IsRefundable(int refund)
        {
            List<Hotel> listhotels = new List<Hotel>();
            //Get The Filtered Hotel List For Ratings and Map It To A Hotel Class for easier Rendering when calling from AJAX in View
            try
            {
                // 0 for Non Refundable Hotels
                // 1 for Refundable Hotels
                if (refund == 1 || refund == 0)
                {
                    AvaliabilitiesResponse model = await _harepo.GetHotelsApi_IsRefundable(refund);
                    listhotels = MapToHotelsList(model);
                }
                else
                {
                    TempData["MSG2"] = "An Invalid Refund Selection";
                    return listhotels;
                }

                return listhotels;
            }
            catch (Exception ex)
            {
                TempData["MSG"] = " Error Occured";
                return listhotels;
            }

        }
        // Get Hotels Avaialable
        public async Task<List<Hotel>> GetHotelList_Available()
        {
            List<Hotel> listhotels = new List<Hotel>();
            //Get The Filtered Hotel List For Availablity and Map It To A Hotel Class for easier Rendering when calling from AJAX in View
            try
            {
                AvaliabilitiesResponse model = await _harepo.GetHotelsApi_Available();
                listhotels = MapToHotelsList(model);
                return listhotels;
            }
            catch (Exception ex)
            {
                TempData["MSG"] = " Error Occured";
                return listhotels;
            }

        }



        // We Use This Mapping Function to Map Recieved Json Complex data Structure to Simplified Hotels List Easier For Rendering
        public List<Hotel> MapToHotelsList(AvaliabilitiesResponse model)
        {
            List<Hotel> listhotels = new List<Hotel>();
            if (model.HOTELS != null)
            {
                foreach (var hotel in model.HOTELS.HOTEL)
                {
                    Hotel shotel = new Hotel();
                    shotel.HOTELID = hotel.HOTELID;
                    shotel.CURRENCY = hotel.CURRENCY;
                    shotel.LOCATION = hotel.LOCATION;
                    shotel.AVAILABLE = hotel.AVAILABLE;
                    shotel.RATING = hotel.RATING;
                    shotel.STARTINGPRICE = hotel.STARTINGPRICE;
                    shotel.HOTELNAME = hotel.HOTELNAME;
                    shotel.ROOMS = hotel.ROOMS;
                    listhotels.Add(shotel);
                }
            }
            return listhotels;

        }
        #endregion

        // Function to Get The Refreshed Hotels List
        public List<Hotel> GetRefreshedHotelsData()
        {
            return HotelsData;
        }

        public List<string> GetRatingsList
        {
            get
            {
                List<string> ratingList = new List<string>();
                for (int i = 1; i <= 5; i++)
                {
                    ratingList.Add(i.ToString() + " Star");
                }
                return ratingList;
            }
        }
        public List<RefundModel> GetRefundList
        {
            get
            {
                List<RefundModel> refundModels = new List<RefundModel>();
                RefundModel model = new RefundModel();
                model.Val = 1;
                model.Name = "Refundable";
                refundModels.Add(model);

                model = new RefundModel();
                model.Val = 0;
                model.Name = "Not Refundable";
                refundModels.Add(model);

                return refundModels;
            }
        }

        #region Extras
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


    }

    public class RefundModel
    {
        public int Val { get; set; }
        public string Name { get; set; }
    }
}