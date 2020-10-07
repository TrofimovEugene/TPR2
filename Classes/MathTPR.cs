using SciChart.Charting3D.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPR2.Classes
{
	public static class MathTpr
	{
		// ReSharper disable CommentTypo
		/* вычисление диспресии по X */
		public static double DispersionX(List<Point> points, double average)
		{
			var result = points.Sum(point => (point.X - average) * (point.X - average));
			result /= points.Count;
			return result;
		}
		/* вычисление диспресии по Y */
		public static double DispersionY(List<Point> points, double average)
		{
			var result = points.Sum(point => (point.Y - average) * (point.Y - average));
			result /= points.Count;
			return result;
		}

		/* функция Гаусса для нормального распределения */
		public static double func_Gauss(double x, double sigma, double mu)
		{
			var result = 0.0;
			result = (1 / (sigma * 2.50662827)) * Math.Exp(-(((x - mu) * (x - mu)) / (2 * sigma * sigma)));
			return result;
		}
		/* производная фукнции Гаусса для нормального распределения */
		public static double func_der_Gauss(double x, double sigma, double mu)
		{
			var result = 0.0;
			result = ((-1 / (sigma * sigma)) * (x - mu)) * func_Gauss(x, sigma, mu);
			return result;
		}

		/* Фукнкция нахождения порогового значения по условию минимума среднего риска */
		public static double DetermThresholdValuesMinAverageRisk(double c11, double c12, double c21, double c22, double p1, double p2,
			double sigma1, double sigma2, double mu1, double mu2)
		{
			var otnoshC = 0.0;
			var porVal1 = 0.0;
			var porVal2 = 0.0;
			otnoshC = ((c12 - c22) * (p2)) / ((c21 - c11) * (p1));
			var a = sigma1 * sigma1 - sigma2 * sigma2;
			var b = 2 * mu1 * sigma2 * sigma2 - 2 * mu2 * sigma1 * sigma1;
			var c = mu2 * mu2 * sigma1 * sigma1 - mu1 * mu1 * sigma2 * sigma2 - 2 * sigma1 * sigma1
			        * sigma2 * sigma2 * Math.Log(otnoshC * sigma1 / sigma2);
			var discriminant = b * b - 4 * a * c;
			if (discriminant >= 0)
			{
				porVal1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
				porVal2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
			}
			if (mu1 <= mu2)
			{
				return porVal1;
			} else
			{
				return porVal2;
			}
		}
		public static double DetermThresholdValuesMinNumberOfErroneousDecisionsX(List<Point> fS1, List<Point> fS2, double p1, double p2,
			double sigma1, double sigma2, double mu1, double mu2, double eps)
		{
			var otnoshF = 0.0;
			var otnoshC = 0.0;
			var porValue = 0.0;
			otnoshC = p2 / p1;
			var points = new List<Point>();
			points = fS1;
			points.AddRange(fS2);
			for (var i = 0; i < points.Count; i++)
			{
				for (var j = 0; j < points.Count; j++)
				{
					if (points[i].X > points[j].X)
					{
						var swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (var i = 0; i < points.Count; i++)
			{
				otnoshF = Math.Abs(func_der_Gauss(points[i].X, sigma1, mu1) / func_der_Gauss(points[i].X, sigma2, mu2));
				if (Math.Abs(otnoshF - otnoshC) < eps)
				{
					porValue = points[i].X;
					break;
				}
			}
			return porValue;
		}
		/* функция нахождения порогового значения по условию минимального числа ошибочных решений*/
		public static double DetermThresholdValuesMinNumberOfErroneousDecisionsY(List<Point> fS1, List<Point> fS2, double p1, double p2,
			double sigma1, double sigma2, double mu1, double mu2, double eps)
		{
			var otnoshF = 0.0;
			var otnoshC = 0.0;
			var porValue = 0.0;
			otnoshC = p2 / p1;
			var points = new List<Point>();
			points = fS1;
			points.AddRange(fS2);
			for (var i = 0; i < points.Count; i++)
			{
				for (var j = 0; j < points.Count; j++)
				{
					if (points[i].Y > points[j].Y)
					{
						var swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}

			}
			for (var i=0; i<points.Count; i++)
			{
				otnoshF = Math.Abs(func_der_Gauss(points[i].Y, sigma1, mu1) / func_der_Gauss(points[i].Y, sigma2, mu2));
				if (Math.Abs(otnoshF - otnoshC) < eps)
				{
					porValue = points[i].Y;
					break;
				}
			}
			return porValue;
		}
		/*Функция вычисления коэффициента корреляции ро*/
		public static double CalculationOfCorrelationCoefficient(List<Point> setPoints, double muX, double muY, double sigmaX, double sigmaY)
		{
			var result = setPoints.Sum(t => (t.X - muX) * (t.Y - muY));
			result /= (setPoints.Count - 1);
			result /= (sigmaX * sigmaY);
			return result;
		}
		/*Фукнция Гаусса для многомерного нормального распределения*/
		public static double func_Gauss_XY(double x, double y, double sigmaX, double sigmaY, double muX, double muY, double ro)
		{
			var result = 0.0;
			var k = 1 / (2 * Math.PI * sigmaX * sigmaY * Math.Sqrt(1 - ro * ro));
			result = k * Math.Exp(-(1 / (2 * (1 - ro * ro))) * ((((x - muX) * (x - muX)) / (sigmaX * sigmaX)) -
				(ro * 2 * (x - muX) * (y - muY)) / (sigmaX * sigmaY) + (((y - muY) * (y - muY)) / (sigmaY * sigmaY))));
			return result;
		}

		public static string DeterminationOfTheSiegert_KotelnikovX(List<Point> fS1, List<Point> fS2, double x, double p1, double p2,
			double sigma1, double sigma2, double mu1, double mu2, double eps)
		{
			var result = "";
			var otnoshF = 0.0;
			var otnoshC = 0.0;
			var points = new List<Point>();
			points = fS1;
			points.AddRange(fS2);
			for (var i = 0; i < points.Count; i++)
			{
				for (var j = 0; j < points.Count; j++)
				{
					if (points[i].X < points[j].X)
					{
						var swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			foreach (var point in points)
			{
				if (Math.Abs(x - point.X) < eps)
				{
					// f`(x/S1)/f`(x/S2)
					otnoshF = Math.Abs(func_der_Gauss(point.X, sigma1, mu1) / func_der_Gauss(point.X, sigma2, mu2));
					otnoshC = p2 / p1;
					return otnoshF >= otnoshC ? "S1" : "S2";
				}
			}
			return result;
		}

		public static string DeterminationOfTheSiegert_KotelnikovY(List<Point> fS1, List<Point> fS2, double x, double p1, double p2,
			double sigma1, double sigma2, double mu1, double mu2, double eps)
		{
			var result = "";
			var otnoshF = 0.0;
			var otnoshC = 0.0;
			var points = new List<Point>();
			points = fS1;
			points.AddRange(fS2);
			for (var i = 0; i < points.Count; i++)
			{
				for (var j = 0; j < points.Count; j++)
				{
					if (points[i].Y > points[j].Y)
					{
						var swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			foreach (var point in points)
			{
				if (Math.Abs(x - point.Y) < eps)
				{
					// f`(y/S1)/f`(y/S2)
					otnoshF = Math.Abs(func_der_Gauss(point.Y, sigma1, mu1) / func_der_Gauss(point.Y, sigma2, mu2));
					otnoshC = p2 / p1;
					return otnoshF >= otnoshC ? "S1" : "S2";
				}
			}
			return result;
		}
		/* Функция построения нормального распределения на ось X */
		public static XyzDataSeries3D<double> f_x_Si(double mu, double sigma, double n, List<Point> points)
		{
			var xyzDataSeries3D = new XyzDataSeries3D<double>();
			var x1 = mu - sigma * 3;
			for (var i = 0; i < n * 24; i++)
			{
				x1 += sigma / (n * 4);
				var fX = func_Gauss(x1, sigma, mu);
				points.Add(new Point(x1, 0, fX));
				xyzDataSeries3D.Append(x1, fX, 0);
			}
			return xyzDataSeries3D;
		}
		/* Функция построения нормального распределения на ось Y*/
		public static XyzDataSeries3D<double> f_y_Si(double mu, double sigma, double n, List<Point> points)
		{
			var xyzDataSeries3D = new XyzDataSeries3D<double>();
			var y = mu - sigma * 3;
			for (var i = 0; i < n * 24; i++)
			{
				y += sigma / (n * 4);
				var fY = func_Gauss(y, sigma, mu);
				points.Add(new Point(0, y, fY));
				xyzDataSeries3D.Append(0, fY, y);
			}
			return xyzDataSeries3D;
		}
		/* Функция построения поверхности f(x,y/Si) */
		public static UniformGridDataSeries3D<double> ConstructionSurfaceDistribution(double xCen, double yCen, double distance,
						double sigmaX, double sigmaY, double muX, double muY, double ro)
		{
			var uniformGridDataSeries3D = new UniformGridDataSeries3D<double>(100, 100)
			{
				StartX = xCen - distance * 3,
				StepX = (6 * distance) / 100,
				StartZ = yCen - distance * 3,
				StepZ = (6 * distance) / 100,
			};
			for (var x = 0; x < 100; x++)
			{
				for (var z = 0; z < 100; z++)
				{
					var xVal = x / (double)100 * (6 * distance) - Math.Abs(xCen - distance * 3);
					var zVal = z / (double)100 * (6 * distance) - Math.Abs(yCen - distance * 3);
					var y = func_Gauss_XY(xVal, zVal, sigmaX, sigmaY, muX, muY, ro);
					uniformGridDataSeries3D[z, x] = y;
				}
			}
			return uniformGridDataSeries3D;
		}
	}
}
