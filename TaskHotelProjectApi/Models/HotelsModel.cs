using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TaskHotelProjectApi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AvailableRooms
    {
        [XmlArray("ROOMS")]
        public List<AvailableRoom> AvailableRoom { get; set; }
    }

    public class AvailableRoom
    {
        [XmlElement("name")]
        public string RoomCode { get; set; }
        [XmlElement("name")]
        public string RoomName { get; set; }
        [XmlElement("name")]
        public string Occupancy { get; set; }
        [XmlElement("name")]
        public string Status { get; set; }
        [XmlElement("name")]
        public string Price { get; set; }
    }

    public class Hotel
    {
        [XmlElement("name")]
        public string HotelCode { get; set; }
        [XmlElement("name")]
        public string HotelsName { get; set; }
        [XmlElement("name")]
        public string Location { get; set; }
        [XmlElement("name")]
        public string Rating { get; set; }
        [XmlElement("name")]
        public string LowestPrice { get; set; }
        [XmlElement("name")]
        public string Currency { get; set; }
        [XmlElement("name")]
        public string IsReady { get; set; }
        [XmlElement("name")]
        public AvailableRooms AvailableRooms { get; set; }
    }
    public class Hotels
    {
        [XmlArray("HOTEL")]
        public List<Hotel> Hotel { get; set; }
    }

    public class AvaliabilitiesResponse
    {
        [XmlArray("HOTELS")]
        public Hotels Hotels { get; set; }
    }

    public class Root
    {
        [XmlArray("HOTEL_SEARCH_RESPONSE")]
        public AvaliabilitiesResponse avaliabilitiesResponse { get; set; }
    }

}
