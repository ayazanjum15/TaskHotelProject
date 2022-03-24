using TaskHotelProjectApi.Models;

namespace TaskHotelProjectApi.Repository.IRepository
{
    public interface IHotelNavigation
    {
        AvaliabilitiesResponse GetAvailableHotels();
        AvaliabilitiesResponse GetAvailableHotels_Rating(int rating);
        AvaliabilitiesResponse GetAvailableHotels_IsAvaialable();
        AvaliabilitiesResponse GetAvailableHotels_HotelRefundable(bool isRefundable);
    }
}
