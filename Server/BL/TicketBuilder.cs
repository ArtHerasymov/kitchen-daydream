﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;

namespace Server.BL
{
    public interface ITicketBuilder
    {
        void SetTicketType();
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
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;
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

        public void CalculateFinalPrice(double clientSidePrice)
        {
            Accounting accounting = new Accounting();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            this.finalPrice = accounting.GetSalesTax(clientSidePrice, this.ticket.TicketType);
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

        public void CalculateFinalPrice(double clientSidePrice)
        {
            Accounting accounting = new Accounting();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            this.finalPrice = accounting.GetSalesTax(clientSidePrice, this.ticket.TicketType);
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

        public void CalculateFinalPrice(double clientSidePrice)
        {
            Accounting accounting = new Accounting();
            accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
            this.finalPrice = accounting.GetSalesTax(clientSidePrice, this.ticket.TicketType);
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
        Order order;

        public void SetOrder(Order order)
        {
            this.order = order;
        }

        public Ticket BuiildTicket(ITicketBuilder _builder)
        {
            _builder.SetTicketType();
            _builder.AssignChiefs();
            _builder.CalculateFinalPrice(order.InitialPrice);
            _builder.CalculateDeadline();
            _builder.GenerateTicketId();
            _builder.DecodeTitles();

            return _builder.GetTicket();
        }
    }
}