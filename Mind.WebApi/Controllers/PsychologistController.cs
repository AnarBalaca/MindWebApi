using Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Mvc;
using Mind.Business.Services;
using Mind.Entity.Dto.Psychologist;
using Mind.WebApi.Common;

namespace Mind.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class PsychologistController : ControllerBase
{
    private readonly IPsychologistService _psychologistService;
    public PsychologistController(IPsychologistService psychologistService)
    {
        _psychologistService = psychologistService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var data = await _psychologistService.GetAll();
            return Ok(data);
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4322, ex.Message));
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status404NotFound, new Response(4101, ex.Message));

        }

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var data = await _psychologistService.Get(id);
            return Ok(data);
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4322, ex.Message));
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status404NotFound, new Response(4101, ex.Message));

        }

    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm]PsychologistCreateDto entity)
    {
        await _psychologistService.Create(entity);
        return NoContent();
    }
}
