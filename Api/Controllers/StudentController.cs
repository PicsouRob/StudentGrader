
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    private static Student DtoToStudent(StudentDto studentDto)
    {
        return new Student
        {
            Id = studentDto.Id,
            StudentId = studentDto.StudentId,
            Name = studentDto.Name,
            Email = studentDto.Email,
            PhoneNumber = studentDto.PhoneNumber,
            EnrollmentDate = studentDto.EnrollmentDate,
            Status = studentDto.Status
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            var students = await _studentService.GetStudentsAsync();
            
            return Ok(students);
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
    public async Task<IActionResult> GetStudentById(string id)
    {
        try
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            
            if (student == null)
                return NotFound($"SNo existe estudiante con esta matricula: {id}");

            return Ok(student);
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
    public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
    {
        var student = DtoToStudent(studentDto);

        var result = await _studentService.AddStudentAsync(student);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
    {
        var student = DtoToStudent(studentDto);

        var result = await _studentService.UpdateStudentAsync(student);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        var result = await _studentService.DeleteStudentAsync(id);
        return Ok(result);
    }
}
