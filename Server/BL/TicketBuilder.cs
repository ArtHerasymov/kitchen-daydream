using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using Server.DAL;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Server.BL
{
    public interface ITicketBuilder
    {
        void SetTicketType();
        void AssignChiefs();
        void SetLocale();
        void CalculateFinalPrice();
        void CalculateDeadline();
        void DecodeTitles();
        void GenerateTicketId();
        void InitiateCookingAsync();
        Ticket GetTicket();
    }

    public class ChineeseBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket;
        Order order;
        UnitOfWork unit = new UnitOfWork();
        Accounting accounting;


        public ChineeseBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
            this.ticket = new Ticket();
        }
   
        public void SetTicketType()
        {
            this.ticket.TicketType = "CHINEESE";
        }

        public void AssignChiefs()
        {
            string contents = File.ReadAllText(@"E:\ChiefsAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Chief = deserializedValue["ChineeseChiefs"]["Chief"];
            ticket.Suchief = deserializedValue["ChineeseChiefs"]["SuChief"];
        }

        public void CalculateDeadline()
        {
            string contents = File.ReadAllText(@"E:\CookingTechnologyAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Deadline = DateTime.Now.
                AddHours((double)deserializedValue["Deadlines"]["ChineeseDeadline"])
                .ToString();
        }

        public void CalculateFinalPrice()
        {
            UnitOfWork unit = new UnitOfWork();

            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            if(discount != null)
            {
                discount.TranscribeState();
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice)
                    - discount.DetermineDiscountAmount() * order.InitialPrice;

                discount.Balance += ticket.FinalPrice;
            } else
            {
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice);
            }
           
            order.InitialPrice = ticket.FinalPrice;
            discount.State.NextState(discount);
            unit.Save();
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
                i.Status = "IN_PROGRESS";

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                    i.Code = 1;
                } else if(item == "2")
                {
                    i.Title = "Chineese_Noodles";
                    i.Code = 2;
                } else
                {
                    i.Title = "Chineese_Dessert";
                    i.Code = 3;
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
            unit.Save();
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }

        public void InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }
          
                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }

        public void SetLocale()
        {
            switch (order.Locale)
            {
                case "USA":
                    accounting = new AmericanTax();
                    break;
                case "EU":
                    accounting = new EuropeanTax();
                    break;
                default:
                    accounting = null;
                    break;
            }
        }
    }

    public class ItalianBuilder : ITicketBuilder
    {

        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        Order order;
        Accounting accounting;

        public ItalianBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }
      
        public void SetTicketType()
        {
            this.ticket.TicketType = "ITALIAN";
        }
        public void AssignChiefs()
        {
            string contents = File.ReadAllText(@"E:\ChiefsAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Chief = deserializedValue["ItalianChiefs"]["Chief"];
            ticket.Suchief = deserializedValue["ItalianChiefs"]["SuChief"];
        }

        public void CalculateDeadline()
        {

            string contents = File.ReadAllText(@"E:\CookingTechnologyAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Deadline = DateTime.Now.
                AddHours((double)deserializedValue["Deadlines"]["ItalianDeadline"])
                .ToString();
        }

        public void CalculateFinalPrice()
        {

            UnitOfWork unit = new UnitOfWork();

            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            if (discount != null)
            {
                discount.TranscribeState();
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice)
                    - discount.DetermineDiscountAmount() * order.InitialPrice;

                discount.Balance += ticket.FinalPrice;
            }
            else
            {
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice);
            }

            order.InitialPrice = ticket.FinalPrice;
            discount.State.NextState(discount);
            unit.Save();
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();
                i.Status = "IN_PROGRESS";

                if (item == "4")
                {
                    i.Title = "Italian_Soup";
                    i.Code = 4;
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                    i.Code = 5;
                }
                else
                {
                    i.Title = "Italian_Dessert";
                    i.Code = 6;
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

        public void InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }

                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }

        public void SetLocale()
        {
            switch (order.Locale)
            {
                case "USA":
                    accounting = new AmericanTax();
                    break;
                case "EU":
                    accounting = new EuropeanTax();
                    break;
                default:
                    accounting = null;
                    break;
            }
        }
    }

    public class MixedBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        Order order;
        Accounting accounting;
    
        public void SetTicketType()
        {
            this.ticket.TicketType = "MIXED";
        }

        public MixedBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }

        public void AssignChiefs()
        {
            string contents = File.ReadAllText(@"E:\ChiefsAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Chief = deserializedValue["ItalianChiefs"]["Chief"];
            ticket.Suchief = deserializedValue["ChineeseChiefs"]["Chief"];
        }

        public void CalculateDeadline()
        {

            string contents = File.ReadAllText(@"E:\CookingTechnologyAPI.json");
            dynamic deserializedValue = JsonConvert.DeserializeObject(contents);
            ticket.Deadline = DateTime.Now.
                AddHours((double)deserializedValue["Deadlines"]["MixedDeadline"])
                .ToString();
        }

        public void CalculateFinalPrice()
        {
            UnitOfWork unit = new UnitOfWork();

            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            if (discount != null)
            {
                discount.TranscribeState();
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice)
                    - discount.DetermineDiscountAmount() * order.InitialPrice;

                discount.Balance += ticket.FinalPrice;
                discount.State.NextState(discount);

            }
            else
            {
                ticket.FinalPrice = order.InitialPrice + accounting.GetAdditionalPrice(order.Type, order.InitialPrice);
            }

            order.InitialPrice = ticket.FinalPrice;
            unit.Save();
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();
                i.Status = "IN_PROGRESS";

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                    i.Code = 1;
                }
                else if (item == "2")
                {
                    i.Title = "Chineese_Noodles";
                    i.Code = 2;
                }
                else if(item == "3")
                {
                    i.Title = "Chineese_Dessert";
                    i.Code = 3;
                }
                else if (item == "4")
                {
                    i.Title = "Italian_Soup";
                    i.Code = 4;
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                    i.Code = 5;
                }
                else
                {
                    i.Title = "Italian_Dessert";
                    i.Code = 6;
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

        public void InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateSoupCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }

                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }

        public void SetLocale()
        {
            switch (order.Locale)
            {
                case "USA":
                    accounting = new AmericanTax();
                    break;
                case "EU":
                    accounting = new EuropeanTax();
                    break;
                default:
                    accounting = null;
                    break;
            }
        }
    }

    public class Dispatcher
    {
        public Ticket BuildTicket(ITicketBuilder _builder)
        {
            _builder.SetTicketType();
            _builder.AssignChiefs();
            _builder.SetLocale();
            _builder.CalculateFinalPrice();
            _builder.CalculateDeadline();
            _builder.GenerateTicketId();
            _builder.DecodeTitles();
            _builder.InitiateCookingAsync();

            return _builder.GetTicket();
        }
    }
}