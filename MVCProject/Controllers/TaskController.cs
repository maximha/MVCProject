using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class TaskController : Controller
    {
        private social_workout_app_dbEntities db = new social_workout_app_dbEntities();

        //
        // GET: /Task/

        public ActionResult Index()
        {
            var tasks = db.tasks.Include(t => t.workout);
            return View(tasks.ToList());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(string id = null)
        {
            task task = db.tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            ViewBag.workoutName = new SelectList(db.workouts, "workoutName", "workoutName");
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(task task)
        {
            if (ModelState.IsValid)
            {
                db.tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.workoutName = new SelectList(db.workouts, "workoutName", "workoutName", task.workoutName);
            return View(task);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(string id = null)
        {
            task task = db.tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.workoutName = new SelectList(db.workouts, "workoutName", "userName", task.workoutName);
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workoutName = new SelectList(db.workouts, "workoutName", "userName", task.workoutName);
            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(string id = null)
        {
            task task = db.tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            task task = db.tasks.Find(id);
            db.tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}