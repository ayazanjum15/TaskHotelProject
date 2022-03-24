using Newtonsoft.Json;
using TaskHotelProject.Models;
using TaskHotelProject.Repository.IRepository;

namespace TaskHotelProject.Repository
{
    public class HotelApi : IHotelApi
    {
        public HttpClient client { get; set; }

        public HotelApi()
        {
            client = new HttpClient();
            // Set The Base Address
            client.BaseAddress = new Uri("https://localhost:7091");

        }
        public async Task<AvaliabilitiesResponse> GetHotelsApi()
        {
            // Function to get the list of all the hotels
            AvaliabilitiesResponse response = new AvaliabilitiesResponse();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api");

                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<AvaliabilitiesResponse>(results);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
            
        }
        // Api Request To Get Filtered Hotel List For Rating
        public async Task<AvaliabilitiesResponse> GetHotelsApi_Ratings(int rating)
        {
            AvaliabilitiesResponse response = new AvaliabilitiesResponse();
            try
            {
                string getrefundableuri = "api/GetHotels/Rating/" + rating.ToString();
                HttpResponseMessage res = await client.GetAsync(getrefundableuri);

                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<AvaliabilitiesResponse>(results);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        // Api Request To Get Filtered Hotel List For Availablity
        public async Task<AvaliabilitiesResponse> GetHotelsApi_Available()
        {
            AvaliabilitiesResponse response = new AvaliabilitiesResponse();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/GetHotels/Available");

                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<AvaliabilitiesResponse>(results);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        // Api Request To Get Filtered Hotel List For Refundablity
        public async Task<AvaliabilitiesResponse> GetHotelsApi_IsRefundable(int refund)
        {
            AvaliabilitiesResponse response = new AvaliabilitiesResponse();
            try
            {
                string getrefundableuri = "api/GetHotels/Refundable/" + refund.ToString();
                HttpResponseMessage res = await client.GetAsync(getrefundableuri);

                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<AvaliabilitiesResponse>(results);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
