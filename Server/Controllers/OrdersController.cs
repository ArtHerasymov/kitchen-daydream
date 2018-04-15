using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Server.DAL;
using Server.Models;
using Server.BL;

namespace Server.Controllers
{
    public class OrdersController : Controller
    {
        private OrderContext db = new OrderContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = unitOfWork.OrderRepository.Get();
            return Json(orders, JsonRequestBehavior.AllowGet);
           // return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return Json(order, JsonRequestBehavior.AllowGet);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "Status");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Waiter, Items, InitialPrice, DiscountID")] Order order)
        {
            order.Status = "IN_PROGRESS";
            DiscountProxy proxy = new DiscountProxy();
            Discount discount = proxy.AccessObject(unitOfWork, order.DiscountID);
            order.DiscountID = discount.DiscountID;

            if (ModelState.IsValid)
            {
                Dispatcher dispatcher = new Dispatcher();
                ITicketBuilder builder = new ChineeseBuilder(order);

                Ticket ticket = dispatcher.BuildTicket(builder);

                //Saving changes to db
             //   proxy.SaveDiscount();
                unitOfWork.OrderRepository.Insert(order);
                unitOfWork.TicketRepository.Insert(ticket);
             
                foreach (Item i in ticket.Items)
                    unitOfWork.ItemRepository.Insert(i);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(order);
        }
    
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "Status");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Waiter,Status,TicketID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketID", "Status");
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
