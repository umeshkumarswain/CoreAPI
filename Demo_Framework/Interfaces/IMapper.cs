using System.Collections.Generic;
using FileUploader.Contracts;

namespace FileUploader.Interfaces
{
    public interface IMapper
    {
        List<CarModel> ConvertData(string filePath);
    }
}