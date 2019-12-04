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
        public double x { get; set; }
		public double y { get; set; }
		public double z { get; set; }
		public Point(double m_x, double s_x, double m_y, double s_y, NormalRandom norm_rand)
        {
            Set_Value(s_x, m_x, s_y, m_y, norm_rand);
        }
		public Point(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
        // инициализация значений
        void Set_Value(double sigma_x, double mu_x, double sigma_y, double mu_y, NormalRandom nr)
        {
            x = nr.NextDouble() * sigma_x + mu_x;
            y = nr.NextDouble() * sigma_y + mu_y;
        }
    }
}
