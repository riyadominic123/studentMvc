using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using student.Model;
using student.Services;

namespace student.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly StudentApiService _apiService;

        public StudentController(StudentApiService apiService)
        {
            _apiService = apiService;
        }

        
        public async Task<IActionResult> Index()
        {
            var students = await _apiService.GetStudentsAsync();
            return View(students);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var classes = await _apiService.GetClassesAsync();

            ViewBag.ClassList = classes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            await _apiService.AddStudentAsync(student);

            return RedirectToAction("Index");
        }
    }
}