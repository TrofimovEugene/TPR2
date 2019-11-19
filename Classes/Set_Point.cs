using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2
{
    public class Set_Point
    {
        // мат. ожидание по X
        double mu_x;
        // мат. ожидание по Y
        double mu_y;
        // среднеквадратическое отклонение X
        double sigma_x;
        // среднеквадратическое отклонение Y
        double sigma_y;
        int count_point;
        NormalRandom normalRandom;
        List<Point> set_of_Point = new List<Point>();
        public Set_Point(double mu_x_c, double sigma_x_c, double mu_y_c, double sigma_y_c, int n, NormalRandom normal)
        {
            mu_x = mu_x_c;
            sigma_x = sigma_x_c;
            mu_y = mu_y_c;
            sigma_y = sigma_y_c;
            count_point = n;
            normalRandom = normal;
            Set_Value_Point();
        }
        // инициализация значений
        void Set_Value_Point()
        {
            for (int i = 0; i < count_point; i++)
            {
                set_of_Point.Add(new Point(mu_x, sigma_x, mu_y, sigma_y, normalRandom));
            }
        }
        // вычисление мат. ожидания X
        public double calculate_Average_Value_x()
        {
            double result_average_x = 0.0;
            for (int i = 0; i < count_point; i++)
            {
                result_average_x += set_of_Point[i].x;
            }
            return result_average_x / count_point;
        }
        // вычисление мат. ожидания Y
        public double calculate_Average_Value_y()
        {
            double result_average_y = 0.0;
            for (int i = 0; i < count_point; i++)
            {
                result_average_y += set_of_Point[i].y;
            }
            return result_average_y / count_point;
        }
        // вернуть список значений
        public List<Point> get_list_point()
        {
            return set_of_Point;
        }
    }
}
