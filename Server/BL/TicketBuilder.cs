using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;

namespace Server.BL
{
    public interface ITicketBuilder
    {
        void SetTicketType();
        void FormItems();
        void AssignChiefs();
        void CalculateFinalPrice(double clientSidePrice);
        void CalculateDeadline();
        void DecodeTitles();
        void GenerateTicketId();

        Ticket GetTicket();
    }

    public class ChineeseBuilder : ITicketBuilder
    {
        String[] items;
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;

        public ChineeseBuilder(string [] items)
        {
            this.items = items;
        }

        public void SetTicketType()
        {
            this.ticket.TicketType = "Chineese";
        }

        public void AssignChiefs()
        {
            throw new NotImplementedException();
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(1).ToString();
        }

        public void CalculateFinalPrice(double clientSidePrice)
        {
            Accounting accounting = new Accounting();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            this.finalPrice = accounting.GetSalesTax(clientSidePrice, this.ticket.TicketType);
        }

        public void DecodeTitles()
        {
        }

        public void FormItems()
        {
            throw new NotImplementedException();
        }

        public void GenerateTicketId()
        {
            string ticketID = "CHI" + new Random().Next();
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }
    }
    public class ItalianBuilder : ITicketBuilder { }
    public class MixedBuilder : ITicketBuilder { }


    public class Dispatcher
    {
        public void BuiildTicket(ITicketBuilder _builder)
        {
            _builder.FormItems();
            _builder.AssignChiefs();

        }
    }
}