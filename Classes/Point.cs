using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2
{
    // класс, описывающий случаную точку
    public class Point
    {
        public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }
		public Point(double mX, double sX, double mY, double sY, NormalRandom normRand)
        {
            Set_Value(sX, mX, sY, mY, normRand);
        }
		public Point(double x, double y, double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
        // инициализация значений
        void Set_Value(double sigmaX, double muX, double sigmaY, double muY, NormalRandom nr)
        {
            X = nr.NextDouble() * sigmaX + muX;
            Y = nr.NextDouble() * sigmaY + muY;
        }
    }
}
