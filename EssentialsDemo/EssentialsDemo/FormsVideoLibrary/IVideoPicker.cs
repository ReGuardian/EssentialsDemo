using System;
using System.Threading.Tasks;

/*************************************************
* Code mainly from: 
* https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/VideoPlayerDemos
* last access: 03.06.2019
*************************************************/

namespace FormsVideoLibrary
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
