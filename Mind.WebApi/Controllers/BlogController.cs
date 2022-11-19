using Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mind.Business.Dto.Blog;
using Mind.Business.Services;
using Mind.WebApi.Common;

namespace Mind.WebApi.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]

public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet("blogGet/{id}")]
    public async Task<ActionResult<List<BlogGetDto>>> BlogGetAsync(int id)
    {
        try
        {
            return Ok(await _blogService.Get(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4322, ex.Message));
        }
    }

    [HttpGet("blogGetAll")]
    public async Task<ActionResult<List<BlogGetDto>>> BlogGetAllAsync()
    {
        try
        {
            return Ok(await _blogService.GetAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4101, ex.Message));
        }
    }


    [HttpGet("blogGetAllByPsycholog")]
    public async Task<ActionResult<List<BlogGetDto>>> BlogGetAllByPsychologAsync()
    {
        try
        {
            return Ok(await _blogService.GetAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4101, ex.Message));
        }
    }




    [HttpPost("blogCreate")]
    public async Task<IActionResult> Create([FromForm] BlogCreateDto entity)
    {
        await _blogService.Create(entity);
        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id ,[FromForm] BlogUpdateDto entity)
    {
        try
        {
            await _blogService.Update(id, entity);
            return NoContent();
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
    public async Task<IActionResult> Delete(int id)
    {
        await _blogService.Delete(id);
        return NoContent();
    }
}
