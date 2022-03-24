using TaskHotelProject.Models;

namespace TaskHotelProject.Repository.IRepository
{
    public interface IHotelApi
    {
        Task<AvaliabilitiesResponse> GetHotelsApi();
        Task<AvaliabilitiesResponse> GetHotelsApi_Ratings(int rating);
        Task<AvaliabilitiesResponse> GetHotelsApi_Available();
        Task<AvaliabilitiesResponse> GetHotelsApi_IsRefundable(int refund);
    }
}
