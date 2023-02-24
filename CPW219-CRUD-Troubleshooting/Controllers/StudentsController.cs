using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = context.Students.Find(id);

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Students.Update(p);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student? p = context.Students.Find(id);

            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Student from database

            Student? p = context.Students.Find(id);
            if (p != null)
            {
                context.Students.Remove(p);
                context.SaveChanges();
                TempData["Message"] = $"{p.Name} was Deleted successfully!";
            }


            return RedirectToAction("Index");
        }
    }
}
