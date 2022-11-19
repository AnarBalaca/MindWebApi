using Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mind.Business.Services;
using Mind.Entity.Dto.Psychologist;
using Mind.WebApi.Common;

namespace Mind.WebApi.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]
public class PsychologistController : ControllerBase
{
    private readonly IPsychologistService _psychologistService;
    public PsychologistController(IPsychologistService psychologistService)
    {
        _psychologistService = psychologistService;
    }

    [HttpGet("PsychologistGetAll")]
    public async Task<ActionResult> PsychologistGetAllAsync()
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
    public async Task<IActionResult> Get(string id)
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



    [HttpGet("GetPsychologist/{id}")]
    public async Task<IActionResult> GetPsychologist(int id)
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


    [HttpPost("Update/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PsychologistUpdateDto entity)
    {
        try
        {
            await _psychologistService.Update(id , entity);
            return Ok();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _psychologistService.Delete(id);
        return NoContent();
    }

}
