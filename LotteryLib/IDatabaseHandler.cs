using System;
using System.Collections.Generic;
using System.Text;

namespace LotteryLib
{
    public interface IDatabaseHandler
    {
        void GenerateSerialNumbers();
        IEnumerable<Ticket> GetAllTickets();
        IEnumerable<SerialNumber> GetAllSerialNumbers();
        void AddTicket(Ticket ticket);
        bool VerifyNewTicket(string input);
    }
}
