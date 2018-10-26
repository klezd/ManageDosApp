
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageDos.Models;

namespace ManageDos.Controllers
{
    [Authorize]
    public class ToDoItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoItems
        public ActionResult Index(int? ListId)
        {
            ViewBag.ListId = ListId;
            return View();
        }
        private IEnumerable<ToDoItem> GetMyToDoes(int? ListId)
        {

            System.Diagnostics.Debug.WriteLine(ListId);

            ToDoList toDoList = db.ToDoLists.FirstOrDefault
                (x => x.ID == ListId);
            System.Diagnostics.Debug.WriteLine(toDoList);

            
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);


            IEnumerable<ToDoItem> MyToDoes = db.ToDoItems.ToList().Where((x => x.User == currentUser &&  x.List == toDoList));

            int completeCount = 0;
            foreach (ToDoItem toDoItem in MyToDoes)
            {
                if (toDoItem.IsDone)
                {
                    completeCount++;
                }
            }

            ViewBag.Percent = Math.Round(100f * ((float)completeCount / (float)MyToDoes.Count()));

            return MyToDoes;
        }

        public ActionResult BuildToDoTable(int? ListId)
        {
            
            return PartialView("_ToDoTable", GetMyToDoes(ListId));
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "ID,Content,DueDate")] ToDoItem toDoItem, int? ListId)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId );
                ToDoList list = db.ToDoLists.FirstOrDefault
                    (x => x.ID == ListId);
                toDoItem.User = currentUser;
                toDoItem.List = list;
                toDoItem.IsDone = false;
                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();
                
            }
            // TODO
            return PartialView("_ToDoTable", GetMyToDoes(ListId));
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            if (toDoItem.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,IsDone,DueDate")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        [HttpPost]
      
        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDoItem.IsDone = value;
                db.Entry(toDoItem).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ToDoTable", GetMyToDoes(toDoItem.List.ID));
            }
           
        }


        // GET: ToDoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            db.ToDoItems.Remove(toDoItem);
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
