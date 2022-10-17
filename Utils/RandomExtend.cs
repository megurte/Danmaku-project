using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Utils
{
    public static class RandomExtend
    {
        public static double NextDouble (this Random @this, double min, double max)
        {
            return @this.NextDouble() * (max - min) + min;
        }

        public static float NextFloat (this Random @this, float min, float max)
        {
            return (float)@this.NextDouble(min, max);
        }
    
        public static Color GetRandomColor(this Random @this)
        {
            List<Color> colors = new List<Color>();
            colors.Add(new Color(0.2202296f,0.8571917f, 0.9528302f, 1f));
            colors.Add(new Color(1f,0.3384168f, 0.3254717f, 1f));
            colors.Add(new Color(0.5603846f,1f, 0.4386792f, 1f));
            colors.Add(new Color(1f,0.9662388f, 0.4392157f, 1f));
            colors.Add(new Color(1f,1f, 1f, 1f));
        
            return colors[@this.Next(0, colors.Count - 1)];
            //return new Color(@this.NextFloat(0, 1f), @this.NextFloat(0, 1f), @this.NextFloat(0, 1f), 1f);
        }
    }
}