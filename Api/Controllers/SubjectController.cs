using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]s")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    private static Subject DtoToSubject(SubjectDto subjectDto)
    {
        return new Subject
        {
            Id = subjectDto.Id,
            Code = subjectDto.Code,
            Name = subjectDto.Name,
            Description = subjectDto.Description,
            Credits = subjectDto.Credits
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjects()
    {
        try
        {
            var subjects = await _subjectService.GetSubjectsAsync();

            return Ok(subjects);
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
    public async Task<IActionResult> GetSubjectById(int id)
    {
        try
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);

            if (subject == null)
                return NotFound($"No existe asignatura con este id: {id}");

            return Ok(subject);
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

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetSubjectByCode(string code)
    {
        try
        {
            var subject = await _subjectService.GetSubjectByCodeAsync(code);

            if (subject == null)
                return NotFound($"No existe asignatura con este codigo: {code}");

            return Ok(subject);
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

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetSubjectByName(string name)
    {
        try
        {
            var subject = await _subjectService.GetSubjectByNameAsync(name);

            if (subject == null)
                return NotFound($"No existe asignatura con este nombre: {name}");

            return Ok(subject);
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
    public async Task<IActionResult> CreateSubject([FromBody] SubjectDto subjectDto)
    {
        var subject = DtoToSubject(subjectDto);

        var result = await _subjectService.AddSubjectAsync(subject);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDto subjectDto)
    {
        var subject = DtoToSubject(subjectDto);

        var result = await _subjectService.UpdateSubjectAsync(subject);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var result = await _subjectService.DeleteSubjectAsync(id);
        return Ok(result);
    }
}
