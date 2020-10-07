using System;
using System.Collections.Generic;
using System.Windows;
using SciChart.Charting3D.Model;
using TPR2.Classes;
using TPR2.Forms;
// ReSharper disable All

namespace TPR2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static List<Point> fXS1 = new List<Point>();
		public static List<Point> fXS2 = new List<Point>();
		public static List<Point> fYS1 = new List<Point>();
		public static List<Point> fYS2 = new List<Point>();
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
			_graph = new _3DGraph();
			InitializeComponent();
		}
		private void button_Click(object sender, RoutedEventArgs e)
		{
			var xyzDataSeries3D1 = new XyzDataSeries3D<double>();
			var xyzDataSeries3D2 = new XyzDataSeries3D<double>();
			_n1 = 0;
			_n2 = 0;
			var mu1X = 0.0;
			var mu1Y = 0.0;
			var mu2X = 0.0;
			var mu2Y = 0.0;
			var sigma1X = 0.0;
			var sigma1Y = 0.0;
			var sigma2X = 0.0;
			var sigma2Y = 0.0;
			var eps = 0.01;
			string inputBuffer;
			try
			{
				// ввод исходных данных
				inputBuffer = TextBox.Text;
				_n1 = Convert.ToInt32(inputBuffer);
				inputBuffer = TextBox1.Text;
				_n2 = Convert.ToInt32(inputBuffer);
				inputBuffer = TextBox2.Text;
				mu1X = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox3.Text;
				mu1Y = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox4.Text;
				mu2X = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox5.Text;
				mu2Y = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox6.Text;
				sigma1X = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox7.Text;
				sigma1Y = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox8.Text;
				sigma2X = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox9.Text;
				sigma2Y = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox24.Text;
				eps = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			List<Point> setP1;
			List<Point> setP2;
			var normRand = new NormalRandom();
			var set1 = new SetPoint(mu1X, sigma1X, mu1Y, sigma1Y, _n1, normRand);
			var set2 = new SetPoint(mu2X, sigma2X, mu2Y, sigma2Y, _n2, normRand);
			setP1 = set1.get_list_point();
			setP2 = set2.get_list_point();
			var ro1 = MathTpr.CalculationOfCorrelationCoefficient(setP1, mu1X, mu1Y, sigma1X, sigma1X);
			var ro2 = MathTpr.CalculationOfCorrelationCoefficient(setP2, mu2X, mu2Y, sigma2X, sigma2X);

			// определение фактических значений мат. ожиданий и среднеквадратических отклонений
			_averageX1 = set1.calculate_Average_Value_x();
			_averageY1 = set1.calculate_Average_Value_y();
			_averageX2 = set2.calculate_Average_Value_x();
			_averageY2 = set2.calculate_Average_Value_y();
			_sigmaX1 = Math.Sqrt(MathTpr.DispersionX(setP1, _averageX1));
			_sigmaY1 = Math.Sqrt(MathTpr.DispersionY(setP1, _averageY1));
			_sigmaX2 = Math.Sqrt(MathTpr.DispersionX(setP2, _averageX2));
			_sigmaY2 = Math.Sqrt(MathTpr.DispersionY(setP2, _averageY2));

			// построение точек на плоскости XY
			foreach (var item in setP1)
			{
				var x = item.X;
				var z = item.Y;
				item.Z = MathTpr.func_Gauss_XY(item.X, item.Y, sigma1X, sigma1Y, mu1X, mu1Y, ro1);
				var y = 0;
				xyzDataSeries3D1.Append(x, y, z);
			}
			foreach (var item in setP2)
			{
				var x = item.X;
				var z = item.Y;
				item.Z = MathTpr.func_Gauss_XY(item.X, item.Y, sigma2X, sigma2Y, mu2X, mu2Y, ro2);
				var y = 0;
				xyzDataSeries3D2.Append(x, y, z);
			}
			xyzDataSeries3D1.SeriesName = "S1";
			xyzDataSeries3D2.SeriesName = "S2";
			
			PointGrid.DataContext = setP1;
			PointGridCopy.DataContext = setP2;

			if (_graph != null)
			{
				_graph = new _3DGraph {Visibility = Visibility.Visible};
			}
			// создание формы с графиком
			_graph?.PaintGraph(set1, set2, sigma1X, sigma1Y, mu1X,
				mu1Y, mu2X, mu2Y, sigma2X, sigma2Y, _n1, _n2);
		} 
		/*Определение порогового значения x по условию минимума среднего риска*/
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			var xPor = 0.0;
			var c11 = -1.0;
			var c22 = -1.0;
			var c12 = 0.0;
			var c21 = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			string inputBuffer;
			try
			{
				// ввод исходных данных
				// ввод стоимости ошибок
				inputBuffer = TextBox14.Text;
				c12 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox15.Text;
				c21 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox16.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19.Text;
				p2 = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			xPor = MathTpr.DetermThresholdValuesMinAverageRisk(c11, c12, c21, c22, p1, p2, 
				_sigmaX1, _sigmaX2, _averageX1, _averageX2);
			TextBox10.Text = xPor.ToString();
		}
		/*Определение порогового значения x по условию минимума числа ошибочных решений*/
		private void button2_Click(object sender, RoutedEventArgs e)
		{
			var xPor = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			var eps = 0.01;
			string inputBuffer;
			try
			{
				// ввод исходных данных
				inputBuffer = TextBox16.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19.Text;
				p2 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox24.Text;
				eps = Convert.ToDouble(inputBuffer);

			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			xPor = MathTpr.DetermThresholdValuesMinNumberOfErroneousDecisionsX(fXS1, fXS2, p1, p2,
				_sigmaX1, _sigmaX2, _averageX1, _averageX2, eps);
			TextBox11.Text = xPor.ToString();
		}
		/*Определение порогового значения y по условию минимума среднего риска*/
		private void button3_Click(object sender, RoutedEventArgs e)
		{
			var yPor = 0.0;
			var c11 = -1.0;
			var c22 = -1.0;
			var c12 = 0.0;
			var c21 = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			string inputBuffer;
			try
			{
				// ввод исходных данных
				// ввод стоимости ошибок
				inputBuffer = TextBox14Copy.Text;
				c12 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox15Copy.Text;
				c21 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox16Copy.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19Copy.Text;
				p2 = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			yPor = MathTpr.DetermThresholdValuesMinAverageRisk(c11, c12, c21, c22, p1, p2,  
				_sigmaY1, _sigmaY2, _averageY1, _averageY2);
			TextBox17.Text = yPor.ToString();		
		}
		/*Определение порогового значения y по условию минимума числа ошибочных решений*/
		private void button4_Click(object sender, RoutedEventArgs e)
		{
			var yPor = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			string inputBuffer;
			var eps = 0.01;
			try
			{
				// ввод исходных данных
				inputBuffer = TextBox16Copy.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19Copy.Text;
				p2 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox24.Text;
				eps = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			yPor = MathTpr.DetermThresholdValuesMinNumberOfErroneousDecisionsY(fYS1, fYS2, p1, p2,
				_sigmaY1, _sigmaY2, _averageY1, _averageY2, eps);
			TextBox21.Text = yPor.ToString();		
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по X*/
		private void button5_Click(object sender, RoutedEventArgs e)
		{
			var x = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			string inputBuffer;
			var eps = 0.01;
			try
			{
				// ввод исходных данных
				inputBuffer = TextBox16.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19.Text;
				p2 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox18.Text;
				x = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox24.Text;
				eps = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			TextBox22.Text = MathTpr.DeterminationOfTheSiegert_KotelnikovX(fXS1, fXS2, x, p1, p2, 
				_sigmaX1, _sigmaX2, _averageX1, _averageX2, eps);
		}
		/*Проверка с помощью критерия Зигерта-Котельникова по Y*/
		private void button6_Click(object sender, RoutedEventArgs e)
		{
			var y = 0.0;
			var p1 = 0.0;
			var p2 = 0.0;
			string inputBuffer;
			var eps = 0.01;
			try
			{
				// ввод исходных данных
				inputBuffer = TextBox16Copy.Text;
				p1 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox19Copy.Text;
				p2 = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox20.Text;
				y = Convert.ToDouble(inputBuffer);
				inputBuffer = TextBox24.Text;
				eps = Convert.ToDouble(inputBuffer);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			TextBox23.Text = MathTpr.DeterminationOfTheSiegert_KotelnikovY(fYS1, fYS2, y, p1, p2, 
				_sigmaY1, _sigmaY2, _averageY1, _averageY2, eps);
		}

		private void button7_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			_instructions?.Close();
			_instructions = new Instructions();
			_instructions.Show();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			_informationAboutProgram?.Close();
			_informationAboutProgram = new InformationAboutProgram();
			_informationAboutProgram.Show();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			_userManual?.Close();
			_userManual = new UserManual();
			_userManual.Show();
		}
	}
}
