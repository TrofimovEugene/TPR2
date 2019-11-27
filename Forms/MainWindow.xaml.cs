using System;
using System.Collections.Generic;
using System.Windows;
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
		private List<Point> f_x_S1 = new List<Point>();
		private List<Point> f_x_S2 = new List<Point>();
		private List<Point> f_y_S1 = new List<Point>();
		private List<Point> f_y_S2 = new List<Point>();
		private double _sigmaX1 = 0.0;
		private double _sigmaX2 = 0.0;
		private double _sigmaY1 = 0.0;
		private double _sigmaY2 = 0.0;
		private double _averageX1 = 0.0;
		private double _averageX2 = 0.0;
		private double _averageY1 = 0.0;
		private double _averageY2 = 0.0;
		private int _n1 = 0;
		private int _n2 = 0;
		private Instructions _instructions;
		private InformationAboutProgram _informationAboutProgram;
		private UserManual _userManual;
		private _3DGraph _graph;
		public MainWindow()
		{
			InitializeComponent();
		}
		private void button_Click(object sender, RoutedEventArgs e)
		{
			XyzDataSeries3D<double> xyzDataSeries3D1 = new XyzDataSeries3D<double>();
			XyzDataSeries3D<double> xyzDataSeries3D2 = new XyzDataSeries3D<double>();
			_n1 = 0;
			_n2 = 0;
			double mu_1_x = 0.0;
			double mu_1_y = 0.0;
			double mu_2_x = 0.0;
			double mu_2_y = 0.0;
			double sigma_1_x = 0.0;
			double sigma_1_y = 0.0;
			double sigma_2_x = 0.0;
			double sigma_2_y = 0.0;
			double eps = 0.01;
			double x_cen = 0.0;
			double y_cen = 0.0;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox.Text;
				_n1 = Convert.ToInt32(input_buffer);
				input_buffer = textBox1.Text;
				_n2 = Convert.ToInt32(input_buffer);
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
				input_buffer = textBox24.Text;
				eps = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			List<Point> set_p_1;
			List<Point> set_p_2;
			NormalRandom norm_rand = new NormalRandom();
			Set_Point set1 = new Set_Point(mu_1_x, sigma_1_x, mu_1_y, sigma_1_y, _n1, norm_rand);
			Set_Point set2 = new Set_Point(mu_2_x, sigma_2_x, mu_2_y, sigma_2_y, _n2, norm_rand);
			set_p_1 = set1.get_list_point();
			set_p_2 = set2.get_list_point();
			double ro_1 = Math_TPR.CalculationOfCorrelationCoefficient(set_p_1, mu_1_x, mu_1_y, sigma_1_x, sigma_1_x);
			double ro_2 = Math_TPR.CalculationOfCorrelationCoefficient(set_p_2, mu_2_x, mu_2_y, sigma_2_x, sigma_2_x);
			// построение точек на плоскости XY
			foreach (var item in set_p_1)
			{
				var x = item.x;
				var z = item.y;
				item.z = Math_TPR.func_Gauss_XY(item.x, item.y, sigma_1_x, sigma_1_y, mu_1_x, mu_1_y, ro_1);
				var y = 0;
				xyzDataSeries3D1.Append(x, y, z);
			}
			foreach (var item in set_p_2)
			{
				var x = item.x;
				var z = item.y;
				item.z = Math_TPR.func_Gauss_XY(item.x, item.y, sigma_2_x, sigma_2_y, mu_2_x, mu_2_y, ro_2);
				var y = 0;
				xyzDataSeries3D2.Append(x, y, z);
			}
			xyzDataSeries3D1.SeriesName = "S1";
			xyzDataSeries3D2.SeriesName = "S2";
			
			pointGrid.DataContext = set_p_1;
			pointGrid_Copy.DataContext = set_p_2;
			// построение плотностей проекций множеств S1 и S2
			// данные полученные из практически заданных СВ величин
			_averageX1 = set1.calculate_Average_Value_x();
			_averageY1 = set1.calculate_Average_Value_y();
			double dispersion_x1;
			double dispersion_y1;
			_averageX2 = set2.calculate_Average_Value_x();
			_averageY2 = set2.calculate_Average_Value_y();
			double dispersion_x2;
			double dispersion_y2;
			// построение плотности проекции f(x/S1)
			dispersion_x1 = Math_TPR.DispersionX(set_p_1, _averageX1);
			_sigmaX1 = Math.Sqrt(dispersion_x1);
			var xyzDataSeries3D_3 = Math_TPR.f_x_Si(_averageX1, _sigmaX1, _n1, f_x_S1);
			xyzDataSeries3D_3.SeriesName = "f(x/S1)";
			// построение плотности проекции f(x/S2)
			dispersion_x2 = Math_TPR.DispersionX(set_p_2, _averageX2);
			_sigmaX2 = Math.Sqrt(dispersion_x2);
			var xyzDataSeries3D_4 = Math_TPR.f_x_Si(_averageX2, _sigmaX2, _n2, f_x_S2);
			xyzDataSeries3D_4.SeriesName = "f(x/S2)";
			// построение плотности проекции f(y/S1)
			dispersion_y1 = Math_TPR.DispersionY(set_p_1, _averageY1);
			_sigmaY1 = Math.Sqrt(dispersion_y1);
			var xyzDataSeries3D_5 = Math_TPR.f_y_Si(_averageY1, _sigmaY1, _n1, f_y_S1);
			xyzDataSeries3D_5.SeriesName = "f(y/S1)";
			// построение плотности проекции f(y/S2)
			dispersion_y2 = Math_TPR.DispersionY(set_p_2, _averageY2);
			_sigmaY2 = Math.Sqrt(dispersion_y2);
			var xyzDataSeries3D_6 = Math_TPR.f_y_Si(_averageY2, _sigmaY2, _n2, f_y_S2);
			xyzDataSeries3D_6.SeriesName = "f(y/S2)";
			// нахождение центра между образами
			x_cen = Math.Abs(mu_1_x - mu_2_x) / 2;
			y_cen = Math.Abs(mu_1_y - mu_2_y) / 2;
			/* Вычисление расстояния до центра от одного из образов, т.к. это центр – следовательно
			 расстояние от первого и второго образа одинаково. Берем данные первого образа.*/
			double distance = Math.Sqrt(Math.Pow(x_cen-mu_1_x, 2) + Math.Pow(y_cen-mu_1_y, 2));
			// определяем, что это расстояние больше чем среднеквадратическое отклонение любого образа
			// если меньше, то берем значение большего среднеквадратического отклонения
			if (sigma_1_x > distance)
			{
				distance = sigma_1_x;
			}
			if (sigma_1_y > distance)
			{
				distance = sigma_1_y;
			}
			if (sigma_2_x > distance)
			{
				distance = sigma_2_x;
			}
			if (sigma_2_y > distance)
			{
				distance = sigma_2_y;
			}
			// данное значение нам необходимо для построения поверхности определённого размера
			// чтобы её отображение на диаграмме было корректным и без смещения относительно других объектов
			// данное значение было определено эмпирическим путем
			var meshDataSeries_1 = Math_TPR.ConstructionSurfaceDistribution(x_cen, y_cen, distance, sigma_1_x, sigma_1_y, mu_1_x, mu_1_y, ro_1);
			meshDataSeries_1.SeriesName = "f(x,y/S1)";

			var meshDataSeries_2 = Math_TPR.ConstructionSurfaceDistribution(x_cen, y_cen, distance, sigma_2_x, sigma_2_y, mu_2_x, mu_2_y, ro_2);
			meshDataSeries_2.SeriesName = "f(x,y/S2)";
			// создание формы с графиком
			if (_graph != null)
			{
				_graph.Close();
			}
			_graph = new _3DGraph(xyzDataSeries3D1, xyzDataSeries3D2, xyzDataSeries3D_3, xyzDataSeries3D_4,
								 xyzDataSeries3D_5, xyzDataSeries3D_6, meshDataSeries_1, meshDataSeries_2);
			_graph.Show();
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
				_sigmaX1, _sigmaX2, _averageX1, _averageX2);
			textBox10.Text = x_por.ToString();
		}
		/*Определение порогового значения x по условию минимума числа ошибочных решений*/
		private void button2_Click(object sender, RoutedEventArgs e)
		{
			double x_por = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			double eps = 0.01;
			string input_buffer;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox24.Text;
				eps = Convert.ToDouble(input_buffer);

			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			x_por = Math_TPR.DetermThresholdValuesMinNumberOfErroneousDecisions(f_x_S1, f_x_S2, p1, p2, true,
				_sigmaX1, _sigmaX2, _averageX1, _averageX2, eps);
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
				_sigmaY1, _sigmaY2, _averageY1, _averageY2);
			textBox17.Text = y_por.ToString();		
		}
		/*Определение порогового значения y по условию минимума числа ошибочных решений*/
		private void button4_Click(object sender, RoutedEventArgs e)
		{
			double y_por = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			double eps = 0.01;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16_Copy.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19_Copy.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox24.Text;
				eps = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			y_por = Math_TPR.DetermThresholdValuesMinNumberOfErroneousDecisions(f_y_S1, f_y_S2, p1, p2, false,
				_sigmaY1, _sigmaY2, _averageY1, _averageY2, eps);
			textBox21.Text = y_por.ToString();		
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по X*/
		private void button5_Click(object sender, RoutedEventArgs e)
		{
			double x = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			double eps = 0.01;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox18.Text;
				x = Convert.ToDouble(input_buffer);
				input_buffer = textBox24.Text;
				eps = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			textBox22.Text = Math_TPR.DeterminationOfTheSiegert_Kotelnikov(f_x_S1, f_x_S2, x, p1, p2, 
				_sigmaX1, _sigmaX2, _averageX1, _averageX2, true, eps);
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по Y*/
		private void button6_Click(object sender, RoutedEventArgs e)
		{
			double y = 0.0;
			double p1 = 0.0;
			double p2 = 0.0;
			string input_buffer;
			double eps = 0.01;
			try
			{
				// ввод исходных данных
				input_buffer = textBox16_Copy.Text;
				p1 = Convert.ToDouble(input_buffer);
				input_buffer = textBox19_Copy.Text;
				p2 = Convert.ToDouble(input_buffer);
				input_buffer = textBox20.Text;
				y = Convert.ToDouble(input_buffer);
				input_buffer = textBox24.Text;
				eps = Convert.ToDouble(input_buffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			textBox23.Text = Math_TPR.DeterminationOfTheSiegert_Kotelnikov(f_y_S1, f_y_S2, y, p1, p2, 
				_sigmaY1, _sigmaY2, _averageY1, _averageY2, false, eps);
		}

		private void button7_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (_instructions != null)
			{
				_instructions.Close();
			}
			_instructions = new Instructions();
			_instructions.Show();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (_informationAboutProgram != null)
			{
				_informationAboutProgram.Close();
			}
			_informationAboutProgram = new InformationAboutProgram();
			_informationAboutProgram.Show();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			if (_userManual != null)
			{
				_userManual.Close();
			}
			_userManual = new UserManual();
			_userManual.Show();
		}
	}
}
