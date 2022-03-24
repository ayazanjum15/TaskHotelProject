using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TaskHotelProjectApi.Models
{

	[XmlRoot(ElementName = "ROOM")]
	public class AvailableRoom
	{
		[JsonProperty("RoomCode")]
		[XmlElement(ElementName = "ROOMID")]
		public int ROOMID { get; set; }
		[JsonProperty("RoomName")]
		[XmlElement(ElementName = "ROOM_NAME")]
		public string ROOMNAME { get; set; }
		[JsonProperty("Occupancy")]
		[XmlElement(ElementName = "OCCUPANCY")]
		public int OCCUPANCY { get; set; }
		[JsonProperty("Status")]
		[XmlElement(ElementName = "ROOM_STATUS")]
		public string ROOMSTATUS { get; set; }
		[JsonProperty("Price")]
		[XmlElement(ElementName = "RULE_TEXT")]
		public string extra { get; set; }
	}

	[XmlRoot(ElementName = "ROOMS")]
	public class AvailableRooms
	{
		[JsonProperty("AvailableRoom")]
		[XmlElement(ElementName = "ROOM")]
		public List<AvailableRoom> ROOM { get; set; }
	}

	[XmlRoot(ElementName = "HOTEL")]
	public class Hotel
	{

		[XmlElement(ElementName = "ROOMS")]
		[JsonProperty("AvailableRooms")]
		public AvailableRooms ROOMS { get; set; }

		[XmlAttribute(AttributeName = "HOTEL_ID")]
		[JsonProperty("HotelCode")]
		public int HOTELID { get; set; }

		[XmlAttribute(AttributeName = "HOTEL_NAME")]
		[JsonProperty("HotelsName")]
		public string HOTELNAME { get; set; }

		[XmlAttribute(AttributeName = "LOCATION")]
		[JsonProperty("Location")]
		public string LOCATION { get; set; }

		[XmlAttribute(AttributeName = "RATING")]
		[JsonProperty("Rating")]
		public string RATING { get; set; }

		[XmlAttribute(AttributeName = "AVAILABLE")]
		[JsonProperty("IsReady")]
		public string AVAILABLE { get; set; }

		//[XmlAttribute(AttributeName = "ISRECOMMENDEDPRODUCT")]
		//[JsonProperty("Extra")]
		//public int ISRECOMMENDEDPRODUCT { get; set; }

		[XmlAttribute(AttributeName = "STARTING_PRICE")]
		[JsonProperty("LowestPrice")]
		public double STARTINGPRICE { get; set; }

		[XmlAttribute(AttributeName = "CURRENCY")]
		[JsonProperty("Currency")]
		public string CURRENCY { get; set; }

	}

	[XmlRoot(ElementName = "HOTELS")]
	public class Hotels
	{

		[XmlElement(ElementName = "HOTEL")]
		[JsonProperty("Hotel")]
		public List<Hotel> HOTEL { get; set; }
	}

	[XmlRoot(ElementName = "HOTEL_SEARCH_RESPONSE")]
	public class AvaliabilitiesResponse
	{
		[XmlElement(ElementName = "HOTELS")]
		[JsonProperty("Hotels")]
		public Hotels HOTELS { get; set; }
	}

	public class Root
	{
		public AvaliabilitiesResponse avaliabilitiesResponse { get; set; }
	}
}
