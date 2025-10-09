using Application.DTOs;
using Application.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public GradeController(
            IGradeService gradeService,
            IStudentService studentService,
            ISubjectService subjectService
        )
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetGradesAsync();

            return View(grades);
        }

        public async Task<IActionResult> Detail(int gradeId)
        {
            var grade = await _gradeService.GetGradeByIdAsync(gradeId);

            return View(grade);
        }

        private async Task<GradeCreateViewModel> GetViewModelAsync()
        {
            var students = await _studentService.GetStudentsAsync();
            var subjects = await _subjectService.GetSubjectsAsync();

            return new GradeCreateViewModel
            {
                Students = students,
                Subjects = subjects,
                GradeDto = new GradeDto(),
            };
        }

        public async Task<IActionResult> Create()
        {
            var model = await GetViewModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var grade = new Grade
                {
                    StudentId = model.GradeDto!.StudentId,
                    SubjectId = model.GradeDto!.SubjectId,
                    Grade1 = model.GradeDto!.Grade1,
                    Grade2 = model.GradeDto!.Grade2,
                    Grade3 = model.GradeDto!.Grade3,
                    Grade4 = model.GradeDto!.Grade4,
                    Exam = model.GradeDto!.Exam,
                    TotalGrade = 0,
                    Classification = "",
                    State = ""
                };

                await _gradeService.AddGradeAsync(grade);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(model);
            }
        }

        public async Task<IActionResult> Update(int gradeId)
        {
            var grade = await _gradeService.GetGradeByIdAsync(gradeId);
            var model = await GetViewModelAsync();

            if (grade == null)
            {
                return View(model);
            }

            var convertedToGradeDto = new GradeDto
            {
                Id = grade.Id,
                StudentId = grade.StudentId,
                SubjectId = grade.SubjectId,
                Grade1 = grade.Grade1,
                Grade2 = grade.Grade2,
                Grade3 = grade.Grade3,
                Grade4 = grade.Grade4,
                Exam = grade.Exam,
            };

            model.GradeDto = convertedToGradeDto;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int gradeId, GradeDto gradeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(gradeDto);
            }

            try
            {
                var grade = new Grade
                {
                    Id = gradeDto.Id,
                    StudentId = gradeDto.StudentId,
                    SubjectId = gradeDto.SubjectId,
                    Grade1 = gradeDto.Grade1,
                    Grade2 = gradeDto.Grade2,
                    Grade3 = gradeDto.Grade3,
                    Grade4 = gradeDto.Grade4,
                    Exam = gradeDto.Exam,
                    TotalGrade = 0,
                    Classification = "",
                    State = ""
                };

                await _gradeService.UpdateGradeAsync(grade);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(gradeDto);
            }
        }

        public async Task<IActionResult> Delete(int gradeId)
        {
            var grade = await _gradeService.GetGradeByIdAsync(gradeId);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int gradeId)
        {
            try
            {
                await _gradeService.DeleteGradeAsync(gradeId);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                throw new Exception($"{exception.Message}");
            }
        }
    }
}

