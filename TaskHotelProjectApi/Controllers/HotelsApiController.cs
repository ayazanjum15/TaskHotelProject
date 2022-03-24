using Microsoft.AspNetCore.Mvc;
using TaskHotelProjectApi.Models;
using TaskHotelProjectApi.Repository.IRepository;

namespace TaskHotelProjectApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class HotelsApiController : ControllerBase
    {
        private IHotelNavigation _hnrepo;
        public HotelsApiController(IHotelNavigation hnrepo)
        {
            _hnrepo = hnrepo;
        }


        #region Hotel APIs

        

        // Get All the hotels available 
        [HttpGet(Name = "GetHotels")]
        public IActionResult GetHotels()
        {
            try
            {
                var gethotels = _hnrepo.GetAvailableHotels();
                return Ok(gethotels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong while Fetching Hotels List List");
                return StatusCode(500, ModelState);
            }

        }
        // Get Hotels based on ratings from 1-5
        [HttpGet, Route("GetHotels/Rating/{rating_}")]
        public IActionResult GetHotelsRating(string rating_)
        {
            try
            {
                int rating;
                bool isNumeric = int.TryParse(rating_, out rating);
                if (isNumeric)
                {
                    if (rating == 1 || rating == 2 || rating == 3 || rating == 4 || rating == 5)
                    {
                        var getHotelsRating = _hnrepo.GetAvailableHotels_Rating(rating);


                        return Ok(getHotelsRating);
                    }
                    return NoContent();
                }

                ModelState.AddModelError("", $"Parameter Format Should be Number");
                return StatusCode(500, ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong while Fetching Hotels List With Rating");
                return StatusCode(500, ModelState);
            }

        }
        // Get all the available hotels
        [HttpGet, Route("GetHotels/Available")]
        public IActionResult GetHotelsAvailable()
        {
            try
            {
                AvaliabilitiesResponse getHotelsAvailable = new AvaliabilitiesResponse();
                getHotelsAvailable = _hnrepo.GetAvailableHotels_IsAvaialable();
                return Ok(getHotelsAvailable);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong while Fetching Hotels List Available");
                return StatusCode(500, ModelState);
            }

        }
        // Get The Hotel Rufundable Or Not Refundable from Hotel Name
        [HttpGet, Route("GetHotels/Refundable/{refund_}")]
        public IActionResult GetHotelsRefundable(string refund_)
        {
            try
            {
                int refund;
                bool isNumeric = int.TryParse(refund_, out refund);
                if (isNumeric)
                {
                    AvaliabilitiesResponse getHotelsAvailable = new AvaliabilitiesResponse();
                    // To Get Refundable Hotels
                    if (refund == 1)
                    {
                        getHotelsAvailable = _hnrepo.GetAvailableHotels_HotelRefundable(true);
                        return Ok(getHotelsAvailable);

                    }
                    // To Get Non Refundable Hotels
                    else if (refund == 0)
                    {
                        getHotelsAvailable = _hnrepo.GetAvailableHotels_HotelRefundable(false);
                        return Ok(getHotelsAvailable);
                    }
                    return NoContent();
                }

                ModelState.AddModelError("", $"Parameter Format Should be Number");
                return StatusCode(500, ModelState);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong while Fetching Hotels List Available");
                return StatusCode(500, ModelState);
            }

        }


        #endregion

    }
}
