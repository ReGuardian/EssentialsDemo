using System;
using System.IO;
using System.Threading.Tasks;

namespace EssentialsDemo
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/photo-picker
    /// Creating the interface, Accessed: 20 May 2019
    /// </summary>
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}