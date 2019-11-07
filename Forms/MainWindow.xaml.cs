using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using SciChart.Charting3D;
using SciChart.Charting3D.Axis;
using SciChart.Charting3D.Model;
using SciChart.Charting3D.Modifiers;

namespace TPR2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Point> f_x_S1;
		List<Point> f_x_S2;
		List<Point> f_y_S1;
		List<Point> f_y_S2;
		double sigma_x1 = 0.0;
		double sigma_x2 = 0.0;
		double sigma_y1 = 0.0;
		double sigma_y2 = 0.0;
		double average_x1 = 0.0;
		double average_x2 = 0.0;
		double average_y1 = 0.0;
		double average_y2 = 0.0;
		int n_1 = 0;
		int n_2 = 0;
		public MainWindow()
		{
			var sciChart3DSurface = new SciChart3DSurface()
			{
				IsAxisCubeVisible = true,
				IsFpsCounterVisible = true,
				IsXyzGizmoVisible = true
			};
			// Create the X,Y,Z Axis
			sciChart3DSurface.XAxis = new NumericAxis3D();
			sciChart3DSurface.YAxis = new NumericAxis3D();
			sciChart3DSurface.ZAxis = new NumericAxis3D();
			// Specify Interactivity Modifiers
			sciChart3DSurface.ChartModifier = new ModifierGroup3D(
					 new OrbitModifier3D(),
					 new ZoomExtentsModifier3D());
			InitializeComponent();
		}
		// вычисление диспресии по X
		private double DispersionX(List<Point> points, double average)
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
		private double DispersionY(List<Point> points, double average)
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
		private double func_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = (1 / (sigma * 2.50662827)) * Math.Exp(-(((x - mu) * (x - mu)) / (2 * sigma * sigma)));
			return result;
		}
		// производная фукнции Гаусса для нормального распределения
		private double func_der_Gauss(double x, double sigma, double mu)
		{
			double result;
			result = ((-1/(sigma*sigma))*(x-mu)) * func_Gauss(x, sigma, mu);
			return result;
		}
		private void button_Click(object sender, RoutedEventArgs e)
		{
			f_x_S1 = new List<Point>();
			f_x_S2 = new List<Point>();
			f_y_S1 = new List<Point>();
			f_y_S2 = new List<Point>();
			XyzDataSeries3D<double> xyzDataSeries3D_1 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_2 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_3 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_4 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_5 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_6 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_7 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_8 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_9 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_10 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_11 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_12 = new XyzDataSeries3D<double>();
			n_1 = 0;
			n_2 = 0;
			double mu_1_x = 0.0;
			double mu_1_y = 0.0;
			double mu_2_x = 0.0;
			double mu_2_y = 0.0;
			double sigma_1_x = 0.0;
			double sigma_1_y = 0.0;
			double sigma_2_x = 0.0;
			double sigma_2_y = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox.Text;
				n_1 = Convert.ToInt32(input_buffer);
				input_buffer = textBox1.Text;
				n_2 = Convert.ToInt32(input_buffer);
				input_buffer = textBox2.Text;
				mu_1_x = Convert.ToDouble(input_buffer);
				input_buffer = textBox3.Text;
				mu_1_y = Convert.ToDouble(input_buffer);
				input_buffer = textBox4.Text;
				mu_2_x = Convert.ToDouble(input_buffer);
				input_buffer = textBox5.Text;
				mu_2_y = Convert.ToDouble(input_buffer);
				input_buffer = textBox6.Text;
				sigma_1_x = Convert.ToDouble(input_buffer);
				input_buffer = textBox7.Text;
				sigma_1_y = Convert.ToDouble(input_buffer);
				input_buffer = textBox8.Text;
				sigma_2_x = Convert.ToDouble(input_buffer);
				input_buffer = textBox9.Text;
				sigma_2_y = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			List<Point> set_p_1;
			List<Point> set_p_2;
			NormalRandom norm_rand = new NormalRandom();
			Set_Point set1 = new Set_Point(mu_1_x, sigma_1_x, mu_1_y, sigma_1_y, n_1, norm_rand);
			Set_Point set2 = new Set_Point(mu_2_x, sigma_2_x, mu_2_y, sigma_2_y, n_2, norm_rand);
			set_p_1 = set1.get_list_point();
			set_p_2 = set2.get_list_point();
			// построение точек на плоскости XY
			foreach (var item in set_p_1)
			{
				var x = item.Return_Value_x();
				var z = item.Return_Value_y();
				var y = 0;
				xyzDataSeries3D_1.Append(x, y, z);
			}
			foreach (var item in set_p_2)
			{
				var x = item.Return_Value_x();
				var z = item.Return_Value_y();
				var y = 0;
				xyzDataSeries3D_2.Append(x, y, z);
			}
			xyzDataSeries3D_1.SeriesName = "S1";
			xyzDataSeries3D_2.SeriesName = "S2";
			ScatterSeries3D_1.DataSeries = xyzDataSeries3D_1;
			ScatterSeries3D_2.DataSeries = xyzDataSeries3D_2;

			double cov_1 = 0.0;
			for (int j = 0; j < n_1; j++)
			{
				cov_1 += (set_p_1[j].Return_Value_x() - mu_1_x) * (set_p_1[j].Return_Value_y() - mu_1_y);
			}
			cov_1 = cov_1 / (n_1-1);
			double r = cov_1 / (sigma_1_x * sigma_1_y);
			double k = 1 / (2 * Math.PI * sigma_1_x * sigma_1_y * Math.Sqrt(1 - r * r));
			for (int i = 0; i < n_1; i++)
			{
				int x = i;
				int z = i;
				double xVal = set_p_1[i].Return_Value_x();
				double zVal = set_p_1[i].Return_Value_y();
				set_p_1[i].Set_Value_z(k * Math.Exp(-(1 / (2 * (1 - r * r))) * ((((xVal - mu_1_x) * (xVal - mu_1_x)) / (sigma_1_x * sigma_1_x)) - (r * 2 * (xVal - mu_1_x) * (zVal - mu_1_y)) / (sigma_1_x * sigma_1_y) + (((zVal - mu_1_y) * (zVal - mu_1_y)) / (sigma_1_y * sigma_1_y)))));
				// построение распределения
				xyzDataSeries3D_3.Append(xVal, set_p_1[i].Return_Value_z(), zVal);
				// построение проекции на оси x и y
				xyzDataSeries3D_5.Append(xVal, set_p_1[i].Return_Value_z(), 0);
				xyzDataSeries3D_6.Append(0, set_p_1[i].Return_Value_z(), zVal);
			}
			xyzDataSeries3D_3.SeriesName = "f(x,y/S1)";
			Column1.DataSeries = xyzDataSeries3D_3;
			xyzDataSeries3D_5.SeriesName = "f(x,0/S1)";
			Column3.DataSeries = xyzDataSeries3D_5;
			xyzDataSeries3D_6.SeriesName = "f(0,y/S1)";
			Column4.DataSeries = xyzDataSeries3D_6;
			
			double cov_2 = 0.0;
			for (int j = 0; j < n_2; j++)
			{
				cov_2 += (set_p_2[j].Return_Value_x() - mu_2_x) * (set_p_2[j].Return_Value_y() - mu_2_y);
			}
			cov_2 = cov_2 / (n_2-1);
			double r_2 = cov_2 / (sigma_2_x * sigma_2_y);
			double k_2 = 1 / (2 * Math.PI * sigma_2_x * sigma_2_y * Math.Sqrt(1 - r * r));
			for (int i = 0; i < n_2; i++)
			{
				double xVal = set_p_2[i].Return_Value_x();
				double zVal = set_p_2[i].Return_Value_y();
				set_p_2[i].Set_Value_z(k_2 * Math.Exp(-(1 / (2 * (1 - r_2 * r_2))) * ((((xVal - mu_2_x) * (xVal - mu_2_x)) / (sigma_2_x * sigma_2_x)) - (r_2 * 2 * (xVal - mu_2_x) * (zVal - mu_2_y)) / (sigma_2_x * sigma_2_y) + (((zVal - mu_2_y) * (zVal - mu_2_y)) / (sigma_2_y * sigma_2_y)))));
				// построение распределения
				xyzDataSeries3D_4.Append(xVal, set_p_2[i].Return_Value_z(), zVal);
				// построение проекции на оси x и y
				xyzDataSeries3D_7.Append(xVal, set_p_2[i].Return_Value_z(), 0);
				xyzDataSeries3D_8.Append(0, set_p_2[i].Return_Value_z(), zVal);
			}
			xyzDataSeries3D_4.SeriesName = "f(x,y/S2)";
			Column2.DataSeries = xyzDataSeries3D_4;
			xyzDataSeries3D_7.SeriesName = "f(x,0/S2)";
			Column5.DataSeries = xyzDataSeries3D_7;
			xyzDataSeries3D_8.SeriesName = "f(0,y/S2)";
			Column6.DataSeries = xyzDataSeries3D_8;

			// построение плотностей проекций множеств S1 и S2
			average_x1 = set1.calculate_Average_Value_x();
			average_y1 = set1.calculate_Average_Value_y();
			double dispersion_x1 = 0.0;
			double dispersion_y1 = 0.0;
			average_x2 = set2.calculate_Average_Value_x();
			average_y2 = set2.calculate_Average_Value_y();
			double dispersion_x2 = 0.0;
			double dispersion_y2 = 0.0;
			// построение плотности проекции f(x/S1)
			dispersion_x1 = DispersionX(set_p_1, average_x1);
			sigma_x1 = Math.Sqrt(dispersion_x1);
			double x1 = average_x1 - sigma_x1*3;
			for (int i = 0; i < n_1*18; i++)
			{
				x1 += sigma_x1 / (n_1*3);
				double f_x = func_Gauss(x1, sigma_1_x, average_x1);
				f_x_S1.Add(new Point(x1, 0, f_x));
				xyzDataSeries3D_9.Append(x1, f_x, 0);
			}
			xyzDataSeries3D_9.SeriesName = "f(x/S1)";
			PointLineSeries3D.DataSeries = xyzDataSeries3D_9;
			// построение плотности проекции f(x/S2)
			dispersion_x2 = DispersionX(set_p_2, average_x2);
			sigma_x2 = Math.Sqrt(dispersion_x2);
			double x2 = average_x2 - sigma_x2 * 3;
			for (int i = 0; i < n_2 * 18; i++)
			{
				x2 += sigma_x2 / (n_2 * 3);
				double f_x = func_Gauss(x2, sigma_x2, average_x2);
				f_x_S2.Add(new Point(x2, 0, f_x));
				xyzDataSeries3D_10.Append(x2, f_x, 0);
			}
			xyzDataSeries3D_10.SeriesName = "f(x/S2)";
			PointLineSeries3D_1.DataSeries = xyzDataSeries3D_10;
			// построение плотности проекции f(y/S1)
			dispersion_y1 = DispersionY(set_p_1, average_y1);
			sigma_y1 = Math.Sqrt(dispersion_y1);
			double y1 = average_y1 - sigma_y1 * 3;
			for (int i = 0; i < n_1*18; i++)
			{
				y1 += sigma_y1 / (n_1 * 3);
				double f_x = func_Gauss(y1, sigma_y1, average_y1);
				f_y_S1.Add(new Point(0, y1, f_x));
				xyzDataSeries3D_11.Append(0, f_x, y1);
			}
			xyzDataSeries3D_11.SeriesName = "f(y/S1)";
			PointLineSeries3D_2.DataSeries = xyzDataSeries3D_11;
			// построение плотности проекции f(y/S2)
			dispersion_y2 = DispersionY(set_p_2, average_y2);
			sigma_y2 = Math.Sqrt(dispersion_y2);
			double y2 = average_y2 - sigma_y2 * 3;
			for (int i = 0; i < n_2 * 18; i++)
			{
				y2 += sigma_y2 / (n_2 * 3);
				double f_x = func_Gauss(y2, sigma_y2, average_y2);
				f_y_S2.Add(new Point(0, y2, f_x));
				xyzDataSeries3D_12.Append(0, f_x, y2);
			}
			xyzDataSeries3D_12.SeriesName = "f(y/S2)";
			PointLineSeries3D_3.DataSeries = xyzDataSeries3D_12;

			// вывод значений точек образов S1 и S2 на экран
			textBox17.Text += "Образ S1\n";
			foreach (var point in set_p_1)
			{
				textBox17.Text += "x= " + point.Return_Value_x().ToString() + "\n"
								+ "y= " + point.Return_Value_y().ToString() + "\n"
								+ "f(x,y)= " + point.Return_Value_z().ToString() + "\n\n";
			}
			textBox17.Text += "Образ S2\n";
			foreach (var point in set_p_2)
			{
				textBox17.Text += "x= " + point.Return_Value_x().ToString() + "\n"
								+ "y= " + point.Return_Value_y().ToString() + "\n"
								+ "f(x,y)= " + point.Return_Value_z().ToString() + "\n\n";
			}
		} 
		/*Определение порогового значения x по условию минимума среднего риска*/
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double x_por = 0.0;
			double C11 = -1.0;
			double C22 = -1.0;
			double C12 = 0.0;
			double C21 = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				// ввод стоимости ошибок
				input_buffer = textBox14.Text;
				C12 = Convert.ToDouble(input_buffer);
				input_buffer = textBox15.Text;
				C21 = Convert.ToDouble(input_buffer);
				input_buffer = textBox16.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19.Text;
				p2 = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_x_S1;
			points.AddRange(f_x_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_x()>points[i].Return_Value_x())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				otnosh_f = func_Gauss(points[i].Return_Value_x(), sigma_x1, average_x1) / func_Gauss(points[i].Return_Value_x(), sigma_x2, average_x2);
				otnosh_C = ((C12 - C22) * (p2)) / ((C21 - C11) * (p1));
				if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
				{
					x_por = points[i].Return_Value_x();
					label24.Content = "X(пор.)= " + x_por.ToString();
					break;
				
				}
				
			}
		}
		/*Определение порогового значения x по условию минимума числа ошибочных решений*/
		private void button2_Click(object sender, RoutedEventArgs e)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double x_por = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19.Text;
				p2 = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_x_S1;
			points.AddRange(f_x_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_x() > points[i].Return_Value_x())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				otnosh_f = Math.Abs(func_der_Gauss(points[i].Return_Value_x(), sigma_x1, average_x1) / func_der_Gauss(points[i].Return_Value_x(), sigma_x2, average_x2) );
				otnosh_C = p2 / p1;
				if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
				{
					x_por = points[i].Return_Value_x();
					label11.Content = "X(пор.)= " + x_por.ToString();
					break;
				}				
			}			
		}
		/*Определение порогового значения y по условию минимума среднего риска*/
		private void button3_Click(object sender, RoutedEventArgs e)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double y_por = 0.0;
			double C11 = -1.0;
			double C22 = -1.0;
			double C12 = 0.0;
			double C21 = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				// ввод стоимости ошибок
				input_buffer = textBox14_Copy.Text;
				C12 = Convert.ToDouble(input_buffer);
				input_buffer = textBox15_Copy.Text;
				C21 = Convert.ToDouble(input_buffer);
				input_buffer = textBox16_Copy.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19_Copy.Text;
				p2 = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_y_S1;
			points.AddRange(f_y_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_y() < points[i].Return_Value_y())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				otnosh_f = func_Gauss(points[i].Return_Value_y(), sigma_y1, average_y1) / func_Gauss(points[i].Return_Value_y(), sigma_y2, average_y2);
				otnosh_C = ((C12 - C22) * (p2)) / ((C21 - C11) * (p1));
				if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
				{
					y_por = points[i].Return_Value_y();
					label24_Copy.Content = "Y(пор.)= " + y_por.ToString();
					break;

				}
			}
		}
		/*Определение порогового значения y по условию минимума числа ошибочных решений*/
		private void button4_Click(object sender, RoutedEventArgs e)
		{
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double y_por = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16_Copy.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19_Copy.Text;
				p2 = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_y_S1;
			points.AddRange(f_y_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_y() < points[i].Return_Value_y())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				otnosh_f = Math.Abs(func_der_Gauss(points[i].Return_Value_y(), sigma_y1, average_y1) / func_der_Gauss(points[i].Return_Value_y(), sigma_y2, average_y2));
				otnosh_C = p2 / p1;
				if (Math.Abs(otnosh_f - otnosh_C) < 0.001)
				{
					y_por = points[i].Return_Value_y();
					label11_Copy.Content = "Y(пор.)= " + y_por.ToString();
					break;
				}
			}
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по X*/
		private void button5_Click(object sender, RoutedEventArgs e)
		{
			double x = 0.0;
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox18.Text;
				x = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_x_S1;
			points.AddRange(f_x_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_x() > points[i].Return_Value_x())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				if (Math.Abs(x - points[i].Return_Value_x()) < 0.001)
				{
					// f`(x/S1)/f`(x/S2)
					otnosh_f = Math.Abs(func_der_Gauss(points[i].Return_Value_x(), sigma_x1, average_x1) / func_der_Gauss(points[i].Return_Value_x(), sigma_x2, average_x2));
					otnosh_C = p2 / p1;
					if (otnosh_f >= otnosh_C)
					{
						label28.Content = "принадлежит S1";
						break;
					}
					else
					{
						label28.Content = "принадлежит S2";
						break;
					}
				}
			}
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по Y*/
		private void button6_Click(object sender, RoutedEventArgs e)
		{
			double y = 0.0;
			double otnosh_f = 0.0;
			double otnosh_C = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16_Copy.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19_Copy.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox20.Text;
				y = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			List<Point> points = new List<Point>();
			points = f_y_S1;
			points.AddRange(f_y_S2);
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = 0; j < points.Count; j++)
				{
					if (points[i].Return_Value_y() > points[i].Return_Value_y())
					{
						Point swap = points[i];
						points[i] = points[j];
						points[j] = swap;
					}
				}
			}
			for (int i = 0; i < points.Count; i++)
			{
				if (Math.Abs(y - points[i].Return_Value_y()) < 0.001)
				{
					// f`(y/S1)/f`(y/S2)
					otnosh_f = Math.Abs(func_der_Gauss(points[i].Return_Value_y(), sigma_y1, average_y1) / func_der_Gauss(points[i].Return_Value_y(), sigma_y2, average_y2));
					otnosh_C = p2 / p1;
					if (otnosh_f >= otnosh_C)
					{
						label29.Content = "принадлежит S1";
						break;
					}
					else
					{
						label29.Content = "принадлежит S2";
						break;
					}
				}
			}
		}

		private void button7_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
	}
}
