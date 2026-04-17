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
            _service.AddStudent(student);
            return RedirectToAction("index");
        }
    }
}
