using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LotteryLib;
using LotteryUI.Models;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LotteryUI.Controllers
{
    public class TicketController : Controller
    {

        private IDatabaseHandler db;
        public TicketController(IDatabaseHandler databaseHandler)
        {
            db = databaseHandler;

            if(db.GetAllSerialNumbers().Count() < 1)
            {
                db.GenerateSerialNumbers();
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Ticket ticket)
        {


            if (ModelState.IsValid && db.VerifyNewTicket(ticket.SerialNumber))
            {
                var _ticket = new Ticket
                {
                    FirstName = ticket.FirstName,
                    SurName = ticket.SurName,
                    Email = ticket.Email,
                    PhoneNumber = ticket.PhoneNumber,
                    DateOfBirth = ticket.DateOfBirth,
                    SerialNumber = ticket.SerialNumber
                };

                db.AddTicket(_ticket);

                return RedirectToAction("Tickets");
            } else
            {
                ViewData.Add("SerialErrorMessage", "The serial key you entered is either invalid or already used");
            }

            return View();
        }

        [HttpGet("tickets")]
        public IActionResult Tickets()
        {
            if (db.GetAllTickets().Any())
            {
                var tickets = db.GetAllTickets();
                ViewBag.Tickets = tickets;
            }

            return View();
        }


        [HttpGet("serials")]
        public IActionResult SerialNumbers()
        {
            if (db.GetAllSerialNumbers().Any())
            {
                var serials = db.GetAllSerialNumbers();
                ViewBag.Serials = serials;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
