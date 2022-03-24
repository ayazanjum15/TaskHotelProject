namespace TaskHotelProject.Models
{
	public class AvailableRoom
	{
		public int ROOMID { get; set; }
		public string ROOMNAME { get; set; }
		public int OCCUPANCY { get; set; }
		public string ROOMSTATUS { get; set; }
		public string extra { get; set; }
	}

	public class AvailableRooms
	{
		public List<AvailableRoom> ROOM { get; set; }
	}

	public class Hotel
	{

		public AvailableRooms ROOMS { get; set; }

		public int HOTELID { get; set; }

		public string HOTELNAME { get; set; }

		public string LOCATION { get; set; }

		public string RATING { get; set; }

		public string AVAILABLE { get; set; }

		public double STARTINGPRICE { get; set; }

		public string CURRENCY { get; set; }

	}

	public class Hotels
	{

		public List<Hotel> HOTEL { get; set; }
	}

	public class AvaliabilitiesResponse
	{
		public Hotels HOTELS { get; set; }
	}

}
