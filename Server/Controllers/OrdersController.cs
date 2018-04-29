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
            String[] itemIds = order.Items.Split(',');
            List<Item>items = (List<Item>)unitOfWork.ItemRepository.Get(o => o.OrderID == order.OrderID);

            foreach(Item item in items)
            {
                if (item == null)
                    return Json("false", JsonRequestBehavior.AllowGet);
                if(item.Status != "READY")
                    return Json("false", JsonRequestBehavior.AllowGet);
            }
            order.ChangeStatus();
            unitOfWork.OrderRepository.Update(order);
            return Json(order.Status , JsonRequestBehavior.AllowGet);
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
        public ActionResult Create([Bind(Include = "Waiter, Items, InitialPrice, DiscountID, Locale, Type")] Order order)
        {
            // Using : Proxy pattern
            order.Status = "IN_PROGRESS";
            DiscountProxy proxy = new DiscountProxy();
            proxy.AccountDiscount(unitOfWork, order.DiscountID);

            if (ModelState.IsValid)
            {
                // Using : Builder pattern
                Dispatcher dispatcher = new Dispatcher();
                ITicketBuilder builder = null;

                switch (order.Type)
                {
                    case "CHINEESE":
                        builder = new ChineeseBuilder(order);
                        break;
                    case "ITALIAN":
                        builder = new ItalianBuilder(order);
                        break;
                    case "MIXED":
                        builder = new MixedBuilder(order);
                        break;
                }

                Ticket ticket = dispatcher.BuildTicket(builder);

                // Using : Unit Of Work + Generic Repository pattern
                unitOfWork.OrderRepository.Insert(order);
                unitOfWork.TicketRepository.Insert(ticket);
             
                foreach (Item i in ticket.Items)
                    unitOfWork.ItemRepository.Insert(i);
                unitOfWork.Save();
            }
            return Json(order.OrderID , JsonRequestBehavior.AllowGet);
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
