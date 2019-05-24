using EssentialsDemo.UWP;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformExtensions))]

namespace EssentialsDemo.UWP
{
    public class PlatformExtensions : IPlatformExtensions
    {
        public string GetPlatformPoint(System.Drawing.Point point)
        {
            // Convert to CoreGraphics.CGPoint, Android.Graphics.Point, and Windows.Foundation.Point
            var platform = point.ToPlatformPoint();
            // Back to System.Drawing.Point
            //var point2 = platform.ToSystemPoint();
            return platform.ToString();
        }

        public string GetPlatformSize(System.Drawing.Size size)
        {
            // Convert to CoreGraphics.CGSize, Android.Util.Size, and Windows.Foundation.Size
            var platform = size.ToPlatformSize();
            // Back to System.Drawing.Size
            //var point2 = platform.ToSystemSize();
            return platform.ToString();
        }

        public string GetPlatformRect(System.Drawing.Rectangle rect)
        {
            // Convert to CoreGraphics.CGRect, Android.Graphics.Rect, and Windows.Foundation.Rect
            var platform = rect.ToPlatformRectangle();
            // Back to System.Drawing.Rectangle
            //var point2 = platform.ToSystemRectangle();
            return platform.ToString();
        }
    }
}