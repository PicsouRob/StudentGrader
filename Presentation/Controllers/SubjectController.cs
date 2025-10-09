
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectService.GetSubjectsAsync();

            return View(subjects);
        }

        public async Task<IActionResult> Detail(int subjectId)
        {
            var student = await _subjectService.GetSubjectByIdAsync(subjectId);

            return View(student);
        }

        public IActionResult Create()
        {
            var model = new SubjectDto();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectDto subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return View(subjectDto);
            }

            try
            {
                var subject = new Subject
                {
                    Name = subjectDto.Name,
                    Code = subjectDto.Code,
                    Description = subjectDto.Description,
                    Credits = subjectDto.Credits
                };

                await _subjectService.AddSubjectAsync(subject);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(subjectDto);
            }
        }
        
        public async Task<IActionResult> Update(int subjectId)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(subjectId);

            if (subject == null)
            {
                return View(new SubjectDto());
            }

            var convertedToSubjectDto = new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Description = subject.Description,
                Credits = subject.Credits
            };

            return View(convertedToSubjectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int subjectId, SubjectDto subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return View(subjectDto);
            }

            try
            {
                var subject = new Subject
                {
                    Id = subjectDto.Id,
                    Name = subjectDto.Name,
                    Code = subjectDto.Code,
                    Description = subjectDto.Description,
                    Credits = subjectDto.Credits
                };

                await _subjectService.UpdateSubjectAsync(subject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ViewData["Error"] = exception.Message;

                return View(subjectDto);
            }
        }

        public async Task<IActionResult> Delete(int subjectId)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(subjectId);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int subjectId)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(subjectId);

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

