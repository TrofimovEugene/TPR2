using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2
{
    public class SetPoint
    {
        // мат. ожидание по X
        double _muX;
        // мат. ожидание по Y
        double _muY;
        // среднеквадратическое отклонение X
        double _sigmaX;
        // среднеквадратическое отклонение Y
        double _sigmaY;
	// количество точек
        int _countPoint;
        NormalRandom _normalRandom;
        List<Point> _setOfPoint = new List<Point>();
        public SetPoint(double muXC, double sigmaXC, double muYC, double sigmaYC, int n, NormalRandom normal)
        {
            _muX = muXC;
            _sigmaX = sigmaXC;
            _muY = muYC;
            _sigmaY = sigmaYC;
            _countPoint = n;
            _normalRandom = normal;
            Set_Value_Point();
        }
        // инициализация значений
        void Set_Value_Point()
        {
            for (int i = 0; i < _countPoint; i++)
            {
                _setOfPoint.Add(new Point(_muX, _sigmaX, _muY, _sigmaY, _normalRandom));
            }
        }
        // вычисление мат. ожидания X
        public double calculate_Average_Value_x()
        {
            double resultAverageX = 0.0;
            for (int i = 0; i < _countPoint; i++)
            {
                resultAverageX += _setOfPoint[i].X;
            }
            return resultAverageX / _countPoint;
        }
        // вычисление мат. ожидания Y
        public double calculate_Average_Value_y()
        {
            double resultAverageY = 0.0;
            for (int i = 0; i < _countPoint; i++)
            {
                resultAverageY += _setOfPoint[i].Y;
            }
            return resultAverageY / _countPoint;
        }
        // вернуть список значений
        public List<Point> get_list_point()
        {
            return _setOfPoint;
        }
    }
}
