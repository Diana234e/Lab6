using Lab_6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_6.Controllers
{
    public class HomeController : Controller
    {
        private TeacherContext db = new TeacherContext();
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        public ActionResult Institute()
        {
            return View(db.Institutes.ToList());
        }

        public ActionResult Cathedra()
        {
            return View(db.Cathedras.ToList());
        }

        public ActionResult Details(int? id)
        {
            Institute institute = db.Institutes.Find(id);
            if (institute == null)
            {
                return HttpNotFound();
            }
            return View(institute
                );
        }
        public ActionResult Edit(int? id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            Teacher newTeacher = db.Teachers.Find(teacher.ID);
            newTeacher.FirstName = teacher.FirstName;
            newTeacher.LastName = teacher.LastName;
            newTeacher.CathedraID = teacher.CathedraID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTeacher(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateInstitute()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateInstitute(Institute institute)
        {
            db.Institutes.Add(institute);
            db.SaveChanges();

            return RedirectToAction("Institute");
        }

        [HttpGet]
        public ActionResult CreateCathedra()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCathedra(Cathedra cathedra)
        {
            db.Cathedras.Add(cathedra);
            db.SaveChanges();

            return RedirectToAction("Cathedra");
        }

        public ActionResult EditCathedra(int? id)
        {
            Cathedra cathedra = db.Cathedras.Find(id);
            if (cathedra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Institute = db.Institutes.ToList();
            return View(cathedra);
        }

        [HttpPost]
        public ActionResult EditCathedra(Cathedra cathedra, int[] selectedInstitute)
        {
            Cathedra newCathedra = db.Cathedras.Find(cathedra.ID);
            newCathedra.Name = cathedra.Name;

            newCathedra.Teacher.Clear();
            if (selectedInstitute != null)
            {

                foreach (var c in db.Teachers.Where(co => selectedInstitute.Contains(co.ID)))
                {
                    newCathedra.Teacher.Add(c);
                }
            }

            db.Entry(newCathedra).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Cathedra");
        }

        [HttpGet]
        public ActionResult DeleteTeacher(int id)
        {
            Teacher b = db.Teachers.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("DeleteTeacher")]
        public ActionResult DeleteStConfirmed(int id)
        {
            Teacher b = db.Teachers.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Teachers.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteInstitute(int id)
        {
            Institute inst = db.Institutes.Find(id);
            if (inst == null)
            {
                return HttpNotFound();
            }
            return View(inst);
        }
        [HttpPost, ActionName("DeleteInstitute")]
        public ActionResult DeleteSbConfirmed(int id)
        {
            Institute inst = db.Institutes.Find(id);
            if (inst == null)
            {
                return HttpNotFound();
            }
            db.Institutes.Remove(inst);
            db.SaveChanges();
            return RedirectToAction("Institute");
        }

        [HttpGet]
        public ActionResult DeleteCathedra(int id)
        {
            Cathedra cath = db.Cathedras.Find(id);
            if (cath == null)
            {
                return HttpNotFound();
            }
            return View(cath);
        }
        [HttpPost, ActionName("DeleteCathedra")]
        public ActionResult DeleteGrConfirmed(int id)
        {
            Cathedra cath = db.Cathedras.Find(id);
            if (cath == null)
            {
                return HttpNotFound();
            }
            db.Cathedras.Remove(cath);
            db.SaveChanges();
            return RedirectToAction("Cathedra");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
