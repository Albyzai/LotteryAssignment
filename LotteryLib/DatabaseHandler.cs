
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LotteryLib
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private string filePathSerials = "serials.bin";
        private string filePathTickets = "tickets.bin";

        public DatabaseHandler()
        {
            
        }

        public void GenerateSerialNumbers()
        {
            List<SerialNumber> SerialNumbers = new List<SerialNumber>();
            for(int i = 0; i < 99; i++)
            {
                SerialNumber serial = new SerialNumber();
                serial.key = Guid.NewGuid().ToString();

                SerialNumbers.Add(serial);

            }

            using(Stream stream = File.Open(filePathSerials, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, SerialNumbers);
                stream.Close();
            }
           
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            if (File.Exists(filePathTickets) && File.ReadAllBytes(filePathTickets).Length != 0)
            {
                using (Stream stream = File.Open(filePathTickets, FileMode.OpenOrCreate))
                {
                    var binaryFormatter = new BinaryFormatter();
                    while(stream.Position != stream.Length)
                    {
                        tickets.Add((Ticket)binaryFormatter.Deserialize(stream));
                    }
                    stream.Close();
                }
            }
            return tickets;
        }

        public IEnumerable<SerialNumber> GetAllSerialNumbers()
        {
            

            if (File.Exists(filePathSerials) && File.ReadAllBytes(filePathSerials).Length != 0)
            {
                using (Stream stream = File.Open(filePathSerials, FileMode.OpenOrCreate))
                {
                    var binaryFormatter = new BinaryFormatter();
                    var otherList = (List<SerialNumber>)binaryFormatter.Deserialize(stream);
                    stream.Close();

                    return otherList;
                }
            }
            return new List<SerialNumber>();
        }

        public void AddTicket(Ticket ticket)
        {

            using (Stream stream = File.Open(filePathTickets, FileMode.Append))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, ticket);
                stream.Close();
                //List<Ticket> _ticket = new List<Ticket>();
                //_ticket.Add(ticket);

                //var binaryFormatter = new BinaryFormatter();
                //binaryFormatter.Serialize(stream, _ticket);
                //stream.Close();
            }
        }

        public bool VerifyNewTicket(string input)
        {
            bool keyExists = false;
            bool keyNotUsed = true;
            List<SerialNumber> serials  = (List<SerialNumber>) GetAllSerialNumbers();
            foreach(var sn in serials)
            {
                if (sn.key.Equals(input))
                {
                    keyExists = true;
                    break;
                }

            }
            List<Ticket> tickets = (List<Ticket>)GetAllTickets();
            foreach(var ticket in tickets)
            {
                if (ticket.SerialNumber.Equals(input))
                {
                    keyNotUsed = false;
                    break;
                }
            }

            return keyExists && keyNotUsed;
            
        }
    }
}
