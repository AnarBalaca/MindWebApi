using Mind.Core.EFRepository;
using Mind.Data.Abstracts;
using Mind.Data.DAL;
using Mind.Entity.Entities;

namespace Mind.Data.İmplementations;

public class ImageRepositoryDal : EFEntityRepositoryBase<Image, AppDbContext> , IImageDal
{
    public ImageRepositoryDal(AppDbContext context) : base(context) { }
}
