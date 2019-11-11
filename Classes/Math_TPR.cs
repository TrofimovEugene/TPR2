using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2.Classes
{
	public static class Math_TPR
	{
		// вычисление диспресии по X
		public static double DispersionX(List<Point> points, double average)
		{
			double result = 0.0;
			foreach (Point point in points)
			{
				result += (point.Return_Value_x() - average) * (point.Return_Value_x() - average);
			}
			result = result / points.Count;
			return result;
		}
		// вычисление диспресии по Y
		public static double DispersionY(List<Point> points, double average)
		{
			double result = 0.0;
			foreach (Point point in points)
			{
				result += (point.Return_Value_y() - average) * (point.Return_Value_y() - average);
			}
			result = result / points.Count;
			return result;
		}

		// функция Гаусса для нормального распределения
		public static double func_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = (1 / (sigma * 2.50662827)) * Math.Exp(-(((x - mu) * (x - mu)) / (2 * sigma * sigma)));
			return result;
		}
		// производная фукнции Гаусса для нормального распределения
		public static double func_der_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = ((-1 / (sigma * sigma)) * (x - mu)) * func_Gauss(x, sigma, mu);
			return result;
		}
	}
}
