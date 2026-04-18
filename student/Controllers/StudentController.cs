using Microsoft.AspNetCore.Mvc;
using student.Service;
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
            return View(student);
        }
        public IActionResult Edit(int id)
        {
            var student = _service.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _service.UpdateStudent(student);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _service.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
