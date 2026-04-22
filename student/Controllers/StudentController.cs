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
            var classes = await _apiService.GetClassesAsync();

            foreach (var student in students)
            {
                student.Class = classes.FirstOrDefault(c => c.Id == student.ClassId);
            }
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
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _apiService.GetStudentByIdAsync(id);
            var classes = await _apiService.GetClassesAsync();
            ViewBag.ClassList = classes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return View(student);

        
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if(ModelState.IsValid)
            {
                await _apiService.UpdateStudentAsync(student.Id, student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _apiService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }
    }
}