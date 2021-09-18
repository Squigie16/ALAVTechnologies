using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public interface IImageModelRepository
    {
        IQueryable<ImageModel> Images { get; }
        Task<int> AddImageAsync(ImageModel image);
        void DeleteImage(int id);
    }
}
