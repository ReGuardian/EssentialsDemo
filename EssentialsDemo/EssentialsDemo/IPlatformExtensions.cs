using System.Drawing;

namespace EssentialsDemo
{
    public interface IPlatformExtensions
    {
        string GetPlatformPoint(Point point);
        string GetPlatformSize(Size size);
        string GetPlatformRect(Rectangle rect);
    }
}