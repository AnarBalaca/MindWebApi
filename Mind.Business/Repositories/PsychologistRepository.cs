using AutoMapper;
using Exceptions.EntityExceptions;
using Microsoft.Extensions.Hosting;
using Mind.Business.Helpers;
using Mind.Business.Services;
using Mind.Data.Abstracts;
using Mind.Entity.Dto.Psychologist;
using Mind.Entity.Entities;

namespace Mind.Business.Repositories
{
    public class PsychologistRepository : IPsychologistService
    {
        private readonly IPsychologistDal _psychologistDal;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IImageDal _imageDal;
        public PsychologistRepository(
            IPsychologistDal psychologistDal,
            IMapper mapper,
            IHostEnvironment hostEnvironment, IImageDal imageDal)
        {
            _psychologistDal = psychologistDal;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _imageDal = imageDal;
        }

        public async Task<PsychologistGetDto> Get(int id)
        {
            var data = await _psychologistDal.GetAsync(n => n.Id == id && !n.IsDeleted, includes: "Image");
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
                PsychologistGetDto psychologistGetDto = new()
                {
                    Id = data.Id,
                    TherapyPrice = data.TherapyPrice,
                    ExperienceYear = data.ExperienceYear,
                    ImageName = imageUrls
                };

                psychologistGetDtos.Add(psychologistGetDto);

            }
            return psychologistGetDtos;
        }

        public async Task Create(PsychologistCreateDto entity)
        {
            var data = _mapper.Map<Psychologist>(entity);
            data.CreateDate = DateTime.UtcNow.AddHours(4);
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
                await _psychologistDal.CreateAsync(data);
            }
        }

        public Task Update(int id, PsychologistUpdateDto entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
