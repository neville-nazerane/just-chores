using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Extensions
{
    public class RelativeSize : IMarkupExtension<Size>
    {

        public float HeightFactor { get; set; } = 1;

        public int HeightOffSet { get; set; }

        public float WidthFactor { get; set; } = 1;

        public int WidthOffSet { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public Size ProvideValue(IServiceProvider serviceProvider)
        {
            var info = DeviceDisplay.MainDisplayInfo;
            var density = info.Density;
            var width = info.Width / density;
            var height = info.Height / density;

            return new()
            {
                Height = Height ?? height * HeightFactor - HeightOffSet,
                Width = Width ?? width * WidthFactor - WidthOffSet,
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) 
            => (this as IMarkupExtension<Size>).ProvideValue(serviceProvider);
    }
}
