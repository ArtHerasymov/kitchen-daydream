using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using Server.DAL;

namespace Server.BL
{
    public interface ITicketBuilder
    {
        void SetTicketType();
        void AssignChiefs();
        void CalculateFinalPrice();
        void CalculateDeadline();
        void DecodeTitles();
        void GenerateTicketId();
    
        Ticket GetTicket();
    }

    public class ChineeseBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        Order order;

        public ChineeseBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }

        public void SetTicketType()
        {
            this.ticket.TicketType = "Chineese";
        }

        public void AssignChiefs()
        {
            ticket.Chief = "Drew";
            ticket.Suchief = "Danny";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(1).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();
            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            double discountAmount;
            if (discount == null)
                discountAmount = 0;
            else
                discountAmount = discount.DetermineDiscountAmount();

            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discountAmount * order.InitialPrice;
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "CHI" + new Random().Next();

        }

        public void DecodeTitles()
        {
            foreach(string item in items)
            {
                Item i = new Item();

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                } else if(item == "2")
                {
                    i.Title = "Chineese_Noodles";
                } else
                {
                    i.Title = "Chineese_Dessert";
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }
    }

    public class ItalianBuilder : ITicketBuilder
    {

        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;
        Order order;

        public ItalianBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }

        public void SetTicketType()
        {
            this.ticket.TicketType = "Italian";
        }
        public void AssignChiefs()
        {
            ticket.Chief = "Drew";
            ticket.Suchief = "Danny";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(0.5).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();
            Discount discount = (Discount)unit.DiscountRepository.Get(o => o.DiscountID == order.DiscountID);
            double discountAmount;
            if (discount == null)
                discountAmount = 0;
            else
                discountAmount = discount.DetermineDiscountAmount();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discountAmount * order.InitialPrice;
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();

                if (item == "4")
                {
                    i.Title = "Italian_Soup";
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                }
                else
                {
                    i.Title = "Italian_Dessert";
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "ITA" + new Random().Next();
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }
    }

    public class MixedBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;
        Order order;

        public void SetTicketType()
        {
            this.type = "MIXED";
        }

        public MixedBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }

        public void AssignChiefs()
        {
            ticket.Chief = "Drew";
            ticket.Suchief = "Danny";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(1.5).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();
            Discount discount = (Discount)unit.DiscountRepository.Get(o => o.DiscountID == order.DiscountID);
            double discountAmount;
            if (discount == null)
                discountAmount = 0;
            else
                discountAmount = discount.DetermineDiscountAmount();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discountAmount * order.InitialPrice;
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                }
                else if (item == "2")
                {
                    i.Title = "Chineese_Noodles";
                }
                else if(item == "3")
                {
                    i.Title = "Chineese_Dessert";
                }
                else if (item == "4")
                {
                    i.Title = "Italian_Soup";
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                }
                else
                {
                    i.Title = "Italian_Dessert";
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "MIX" + new Random().Next();

        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }
    }


    public class Dispatcher
    {

        public Ticket BuildTicket(ITicketBuilder _builder)
        {
            _builder.SetTicketType();
            _builder.AssignChiefs();
            _builder.CalculateFinalPrice();
            _builder.CalculateDeadline();
            _builder.GenerateTicketId();
            _builder.DecodeTitles();

            return _builder.GetTicket();
        }
    }
}