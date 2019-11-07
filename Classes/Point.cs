using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2
{
    // класс, описывающий случаную точку
    class Point
    {
        private double x;
        private double y;
		private double z;
        // мат. ожидание по X
        public double mu_x;
        // мат. ожидание по Y
        public double mu_y;
        // среднеквадратическое отклонение X
        public double sigma_x { get; set; }
        // среднеквадратическое отклонение Y
        public double sigma_y { get; set; }
		NormalRandom nr;
        public Point(double m_x, double s_x, double m_y, double s_y, NormalRandom norm_rand)
        {
            mu_x = m_x;
            sigma_x = s_x;
            mu_y = m_y;
            sigma_y = s_y;
            nr = norm_rand;
            Set_Value();
        }
		public Point(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
        // инициализация значений
        void Set_Value()
        {
            x = nr.NextDouble() * sigma_x + mu_x;
            y = nr.NextDouble() * sigma_y + mu_y;
        }
		public void Set_Value_z(double z)
		{
			this.z = z;
		}
        // возврат значения X
        public double Return_Value_x()
        {
            return x;
        }
        // возврат значения Y
        public double Return_Value_y()
        {
            return y;
        }
		public double Return_Value_z()
		{
			return z;
		}
    }
}
