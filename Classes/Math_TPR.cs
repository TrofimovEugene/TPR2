using SciChart.Charting3D.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
				result += (point.x - average) * (point.x - average);
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
				result += (point.y - average) * (point.y - average);
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
		public static double DetermThresholdValuesMinAverageRisk(double c11, double c12, double c21, double c22, double p_1, double p_2,
			double sigma_1, double sigma_2, double mu_1, double mu_2)
		{
			double otnosh_C = 0.0;
			double por_val_1 = 0.0;
			double por_val_2 = 0.0;
			otnosh_C = ((c12 - c22) * (p_2)) / ((c21 - c11) * (p_1));
			double a = sigma_1 * sigma_1 - sigma_2 * sigma_2;
			double b = 2 * mu_1 * sigma_2 * sigma_2 - 2 * mu_2 * sigma_1 * sigma_1;
			double c = mu_2 * mu_2 * sigma_1 * sigma_1 - mu_1 * mu_1 * sigma_2 * sigma_2 - 2 * sigma_1 * sigma_1
				* sigma_2 * sigma_2 * Math.Log(otnosh_C * sigma_1 / sigma_2);
			double discriminant = b * b - 4 * a * c;
			if (discriminant >= 0)
			{
				por_val_1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
				por_val_2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
			}
			if (mu_1 <= mu_2)
			{
				return por_val_1;
			} else
			{
				return por_val_2;
			}
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
						if (points[i].x > points[j].x)
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].x, sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].x, sigma_2, mu_2));
					otnosh_C = p_2 / p_1;
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_value = points[i].x;
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
						if (points[i].y > points[j].y)
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].y, sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].y, sigma_2, mu_2));
					otnosh_C = p_2 / p_1;
					if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
					{
						por_value = points[i].y;
						break;
					}
				}
			}
			return por_value;
		}
		/*Функция вычисления коэффициента корреляции ро*/
		public static double CalculationOfCorrelationCoefficient(List<Point> set_points, double mu_x, double mu_y, double sigma_x, double sigma_y)
		{
			double result = 0.0;
			for (int i = 0; i < set_points.Count; i++)
			{
				result += (set_points[i].x - mu_x) * (set_points[i].y - mu_y);
			}
			result = result / (set_points.Count - 1);
			result = result / (sigma_x * sigma_y);
			return result;
		}
		/*Фукнция Гаусса для многомерного нормального распределения*/
		public static double func_Gauss_XY(double x, double y, double sigma_x, double sigma_y, double mu_x, double mu_y, double ro)
		{
			double result = 0.0;
			double k = 1 / (2 * Math.PI * sigma_x * sigma_y * Math.Sqrt(1 - ro * ro));
			result = k * Math.Exp(-(1 / (2 * (1 - ro * ro))) * ((((x - mu_x) * (x - mu_x)) / (sigma_x * sigma_x)) -
				(ro * 2 * (x - mu_x) * (y - mu_y)) / (sigma_x * sigma_y) + (((y - mu_y) * (y - mu_y)) / (sigma_y * sigma_y))));
			return result;
		}

		public static string DeterminationOfTheSiegert_Kotelnikov(List<Point> f_S1, List<Point> f_S2, double x, double p_1, double p_2,
			double sigma_1, double sigma_2, double mu_1, double mu_2, bool x_or_y)
		{
			string result = "";
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			List<Point> points = new List<Point>();
			points = f_S1;
			points.AddRange(f_S2);
			if (x_or_y == true)
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].x < points[j].x)
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					if (Math.Abs(x - points[i].x) < 0.01)
					{
						// f`(x/S1)/f`(x/S2)
						otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].x, sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].x, sigma_2, mu_2));
						otnosh_C = p_2 / p_1;
						if (otnosh_f >= otnosh_C)
						{
							result = "S1";
							break;
						}
						else
						{
							result = "S2";
							break;
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < points.Count; i++)
				{
					for (int j = 0; j < points.Count; j++)
					{
						if (points[i].y > points[j].y)
						{
							Point swap = points[i];
							points[i] = points[j];
							points[j] = swap;
						}
					}
				}
				for (int i = 0; i < points.Count; i++)
				{
					if (Math.Abs(x - points[i].y) < 0.01)
					{
						// f`(y/S1)/f`(y/S2)
						otnosh_f = Math.Abs(Math_TPR.func_der_Gauss(points[i].y, sigma_1, mu_1) / Math_TPR.func_der_Gauss(points[i].y, sigma_2, mu_2));
						otnosh_C = p_2 / p_1;
						if (otnosh_f >= otnosh_C)
						{
							result = "S1";
							break;
						}
						else
						{
							result = "S2";
							break;
						}
					}
				}
			}
			return result;
		}
		/* Функция построения нормального распределения на ось X */
		public static XyzDataSeries3D<double> f_x_Si(double mu, double sigma, double n, List<Point> points)
		{
			XyzDataSeries3D<double> xyzDataSeries3D = new XyzDataSeries3D<double>();
			double x1 = mu - sigma * 3;
			for (int i = 0; i < n * 18; i++)
			{
				x1 += sigma / (n * 3);
				double f_x = Math_TPR.func_Gauss(x1, sigma, mu);
				points.Add(new Point(x1, 0, f_x));
				xyzDataSeries3D.Append(x1, f_x, 0);
			}
			return xyzDataSeries3D;
		}
		/* Функция построения нормального распределения на ось Y*/
		public static XyzDataSeries3D<double> f_y_Si(double mu, double sigma, double n, List<Point> points)
		{
			XyzDataSeries3D<double> xyzDataSeries3D = new XyzDataSeries3D<double>();
			double y = mu - sigma * 3;
			for (int i = 0; i < n * 18; i++)
			{
				y += sigma / (n * 3);
				double f_x = Math_TPR.func_Gauss(y, sigma, mu);
				points.Add(new Point(0, y, f_x));
				xyzDataSeries3D.Append(0, f_x, y);
			}
			return xyzDataSeries3D;
		}
		/* Функция построения поверхности f(x,y/Si) */
		public static UniformGridDataSeries3D<double> ConstructionSurfaceDistribution(double x_cen, double y_cen, double distance,
						double sigma_x, double sigma_y, double mu_x, double mu_y, double ro)
		{
			var uniformGridDataSeries3D = new UniformGridDataSeries3D<double>(100, 100)
			{
				StartX = x_cen - distance * 2,
				StepX = (4 * distance) / 100,
				StartZ = y_cen - distance * 2,
				StepZ = (4 * distance) / 100,
			};
			for (int x = 0; x < 100; x++)
			{
				for (int z = 0; z < 100; z++)
				{
					double xVal = x / (double)100 * (4 * distance) - Math.Abs(x_cen - distance * 2);
					double zVal = z / (double)100 * (4 * distance) - Math.Abs(y_cen - distance * 2);
					double y = Math_TPR.func_Gauss_XY(xVal, zVal, sigma_x, sigma_y, mu_x, mu_y, ro);
					uniformGridDataSeries3D[z, x] = y;
				}
			}
			return uniformGridDataSeries3D;
		}
	}
}
