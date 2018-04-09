using System;
using Server.Models;
using Server.DAL;

namespace Server.DAL
{
    public class UnitOfWork : IDisposable
    {
        private TicketContext context = new TicketContext();
        private GenericRepository<Ticket> ticketRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<Item> itemRepository;

        public GenericRepository<Item> ItemRepository
        {
            get
            {
                if(this.itemRepository == null)
                {
                    this.itemRepository = new GenericRepository<Item>(context);
                }
                return itemRepository;
            }
        }

        public GenericRepository<Ticket> TicketRepository
        {
            get
            {

                if (this.ticketRepository == null)
                {
                    this.ticketRepository = new GenericRepository<Ticket>(context);
                }
                return ticketRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}