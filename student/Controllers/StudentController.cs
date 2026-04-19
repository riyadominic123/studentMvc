using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using student.Model;
using student.Service;

namespace student.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var student = _service.GetStudents();
            return View(student);
        }
        public IActionResult Create()
        {
          
            ViewBag.ClassList = _service.GetClasses()
               .Select(c => new SelectListItem
               {
                     Value = c.Id.ToString(),
                     Text = c.Name
               }).ToList();
            return View();

        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student.Name == student.Age.ToString())
            {
                ModelState.AddModelError("Name", "Name cannot be same as Age");
            }
            if (ModelState.IsValid)
            {
                _service.AddStudent(student);
                return RedirectToAction("index");
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine(error.Key);
                }
            }
            ViewBag.ClassList = _service.GetClasses()
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
            return View(student);
        }
        public IActionResult Edit(int id)
        {
            var student = _service.GetStudentById(id);
            ViewBag.ClassList = _service.GetClasses()
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateStudent(student);
                return RedirectToAction("Index");
            }

            // reload dropdown
            ViewBag.ClassList = _service.GetClasses()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(student);
        }
        public IActionResult Delete(int id)
        {
            _service.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
