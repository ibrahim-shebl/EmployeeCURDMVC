using EmployeeCURD.Data;
using EmployeeCURD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCURD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            var Result = _context.Employees.Include(x => x.Department)
               .OrderBy(x => x.EmployeeName).ToList();
            return View(Result);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            UploadImage(model);

            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }
        private void UploadImage(Employee model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {//@"wwwroot/"
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.ImageUser = ImageName;
            }
            else if (model.ImageUser == null && model.EmployeeId == null)
            {
                model.ImageUser = "DefultImage.jpg";
            }
            else
            {
                model.ImageUser = model.ImageUser;
            }
        }
        public IActionResult Edit(int? Id)
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            var result = _context.Employees.Find(Id);
            return View("Create", result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            UploadImage(model);
            if (model != null)
            {
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View(model);
        }
        public IActionResult Delete(int? Id)
        {
            var result = _context.Employees.Find(Id);
            if (result != null)
            {
                _context.Employees.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
