using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SciChart.Charting3D;
using SciChart.Charting3D.Axis;
using SciChart.Charting3D.Model;
using SciChart.Charting3D.Modifiers;
using TPR2.Classes;
using TPR2.Forms;

namespace TPR2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Point> f_x_S1 = new List<Point>();
		List<Point> f_x_S2 = new List<Point>();
		List<Point> f_y_S1 = new List<Point>();
		List<Point> f_y_S2 = new List<Point>();
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
		Instructions instructions;
		InformationAboutProgram informationAboutProgram;
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
			instructions = new Instructions();
			instructions.Show();
			instructions.Activate();
			informationAboutProgram = new InformationAboutProgram();
			informationAboutProgram.Show();
			informationAboutProgram.Activate();

		}
		private void button_Click(object sender, RoutedEventArgs e)
		{
			XyzDataSeries3D<double> xyzDataSeries3D_1 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_2 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_3 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_4 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_5 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_6 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_7 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_8 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D_9;
			XyzDataSeries3D<double> xyzDataSeries3D_10;
			XyzDataSeries3D<double> xyzDataSeries3D_11;
			XyzDataSeries3D<double> xyzDataSeries3D_12;
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
			double x_cen = 0.0;
			double y_cen = 0.0;
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
				var x = item.x;
				var z = item.y;
				var y = 0;
				xyzDataSeries3D_1.Append(x, y, z);
			}
			foreach (var item in set_p_2)
			{
				var x = item.x;
				var z = item.y;
				var y = 0;
				xyzDataSeries3D_2.Append(x, y, z);
			}
			xyzDataSeries3D_1.SeriesName = "S1";
			xyzDataSeries3D_2.SeriesName = "S2";
			ScatterSeries3D_1.DataSeries = xyzDataSeries3D_1;
			ScatterSeries3D_2.DataSeries = xyzDataSeries3D_2;
			
			double ro_1 = Math_TPR.CalculationOfCorrelationCoefficient(set_p_1, mu_1_x, mu_1_y, sigma_1_x, sigma_1_x);
			for (int i = 0; i < n_1; i++)
			{
				int x = i;
				int z = i;
				double xVal = set_p_1[i].x;
				double zVal = set_p_1[i].y;
				set_p_1[i].z = Math_TPR.func_Gauss_XY(xVal, zVal, sigma_1_x, sigma_1_y, mu_1_x, mu_1_y, ro_1);
				// построение распределения
				xyzDataSeries3D_3.Append(xVal, set_p_1[i].z, zVal);
				// построение проекции на оси x и y
				xyzDataSeries3D_4.Append(xVal, set_p_1[i].z, 0);
				xyzDataSeries3D_5.Append(0, set_p_1[i].z, zVal);
			}
			pointGrid.DataContext = set_p_1;
			double ro_2 = Math_TPR.CalculationOfCorrelationCoefficient(set_p_2, mu_2_x, mu_2_y, sigma_2_x, sigma_2_x);
			for (int i = 0; i < n_2; i++)
			{
				double xVal = set_p_2[i].x;
				double zVal = set_p_2[i].y;
				set_p_2[i].z = Math_TPR.func_Gauss_XY(xVal, zVal, sigma_2_x, sigma_2_y, mu_2_x, mu_2_y, ro_2);
				// построение распределения
				xyzDataSeries3D_6.Append(xVal, set_p_2[i].z, zVal);
				// построение проекции на оси x и y
				xyzDataSeries3D_7.Append(xVal, set_p_2[i].z, 0);
				xyzDataSeries3D_8.Append(0, set_p_2[i].z, zVal);
			}
			pointGrid_Copy.DataContext = set_p_2;
			// построение плотностей проекций множеств S1 и S2
			// данные полученные из практически заданных СВ величин
			average_x1 = set1.calculate_Average_Value_x();
			average_y1 = set1.calculate_Average_Value_y();
			double dispersion_x1;
			double dispersion_y1;
			average_x2 = set2.calculate_Average_Value_x();
			average_y2 = set2.calculate_Average_Value_y();
			double dispersion_x2;
			double dispersion_y2;
			// построение плотности проекции f(x/S1)
			dispersion_x1 = Math_TPR.DispersionX(set_p_1, average_x1);
			sigma_x1 = Math.Sqrt(dispersion_x1);
			xyzDataSeries3D_9 = Math_TPR.f_x_Si(average_x1, sigma_x1, n_1, f_x_S1);
			xyzDataSeries3D_9.SeriesName = "f(x/S1)";
			PointLineSeries3D.DataSeries = xyzDataSeries3D_9;
			// построение плотности проекции f(x/S2)
			dispersion_x2 = Math_TPR.DispersionX(set_p_2, average_x2);
			sigma_x2 = Math.Sqrt(dispersion_x2);
			xyzDataSeries3D_10 = Math_TPR.f_x_Si(average_x2, sigma_x2, n_2, f_x_S2);
			xyzDataSeries3D_10.SeriesName = "f(x/S2)";
			PointLineSeries3D_1.DataSeries = xyzDataSeries3D_10;
			// построение плотности проекции f(y/S1)
			dispersion_y1 = Math_TPR.DispersionY(set_p_1, average_y1);
			sigma_y1 = Math.Sqrt(dispersion_y1);
			xyzDataSeries3D_11 = Math_TPR.f_y_Si(average_y1, sigma_y1, n_1, f_y_S1);
			xyzDataSeries3D_11.SeriesName = "f(y/S1)";
			PointLineSeries3D_2.DataSeries = xyzDataSeries3D_11;
			// построение плотности проекции f(y/S2)
			dispersion_y2 = Math_TPR.DispersionY(set_p_2, average_y2);
			sigma_y2 = Math.Sqrt(dispersion_y2);
			xyzDataSeries3D_12 = Math_TPR.f_y_Si(average_y2, sigma_y2, n_2, f_y_S2);
			xyzDataSeries3D_12.SeriesName = "f(y/S2)";
			PointLineSeries3D_3.DataSeries = xyzDataSeries3D_12;
			
			int xSize = 100;
			int zSize = 100;

			x_cen = Math.Abs(mu_1_x - mu_2_x) / 2;
			y_cen = Math.Abs(mu_1_y - mu_2_y) / 2;

			double distance_1 = Math.Sqrt(Math.Pow(x_cen-mu_1_x, 2) + Math.Pow(y_cen-mu_1_y, 2));
			if (sigma_1_x > distance_1)
			{
				distance_1 = sigma_1_x;
			}
			if (sigma_1_y > distance_1)
			{
				distance_1 = sigma_1_y;
			}
			if (sigma_2_x > distance_1)
			{
				distance_1 = sigma_2_x;
			}
			if (sigma_2_y > distance_1)
			{
				distance_1 = sigma_2_y;
			}

			var meshDataSeries = new UniformGridDataSeries3D<double>(xSize, zSize)
			{
				StartX = x_cen - distance_1*2,
				StepX = (4 * distance_1) / 100,
				StartZ = y_cen - distance_1*2,
				StepZ = (4 * distance_1) / 100,
				SeriesName = "f(x,y/S1)",
			};
			string text = "";
			for (int x = 0; x < xSize; x++)
			{
				for (int z = 0; z < zSize; z++)
				{
					double xVal = x / (double)xSize  * (4 * distance_1) - Math.Abs(x_cen - distance_1 * 2);
					double zVal = z / (double)zSize  * (4 * distance_1) - Math.Abs(y_cen - distance_1 * 2);
					double y = Math_TPR.func_Gauss_XY(xVal, zVal, sigma_1_x, sigma_1_y, mu_1_x, mu_1_y, ro_1);
					meshDataSeries[z, x] = y;
				}
			}
			surfaceMeshRenderableSeries.DataSeries = meshDataSeries;

			var meshDataSeries_2 = new UniformGridDataSeries3D<double>(xSize, zSize)
			{
				StartX = x_cen - distance_1 * 2,
				StepX = (4 * distance_1) / 100,
				StartZ = y_cen - distance_1 * 2,
				StepZ = (4 * distance_1) / 100,
				SeriesName = "f(x,y/S2)",
			};
			for (int x = 0; x < xSize; x++)
			{
				for (int z = 0; z < zSize; z++)
				{
					double xVal = x / (double)xSize * (4 * distance_1) - Math.Abs(x_cen - distance_1 * 2);
					double zVal = z / (double)zSize * (4 * distance_1) - Math.Abs(y_cen - distance_1 * 2);
					double y = Math_TPR.func_Gauss_XY(xVal, zVal, sigma_2_x, sigma_2_y, mu_2_x, mu_2_y, ro_2);
					meshDataSeries_2[z, x] = y;
				}
			}
			surfaceMeshRenderableSeries_2.DataSeries = meshDataSeries_2;
		} 
		/*Определение порогового значения x по условию минимума среднего риска*/
		private void button1_Click(object sender, RoutedEventArgs e)
		{
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
			x_por = Math_TPR.DetermThresholdValuesMinAverageRisk(C11, C12, C21, C22, p1, p2, 
				sigma_x1, sigma_x2, average_x1, average_x2);
			textBox10.Text = x_por.ToString();
		}
		/*Определение порогового значения x по условию минимума числа ошибочных решений*/
		private void button2_Click(object sender, RoutedEventArgs e)
		{
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
			x_por = Math_TPR.DetermThresholdValuesMinNumberOfErroneousDecisions(f_x_S1, f_x_S2, p1, p2, true,
				sigma_x1, sigma_x2, average_x1, average_x2);
			textBox11.Text = x_por.ToString();
		}
		/*Определение порогового значения y по условию минимума среднего риска*/
		private void button3_Click(object sender, RoutedEventArgs e)
		{
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

			y_por = Math_TPR.DetermThresholdValuesMinAverageRisk(C11, C12, C21, C22, p1, p2,  
				sigma_y1, sigma_y2, average_y1, average_y2);
			textBox17.Text = y_por.ToString();		
		}
		/*Определение порогового значения y по условию минимума числа ошибочных решений*/
		private void button4_Click(object sender, RoutedEventArgs e)
		{
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
			y_por = Math_TPR.DetermThresholdValuesMinNumberOfErroneousDecisions(f_y_S1, f_y_S2, p1, p2, false,
				sigma_y1, sigma_y2, average_y1, average_y2);
			textBox21.Text = y_por.ToString();		
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по X*/
		private void button5_Click(object sender, RoutedEventArgs e)
		{
			double x = 0.0;
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
			textBox22.Text = Math_TPR.DeterminationOfTheSiegert_Kotelnikov(f_x_S1, f_x_S2, x, p1, p2, 
				sigma_x1, sigma_x2, average_x1, average_x2, true);
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по Y*/
		private void button6_Click(object sender, RoutedEventArgs e)
		{
			double y = 0.0;
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
			textBox23.Text = Math_TPR.DeterminationOfTheSiegert_Kotelnikov(f_y_S1, f_y_S2, y, p1, p2, 
				sigma_y1, sigma_y2, average_y1, average_y2, false);
		}

		private void button7_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			instructions.Close();
			instructions = new Instructions();
			instructions.Show();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			informationAboutProgram.Close();
			informationAboutProgram = new InformationAboutProgram();
			informationAboutProgram.Show();
		}
	}
}
