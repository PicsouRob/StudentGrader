using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    private static Grade DtoToGrade(GradeDto gradeDto)
    {
        return new Grade
        {
            Id = gradeDto.Id,
            StudentId = gradeDto.StudentId,
            SubjectId = gradeDto.SubjectId,
            Grade1 = gradeDto.Grade1,
            Grade2 = gradeDto.Grade2,
            Grade3 = gradeDto.Grade3,
            Grade4 = gradeDto.Grade4,
            Exam = gradeDto.Exam,
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetGrades()
    {
        try
        {
            var grades = await _gradeService.GetGradesAsync();

            return Ok(grades);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Tiene que estar autenticado para acceder a este recurso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGradeById(int id)
    {
        try
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);

            if (grade == null)
                return NotFound($"No existe nota con este id: {id}");

            return Ok(grade);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Tiene que estar autenticado para acceder a este recurso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor: {ex.Message}");
        }
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetGradeByStudentId(int studentId)
    {
        try
        {
            var grade = await _gradeService.GetGradeByStudentIdAsync(studentId);

            if (grade == null)
                return NotFound($"No existe nota con este id: {studentId}");

            return Ok(grade);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Tiene que estar autenticado para acceder a este recurso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor: {ex.Message}");
        }
    }

    [HttpGet("subject/{subjectId}")]
    public async Task<IActionResult> GetGradeBySubjectId(int subjectId)
    {
        try
        {
            var grade = await _gradeService.GetGradeBySubjectIdAsync(subjectId);

            if (grade == null)
                return NotFound($"No existe nota con este id: {subjectId}");

            return Ok(grade);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Tiene que estar autenticado para acceder a este recurso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateGrade([FromBody] GradeDto gradeDto)
    {
        var grade = DtoToGrade(gradeDto);

        var result = await _gradeService.AddGradeAsync(grade);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDto gradeDto)
    {
        var grade = DtoToGrade(gradeDto);

        var result = await _gradeService.UpdateGradeAsync(grade);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrade(int id)
    {
        var result = await _gradeService.DeleteGradeAsync(id);
        return Ok(result);
    }
}
