using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR2.Classes
{
	public static class Math_TPR
	{
		/* вычисление диспресии по X */
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
		/* вычисление диспресии по Y */
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

		/* функция Гаусса для нормального распределения */
		public static double func_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = (1 / (sigma * 2.50662827)) * Math.Exp(-(((x - mu) * (x - mu)) / (2 * sigma * sigma)));
			return result;
		}
		/* производная фукнции Гаусса для нормального распределения */
		public static double func_der_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = ((-1 / (sigma * sigma)) * (x - mu)) * func_Gauss(x, sigma, mu);
			return result;
		}

		/* Фукнкция нахождения порогового значения по условию минимума среднего риска */
		public static double DetermThresholdValuesMinAverageRisk(List<Point> f_s1, List<Point> f_s2,
			double c11, double c12, double c21, double c22, double p_1, double p_2, bool x_or_y,
			double sigma_1, double sigma_2, double mu_1, double mu_2)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double por_val = 0.0;
			List<Point> points;
			points = f_s1;
			points.AddRange(f_s2);
			if (x_or_y == true)
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].Return_Value_x() > points[j].Return_Value_x())
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math_TPR.func_Gauss(points[i].Return_Value_x(), sigma_1, mu_1) / Math_TPR.func_Gauss(points[i].Return_Value_x(), sigma_2, mu_2);
					otnosh_C = ((c12 - c22) * (p_2)) / ((c21 - c11) * (p_1));
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_val = points[i].Return_Value_x();
						break;
					}
				}
			}
			else
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].Return_Value_y() < points[j].Return_Value_y())
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math_TPR.func_Gauss(points[i].Return_Value_y(), sigma_1, mu_1) / Math_TPR.func_Gauss(points[i].Return_Value_y(), sigma_2, mu_2);
					otnosh_C = ((c12 - c22) * (p_2)) / ((c21 - c11) * (p_1));
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_val = points[i].Return_Value_y();
						break;
					}
				}
			}
			return por_val;
		}
		public static double DetermThresholdValuesMinNumberOfErroneousDecisions(List<Point> f_s1, List<Point> f_s2, double p_1, double p_2, bool x_or_y,
			double sigma_1, double sigma_2, double mu_1, double mu_2)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double por_value = 0.0;
			List<Point> points = new List<Point>();
			points = f_s1;
			points.AddRange(f_s2);
			if (x_or_y == true)
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].Return_Value_x() > points[j].Return_Value_x())
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].Return_Value_x(), sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].Return_Value_x(), sigma_2, mu_2));
					otnosh_C = p_2 / p_1;
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_value = points[i].Return_Value_x();
						break;
					}
				}
			}
			else
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].Return_Value_y() > points[j].Return_Value_y())
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].Return_Value_y(), sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].Return_Value_y(), sigma_2, mu_2));
					otnosh_C = p_2 / p_1;
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_value = points[i].Return_Value_y();
						break;
					}
				}
			}
			return por_value;
		}
	}
}
