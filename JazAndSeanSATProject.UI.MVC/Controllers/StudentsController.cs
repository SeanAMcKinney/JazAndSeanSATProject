using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JazAndSeanSATProject.DATA.EF;
using PagedList;
using PagedList.Mvc;
using JazAndSeanSATProject.UI.MVC.Models;
using System.Drawing;
using MVC3.UI.MVC.Utilities;

namespace JazAndSeanSATProject.UI.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private SATDatabaseEntities db = new SATDatabaseEntities();

        // GET: Students
        [Authorize(Roles = "Admin, Scheduler")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin, Scheduler")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase photoUrl)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string file = "NoImage.png";

                if (photoUrl != null)
                {
                    file = photoUrl.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".jif" };

                    if (goodExts.Contains(ext.ToLower()) && photoUrl.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/img/");

                        Image convertedImage = Image.FromStream(photoUrl.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }

                    student.PhotoUrl = file;
                }

                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase photoUrl)
        {
            #region File Upload

            string file = "NoImage.png";

            if (photoUrl != null)
            {
                file = photoUrl.FileName;

                string ext = file.Substring(file.LastIndexOf('.'));

                string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                if (goodExts.Contains(ext.ToLower()) && photoUrl.ContentLength <= 4194304)
                {
                    file = Guid.NewGuid() + ext;

                    #region Resize Image

                    string savePath = Server.MapPath("~/Content/img/");

                    Image convertedImage = Image.FromStream(photoUrl.InputStream);

                    int maxImageSize = 500;

                    int maxThumbSize = 100;

                    ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                    #endregion

                    if (student.PhotoUrl != null && student.PhotoUrl != "NoImage.png")
                    {
                        string path = Server.MapPath("/Content/store_images/");
                        ImageUtility.Delete(path, student.PhotoUrl);
                    }

                    student.PhotoUrl = file;
                }
            }

            #endregion

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
