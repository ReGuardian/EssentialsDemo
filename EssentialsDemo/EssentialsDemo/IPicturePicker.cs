using System;
using System.IO;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}