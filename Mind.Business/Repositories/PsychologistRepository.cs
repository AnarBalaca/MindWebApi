using AutoMapper;
using Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Mind.Business.Helpers;
using Mind.Business.Services;
using Mind.Data.Abstracts;
using Mind.Entity.Dto.Psychologist;
using Mind.Entity.Entities;
using Mind.Entity.Identity;
using System.Security.Claims;

namespace Mind.Business.Repositories
{
    public class PsychologistRepository : IPsychologistService
    {
        private readonly IPsychologistDal _psychologistDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserDal _userDal;
        private readonly IImageDal _imageDal;
        public PsychologistRepository(
            UserManager<AppUser> userManager,
            IPsychologistDal psychologistDal,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserDal userDal,
        IHostEnvironment hostEnvironment, IImageDal imageDal)

        {
            _psychologistDal = psychologistDal;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userDal = userDal;
            _imageDal = imageDal;
        }

        public async Task<PsychologistGetDto> Get(string id)
        {

            var data = await _psychologistDal.GetAsync(n => n.UserId == id && !n.IsDeleted, includes: "Image");
            if (data is null)
            {
                throw new EntityCouldNotFoundException();
            }
            List<string?> imageUrls = new();
            imageUrls.AddRange(data.Image!.Where(n => n.PsychologistId == data.Id).ToList().Select(psycoImage => psycoImage!.Name));
            var psychologistGetDto = _mapper.Map<PsychologistGetDto>(data);
            psychologistGetDto.ImageName = imageUrls;
            return psychologistGetDto;
        }


        public async Task<PsychologistGetDto> Get(int id)
        {
          
            var data = await _psychologistDal.GetAsync(n => n.Id == id && !n.IsDeleted, includes: "Image");
            AppUser user = await _userDal.GetAsync(n=>n.Id == data.UserId);
            if (data is null)
            {
                throw new EntityCouldNotFoundException();
            }
            List<string?> imageUrls = new();
            imageUrls.AddRange(data.Image!.Where(n => n.PsychologistId == data.Id).ToList().Select(psycoImage => psycoImage!.Name));
            var psychologistGetDto = _mapper.Map<PsychologistGetDto>(data);
            psychologistGetDto.ImageName = imageUrls;
            psychologistGetDto.UserName = user.UserName;
            psychologistGetDto.Firstname = user.Firstname;
            psychologistGetDto.Lastname = user.Lastname;
            psychologistGetDto.Email = user.Email;



            return psychologistGetDto;
        }

        public async Task<List<PsychologistGetDto>> GetAll()
        {

            var datas = await _psychologistDal.GetAllAsync(n => !n.IsDeleted, includes: "Image");
            if (datas is null)
            {
                throw new EntityCouldNotFoundException();
            }
            List<PsychologistGetDto> psychologistGetDtos = new();

            foreach (var data in datas)
            {
                List<string?> imageUrls = new();
                imageUrls.AddRange(data.Image!.Where(n => n.PsychologistId == data.Id).ToList().Select(psycoImage => psycoImage.Name));
                AppUser user = await _userDal.GetAsync(n => n.Id == data.UserId);
                PsychologistGetDto psychologistGetDto = new()
                {
                    Id = data.Id,
                    TherapyPrice = data.TherapyPrice,
                    ExperienceYear = data.ExperienceYear,
                    ImageName = imageUrls
                };

                psychologistGetDto.UserName = user.UserName;
                psychologistGetDto.Firstname = user.Firstname;
                psychologistGetDto.Lastname = user.Lastname;
                psychologistGetDto.Email = user.Email;


                psychologistGetDtos.Add(psychologistGetDto);

            }
            return psychologistGetDtos;
        }

        public async Task Create(PsychologistCreateDto entity)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
            
            var psychologist = _mapper.Map<Psychologist>(entity);
            psychologist.UserId = userId;
            psychologist.User = appUser;
            psychologist.CreateDate = DateTime.UtcNow.AddHours(4);

            if (entity.ImageFiles != null)
            {
                List<Image> images = new();
                foreach (var imageFile in entity.ImageFiles)
                {
                    Image image = new()
                    {
                        Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Image")
                    };
                    await _imageDal.CreateAsync(image);

                    psychologist.Image = images;
                }
                await _psychologistDal.CreateAsync(psychologist);
            }
        }

        public async Task Update(string id, PsychologistUpdateDto entity)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            AppUser appUser = await _userDal.GetAsync(u => u.Id == id && u.IsPsycho);
            
            Psychologist psychologist = await _psychologistDal.GetAsync(n => n.UserId == id, includes: "Image");
            if (appUser is null) throw new NullReferenceException();
            

           
           
            psychologist.LocalAdress = entity.LocalAdress;
            psychologist.PhoneNumber =  entity.PhoneNumber;
            psychologist.ExperienceYear =  entity.ExperienceYear;
            psychologist.TherapyPrice =  entity.TherapyPrice;
            psychologist.UpdateDate = DateTime.UtcNow.AddHours(4);

            await _psychologistDal.UpdateAsync(psychologist);


        }
        public async Task Delete(string id)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Psychologist psychologist = await _psychologistDal.GetAsync(n => n.UserId == id, includes: "Images");
            AppUser psychoUser = await _userDal.GetAsync(c => c.Id == psychologist.UserId);
            if (psychologist == null) throw new NullReferenceException();
            
            psychologist.IsDeleted = true;
            await _psychologistDal.DeleteAsync(psychologist);
            await _userDal.DeleteAsync(psychoUser);

        }

    }
}
