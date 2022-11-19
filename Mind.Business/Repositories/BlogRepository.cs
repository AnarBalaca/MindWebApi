using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Mind.Business.Dto.Blog;
using Mind.Business.Helpers;
using Mind.Business.Services;
using Mind.Data.Abstracts;
using Mind.Entity.Entities;
using Mind.Entity.Identity;
using System.Security.Claims;

namespace Mind.Business.Repositories;

public class BlogRepository : IBlogService
{
    private readonly IBlogDal _blogDal;
    private readonly IMapper _mapper;
    private readonly IImageDal _imageDal;
    private readonly IUserDal _userDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;

    public BlogRepository(

        IUserDal userDal,
        IBlogDal blogDal,
        IImageDal imageDal,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _imageDal = imageDal;
        _blogDal = blogDal;
        _mapper = mapper;
        _userDal = userDal;
    }

    public async Task<BlogGetDto> Get(int id)
    {
        Blog blog = await _blogDal.GetAsync(n => n.Id == id, "Images");
        BlogGetDto dto = _mapper.Map<BlogGetDto>(blog);
        List<string>? imageUrls = new();
        foreach (var image in blog.Images)
        {
            imageUrls.Add(image.Name);
        }
        dto.ImageName = imageUrls;
        

        return dto;
    }

    public async Task<List<BlogGetDto>> GetAll()
    {
        List<Blog> blogs = await _blogDal.GetAllAsync(n => !n.IsDeleted, 0, int.MaxValue, "Images", "User");
        List<BlogGetDto> dtos = _mapper.Map<List<BlogGetDto>>(blogs);

        for (int i = 0; i < blogs.Count; i++)
        {
            List<string> imageUrls = new();
            foreach (var image in blogs[i].Images)
            {
                imageUrls.Add(image.Name);
            }
            dtos[i].ImageName = imageUrls;
        }

        return dtos;
    }


    public async Task<List<BlogGetDto>> GetAllByPsycholog()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        List<Blog> blogs = await _blogDal.GetAllAsync(n => n.UserId == userId, 0, int.MaxValue, "Images", "User");
        List<BlogGetDto> dtos = _mapper.Map<List<BlogGetDto>>(blogs);

        for (int i = 0; i < blogs.Count; i++)
        {
            List<string> imageUrls = new();
            foreach (var image in blogs[i].Images)
            {
                imageUrls.Add(image.Name);
            }
            dtos[i].ImageName = imageUrls;
        }

        return dtos;
    }





    public async Task Create(BlogCreateDto entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        var blog = _mapper.Map<Blog>(entity);
        blog.UserId = userId;
        blog.User = appUser;
        blog.CreateDate = DateTime.UtcNow.AddHours(4);
        blog.AuthorName = blog.User.Firstname;
         
        if (entity.ImageFiles != null)
        {
            List<Image> images = new();
            foreach (var imageFile in entity.ImageFiles)
            {
                Image image = new()
                {
                    Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
                };
                await _imageDal.CreateAsync(image);
                images.Add(image);
            }
            blog.Images = images;
        }

         await _blogDal.CreateAsync(blog);
    }

    public async Task Update(int id , BlogUpdateDto entity)
    {
        
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId && u.IsPsycho);
        Blog blog = await _blogDal.GetAsync(n => n.UserId == userId, includes: "Images");
        if (appUser is null) throw new NullReferenceException();

        if (entity.ImageFiles != null)
        {
            List<Image> images = new();
            foreach (var imageFile in entity.ImageFiles)
            {
                Image image = new()
                {
                    Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
                };
                await _imageDal.CreateAsync(image);
                images.Add(image);
            }
            blog.Images = images;
        }

        if (entity.Body is not null && entity.Body?.Trim() != "")
        {
            blog.Body = entity.Body?.Trim();
        }


        if (entity.Title is not null && entity.Title?.Trim() != "")
        {
            appUser.Lastname = entity.Title?.Trim();
        }

        if (entity.Title is not null && entity.Title?.Trim() != "")
        {
            appUser.UserName = entity.Title?.Trim();
        }

        blog.AuthorName = blog.User.Firstname;



        blog.UpdateDate = DateTime.UtcNow.AddHours(4);

        await _blogDal.UpdateAsync(blog);
    }
    public async Task Delete(int id)
    {

        Blog blog = await _blogDal.GetAsync(n => n.Id == id, includes: "Images");
        if (blog == null) throw new NullReferenceException();
        List<Image> blogImages = await _imageDal.GetAllAsync(n => n.BlogId == id);
        foreach (Image image in blogImages)
        {
            await _imageDal.DeleteAsync(image);
        }
        await _blogDal.DeleteAsync(blog);
    }

}
