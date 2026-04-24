using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using student.Model;
using student.Services;

namespace student.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Student student, IFormFile imageFile)
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("❌ MODEL INVALID");

                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }

                    // reload dropdown
                    var classes = await _apiService.GetClassesAsync();
                    ViewBag.ClassList = classes.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                    return View(student);
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/");


                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    student.ImagePath = "images/" + fileName;
                }
                     Console.WriteLine("ClassId: " + student.ClassId);

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
                if (ModelState.IsValid)
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

