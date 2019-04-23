using System;
using System.IO;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
