using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Mind.Business.Dto.User;
using Mind.Business.Helpers;
using Mind.Business.Services;
using Mind.Data.Abstracts;
using Mind.Entity.Entities;
using Mind.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Repositories
{

    public class UserRepository : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserDal _userDal;
        private readonly IImageDal _imageDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostEnvironment _hostEnvironment;

        public UserRepository(IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IUserDal userDal,
        UserManager<AppUser> userManager,
        IImageDal imageDal)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _userDal = userDal;
            _userManager = userManager;
            _imageDal = imageDal;
        }


        public async Task<UserGetDto> Get(string id)
        {
           AppUser appUser = await _userDal.GetAsync(u => u.Id == id,"ProfileImage");
            if (appUser is null) throw new NullReferenceException();
            return _mapper.Map<UserGetDto>(appUser);
        }

        public async Task<List<UserGetDto>> GetAll()
        {
            return _mapper.Map<List<UserGetDto>>(await _userDal.GetAllAsync(c => !c.IsPsycho));
        }

        public async Task<List<UserGetDto>> GetAllPsychologists()
        {
            return _mapper.Map<List<UserGetDto>>(await _userDal.GetAllAsync(c => c.IsPsycho));
        }



        public async Task Update(UserUpdateDto entity)
        {
            var mail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            if (mail is null) throw new NullReferenceException();
            

            AppUser user = await _userManager.FindByEmailAsync(mail);
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
            //AppUser user = await _userManager.FindByIdAsync(userId);

            if (appUser is null) throw new NullReferenceException();
            AppUser checkUsername = await _userDal.GetAsync(u => u.UserName == entity.UserName);
            if (checkUsername is not null) throw new ArgumentException();

            if (entity.Firstname is not null && entity.Firstname?.Trim() != "")
            {
                appUser.Firstname = entity.Firstname?.Trim();
            }

            if (entity.Lastname is not null && entity.Lastname?.Trim() != "")
            {
                appUser.Lastname = entity.Lastname?.Trim();
            }

            if (entity.UserName is not null && entity.UserName?.Trim() != "")
            {
                appUser.UserName = entity.UserName?.Trim();
                
            }

            if (entity.Email is not null && entity.Email?.Trim() != "")
            {
                appUser.Email = entity.Email?.Trim();

            }


            appUser.Age = entity.Age;


            if (entity.Firstname is not null && entity.Firstname?.Trim() != "")
            {
                user.Firstname = entity.Firstname?.Trim();
            }

            if (entity.Lastname is not null && entity.Lastname?.Trim() != "")
            {
                user.Lastname = entity.Lastname?.Trim();
            }

            if (entity.UserName is not null && entity.UserName?.Trim() != "")
            {
                user.UserName = entity.UserName?.Trim();

            }

            if (entity.Email is not null && entity.Email?.Trim() != "")
            {
                user.Email = entity.Email?.Trim();

            }


            user.Age = entity.Age;






            await _userManager.UpdateAsync(user);

            await _userDal.UpdateAsync(appUser);
        }


        public async Task ChangeProfilePhotoAsync(ProfileImageDto profileImageDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userDal.GetAsync(u => u.Id == userId, "ProfileImage");
            if (appUser is null) throw new NullReferenceException();
#pragma warning disable CS8604 // Possible null reference argument.
            var image = new Image
            {
                Name = await profileImageDto.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
            };
#pragma warning restore CS8604 // Possible null reference argument.
            await _imageDal.CreateAsync(image);
            appUser.ProfileImage = image;
            appUser.ProfileImageId = image.Id;
            await _userDal.SaveAsync();
        }

    }


}
