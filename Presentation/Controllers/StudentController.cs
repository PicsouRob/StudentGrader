using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using Domain.Entities;

namespace Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetStudentsAsync();

            return View(students);
        }

        public async Task<IActionResult> Detail(string studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);

            return View(student);
        }

        public IActionResult Create()
        {
            var model = new StudentDto() { EnrollmentDate = DateTime.Now };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(studentDto);
            }

            try
            {
                var studentId = await _studentService.GenerateUniqueStudentIdAsync();

                var student = new Student
                {
                    Name = studentDto.Name,
                    Email = studentDto.Email,
                    StudentId = studentId,
                    Status = studentDto.Status,
                    EnrollmentDate = studentDto.EnrollmentDate,
                    PhoneNumber = studentDto.PhoneNumber
                };

                await _studentService.AddStudentAsync(student);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(studentDto);
            }
        }

        public async Task<IActionResult> Update(string studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);
            Console.WriteLine($"Student: {student.Id}");

            if (student == null)
            {
                return View(new StudentDto());
            }

            var convertedToStudentDto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                StudentId = student.StudentId,
                Status = student.Status,
                EnrollmentDate = student.EnrollmentDate,
                PhoneNumber = student.PhoneNumber
            };

            return View(convertedToStudentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(String studentId, StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(studentDto);
            }

            try
            {
                var student = new Student
                {
                    Id = studentDto.Id,
                    Name = studentDto.Name,
                    Email = studentDto.Email,
                    StudentId = studentId,
                    Status = studentDto.Status,
                    EnrollmentDate = studentDto.EnrollmentDate,
                    PhoneNumber = studentDto.PhoneNumber
                };

                await _studentService.UpdateStudentAsync(student);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(studentDto);
            }
        }

        public async Task<IActionResult> Delete(string studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string studentId)
        {
            try
            {
                await _studentService.DeleteStudentAsync(studentId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                throw new Exception($"{exception.Message}");
            }
        }
    }
}

