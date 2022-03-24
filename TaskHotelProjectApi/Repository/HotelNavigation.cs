using Newtonsoft.Json;
using System.Xml.Serialization;
using TaskHotelProjectApi.Models;
using TaskHotelProjectApi.Repository.IRepository;

namespace TaskHotelProjectApi.Repository
{
    public class HotelNavigation : IHotelNavigation
    {
        public AvaliabilitiesResponse GetAvailableHotels()
        {
            AvaliabilitiesResponse response = new AvaliabilitiesResponse();
            // Directory to Get the Containing Folder of Json and XML Files
            string pathdir = Environment.CurrentDirectory;
            if (!Directory.Exists(pathdir))
            {
                return response;
            }
            pathdir = pathdir + @"\Data";

            //DirectoryInfo d = new DirectoryInfo(@"C:\Users\ayaza\source\repos\TaskHotelProject\TaskHotelProjectApi\Data");
            DirectoryInfo d = new DirectoryInfo(pathdir);

            //Deserialize XML File
            AvaliabilitiesResponse ExtractedDataxml = DeserealizeXmlData(d);

            //Deserialize Json File
            Root ExtractedDatajson = DeserializeJsonData(d);

            response = MergeJsonModeltoXmlModel(ExtractedDataxml, ExtractedDatajson);
            return response;
        }


        public AvaliabilitiesResponse DeserealizeXmlData(DirectoryInfo d)
        {
            AvaliabilitiesResponse ExtractedDataxml = new AvaliabilitiesResponse();
            foreach (var file in d.GetFiles("*.xml"))
            {

                XmlSerializer serializer = new XmlSerializer(typeof(AvaliabilitiesResponse));
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    ExtractedDataxml = (AvaliabilitiesResponse)serializer.Deserialize(reader);
                }
            }
            return ExtractedDataxml;
        }

        public Root DeserializeJsonData(DirectoryInfo d)
        {
            Root ExtractedDatajson = new Root();
            foreach (var file in d.GetFiles("*.json"))
            {

                using (StreamReader r = new StreamReader(file.FullName))
                {
                    string json = r.ReadToEnd();
                    if (json != null)
                    {
                        ExtractedDatajson = JsonConvert.DeserializeObject<Root>(json);
                    }
                }
            }
            return ExtractedDatajson;
        }

        // merge the extracted json model into the extracted xml model to combine for a final output
        public AvaliabilitiesResponse MergeJsonModeltoXmlModel(AvaliabilitiesResponse xmlModel, Root jsonModel )
        {
            
            foreach (var item in jsonModel.avaliabilitiesResponse.HOTELS.HOTEL)
            {
                Hotel hotel = new Hotel();
                hotel.HOTELID = item.HOTELID;
                hotel.HOTELNAME = item.HOTELNAME;
                hotel.LOCATION = item.LOCATION;
                hotel.RATING = item.RATING;
                if (item.AVAILABLE.ToLower().Contains("true"))
                    hotel.AVAILABLE = "Yes";
                else
                    hotel.AVAILABLE = "No";
                hotel.CURRENCY = item.CURRENCY;
                hotel.STARTINGPRICE = item.STARTINGPRICE;
                AvailableRooms availableRooms = new AvailableRooms();
                List<AvailableRoom> availableRoom = new List<AvailableRoom>();
                foreach (var it in item.ROOMS.ROOM)
                {

                    AvailableRoom room = new AvailableRoom();
                    room.ROOMID = it.ROOMID;
                    room.ROOMNAME = it.ROOMNAME;
                    room.OCCUPANCY = it.OCCUPANCY;
                    if (it.ROOMSTATUS.ToLower().Contains("true"))
                        room.ROOMSTATUS = "Available";
                    else
                        room.ROOMSTATUS = "Not Available";
                    room.extra = it.extra;
                    availableRoom.Add(room);
                }
                availableRooms.ROOM = availableRoom;
                hotel.ROOMS = availableRooms;

                xmlModel.HOTELS.HOTEL.Add(hotel);


            }

            return xmlModel;
        }

        // Function To Get Hotels Based On rating 1-5
        public AvaliabilitiesResponse GetAvailableHotels_Rating(int rating)
        {
            string srating = rating.ToString();
            var allhotelsdata = GetAvailableHotels();
            List<Hotel> hotels = allhotelsdata.HOTELS.HOTEL.Where(x=> x.RATING.Contains(srating)).ToList();

            Hotels h = new Hotels();
            AvaliabilitiesResponse reponse = new AvaliabilitiesResponse();
            h.HOTEL = hotels;
            reponse.HOTELS = h;


            return reponse;
        }
        // Functio To Get Hotels Available
        public AvaliabilitiesResponse GetAvailableHotels_IsAvaialable()
        {
            var allhotelsdata = GetAvailableHotels();
            List<Hotel> hotels = allhotelsdata.HOTELS.HOTEL.Where(x => x.AVAILABLE.ToLower().Contains("yes")).ToList();

            Hotels h = new Hotels();
            AvaliabilitiesResponse reponse = new AvaliabilitiesResponse();
            h.HOTEL = hotels;
            reponse.HOTELS = h;


            return reponse;
        }
        // Function To Get Hotel Based On Refundabel Or Non Refundable
        public AvaliabilitiesResponse GetAvailableHotels_HotelRefundable(bool isRefundable)
        {
            var allhotelsdata = GetAvailableHotels();
            List<Hotel> hotels = new List<Hotel>();
            // Refundable Hotels
            if (isRefundable)
            {
                hotels = allhotelsdata.HOTELS.HOTEL.Where(x => !x.HOTELNAME.ToUpper().Contains("NON REFUNDABLE ROOM")).ToList();
                
            }
            // Non Refundable Hotels
            else if(!isRefundable)
            {
                hotels = allhotelsdata.HOTELS.HOTEL.Where(x => x.HOTELNAME.ToUpper().Contains("NON REFUNDABLE ROOM")).ToList();
            }

            Hotels h = new Hotels();
            AvaliabilitiesResponse reponse = new AvaliabilitiesResponse();
            h.HOTEL = hotels;
            reponse.HOTELS = h;


            return reponse;
        }
    }
}
