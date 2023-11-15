using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MimeKit;using MailKit.Net.Smtp;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task<string> SendEmail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Test Message", "moatazfakh@gmail.com"));
            message.To.Add((MailboxAddress.Parse("exuberantma@gmail.com")));
            message.Subject = "Test From My Project in ASP.NET core MVC";
            message.Body = new TextPart("plain")
            {
                Text = "Welcome to My App!"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("moatazfakh@gmail.com", "zccpbqmikzsjcztu");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                catch{ }
            }

            return "Ok";
		}

		public IActionResult Delete(int id) 
        { 
            var hotelDelete = _context.hotel.SingleOrDefault(x=> x.Id == id);
            if (hotelDelete != null) 
            { 
                _context.hotel.Remove(hotelDelete);
                _context.SaveChanges();
                TempData["Delete"] = "Ok";
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteRoom(int id)
        {
            var roomDelete = _context.rooms.SingleOrDefault(x => x.Id == id);
            if (roomDelete != null)
            {
                _context.rooms.Remove(roomDelete);
                _context.SaveChanges();
                TempData["Delete"] = "Ok";
            }

            return RedirectToAction("Rooms");
        }

        public IActionResult DeleteRoomDetails(int id)
        {
            var roomDetailDelete = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            if (roomDetailDelete != null)
            {
                _context.roomDetails.Remove(roomDetailDelete);
                _context.SaveChanges();
                TempData["Delete"] = "Ok";
            }

            return RedirectToAction("RoomDetails");
        }

        //public IActionResult RoomDelete(int id)
        //{
        //	var roomDelete = _context.hotel.SingleOrDefault(x => x.Id == id);
        //	if (roomDelete != null)
        //	{
        //		_context.rooms.Remove(roomDelete);
        //		_context.SaveChanges();
        //		TempData["roomDelete"] = "Ok";
        //	}

        //	return RedirectToAction("Rooms");
        //}

        public IActionResult CreateNewRooms(Rooms rooms)
        {
           
            
                _context.rooms.Add(rooms);
                _context.SaveChanges();
			    return RedirectToAction("Rooms");
           
        }

		public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
		{


			_context.roomDetails.Add(roomDetails);
			_context.SaveChanges();
			return RedirectToAction("RoomDetails");

		}

		[HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            var hotel = _context.hotel.Where(x=>x.City.Equals(city));
            return View(hotel);
        }
        
		public async Task<IActionResult> RoomDetails()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;

			var roomDetails = _context.roomDetails.ToList();
			return View(roomDetails);
		}
		public IActionResult Rooms()
		{
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            //ViewBag.currentuser = Request.Cookies["UserName"]; //passing the saved data to rooms with the word [Request] / using ["UserName"] ? as a key
            ViewBag.currentuser = HttpContext.Session.GetString("UserName"); //Another way 

			var rooms = _context.rooms.ToList();
            return View(rooms);
		}

        public IActionResult Edit(int id)
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(hoteledit);
        }

        public IActionResult EditRooms(int id)
        {
            var editRooms = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(editRooms);
        }

        public IActionResult EditRoomDetails(int id)
        {
            var editRoomDetails = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(editRoomDetails);
        }


        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit");
        }
        public IActionResult UpdateRoom(Rooms room)
        {
            if (ModelState.IsValid)
            {
                _context.rooms.Update(room);
                _context.SaveChanges();
                return RedirectToAction("Rooms");
            }

            return View("EditRooms");
        }

        public IActionResult UpdateRoomDetails(RoomDetails roomDetail)
		{
			if (ModelState.IsValid)
			{
				_context.roomDetails.Update(roomDetail);
				_context.SaveChanges();
				return RedirectToAction("RoomDetails");
			}

			return View("EditRoomDetails");
		}

		//zccpbqmikzsjcztu
		[Authorize]
		public IActionResult Index()
        {
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.currentuser = currentuser;
            CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddMinutes(20);
            
            //Response.Cookies.Append("UserName", currentuser, option); //saving to cookies with the word [Response]
            HttpContext.Session.SetString("UserName", currentuser); //another way as the cookies

			var hotel = _context.hotel.ToList();
            return View(hotel);
        }

        public IActionResult CreateNewHotel(Hotel hotels)
        {
            if(ModelState.IsValid) 
            { 
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel = _context.hotel.ToList();
            return View("index", hotel);
        }
    }
}
