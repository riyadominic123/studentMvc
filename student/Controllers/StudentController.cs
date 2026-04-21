using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using student.Model;
using student.Service;
using System.Security.Claims;

namespace student.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(IStudentService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            if (User.IsInRole("Admin"))
            {
                return View(_service.GetStudents());
            }

           
            var classId = _service.GetClassIdByUserId(userId);

            if (classId == null)
            {
                return View(new List<Student>());
            }

            var students = _service.GetStudents()
                .Where(s => s.ClassId == classId)
                .ToList();

            return View(students);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (student.Name == student.Age.ToString())
            {
                ModelState.AddModelError("Name", "Name cannot be same as Age");
            }

            if (ModelState.IsValid)
            {
                
                _service.AddStudent(student);

                
                var user = new IdentityUser
                {
                    UserName = student.Name + "@mail.com",
                    Email = student.Name + "@mail.com"
                };

                var result = await _userManager.CreateAsync(user, "Test@123");

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "Student");


                    _service.AddUserClass(new UserClass
                    {
                        UserId = user.Id,
                        ClassId = student.ClassId
                    });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                return RedirectToAction("Index");
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
