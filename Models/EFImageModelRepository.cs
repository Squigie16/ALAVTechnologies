using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class EFImageModelRepository : IImageModelRepository
    {
        private MBS_DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public EFImageModelRepository(MBS_DBContext dBContext, IWebHostEnvironment webHost)
        {
            context = dBContext;
            hostEnvironment = webHost;
        }

        public IQueryable<ImageModel> Images => context.Images;

        public async Task<int> AddImageAsync(ImageModel image)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            image.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(fileStream);
            }

            context.Images.Add(image);
            await context.SaveChangesAsync();

            ImageModel savedImage = context.Images.Find(image);

            return savedImage.ImageId;
        }

        public void DeleteImage(int id)
        {

        }
    }
}
