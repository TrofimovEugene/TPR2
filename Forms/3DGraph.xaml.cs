using System;
using SciChart.Charting3D.Model;
using System.Windows;
using SciChart.Charting.Visuals;
using SciChart.Charting3D;
using SciChart.Charting3D.Axis;
using SciChart.Charting3D.Modifiers;
using TPR2.Classes;

namespace TPR2.Forms
{
	// ReSharper disable CommentTypo
	// ReSharper disable StringLiteralTypo
	/// <summary>
	/// Логика взаимодействия для _3DGraph.xaml
	/// </summary>
	public partial class _3DGraph : Window
	{
		public _3DGraph()
		{
			SciChartSurface.SetRuntimeLicenseKey(
				
				@"<LicenseContract>
				   <Customer>IRGUPS</Customer>
				   <OrderId>EDUCATIONAL-USE-0137</OrderId>
				   <LicenseCount>1</LicenseCount>
				   <IsTrialLicense>false</IsTrialLicense>
				   <SupportExpires>02/10/2021 00:00:00</SupportExpires>
				   <ProductCode>SC-WPF-SDK-PRO</ProductCode>
				   <KeyCode>lwAAAQEAAAD5I83qYuLVAYIAQ3VzdG9tZXI9SVJHVVBTO09yZGVySWQ9RURVQ0FUSU9OQUwtVVNFLTAxMzc7U3Vic2NyaXB0aW9uVmFsaWRUbz0xMC1GZWItMjAyMTtQcm9kdWN0Q29kZT1TQy1XUEYtU0RLLVBSTztOdW1iZXJEZXZlbG9wZXJzT3ZlcnJpZGU9MXRC1DrVIb2fNVT7eY8vX6L9nySC1+GVpdKsEApUFAC0K1k7hYwXqlKXhOIoilgwPQ==</KeyCode>
				 </LicenseContract>");
			var sciChart3DSurface = new SciChart3DSurface
			{
				IsAxisCubeVisible = true,
				IsFpsCounterVisible = true,
				IsXyzGizmoVisible = true,
				XAxis = new NumericAxis3D(),
				YAxis = new NumericAxis3D(),
				ZAxis = new NumericAxis3D(),
				ChartModifier = new ModifierGroup3D(
					new OrbitModifier3D(),
					new ZoomExtentsModifier3D())
			};
			// Create the X,Y,Z Axis
			// Specify Interactivity Modifiers
			InitializeComponent();
			SciChartSurface.SetRuntimeLicenseKey(
				@"<LicenseContract>
				   <Customer>IRGUPS</Customer>
				   <OrderId>EDUCATIONAL-USE-0137</OrderId>
				   <LicenseCount>1</LicenseCount>
				   <IsTrialLicense>false</IsTrialLicense>
				   <SupportExpires>02/10/2021 00:00:00</SupportExpires>
				   <ProductCode>SC-WPF-SDK-PRO</ProductCode>
				   <KeyCode>lwAAAQEAAAD5I83qYuLVAYIAQ3VzdG9tZXI9SVJHVVBTO09yZGVySWQ9RURVQ0FUSU9OQUwtVVNFLTAxMzc7U3Vic2NyaXB0aW9uVmFsaWRUbz0xMC1GZWItMjAyMTtQcm9kdWN0Q29kZT1TQy1XUEYtU0RLLVBSTztOdW1iZXJEZXZlbG9wZXJzT3ZlcnJpZGU9MXRC1DrVIb2fNVT7eY8vX6L9nySC1+GVpdKsEApUFAC0K1k7hYwXqlKXhOIoilgwPQ==</KeyCode>
				 </LicenseContract>");
		}

		public void PaintGraph(SetPoint set1, SetPoint set2, double sigma1X, double sigma1Y, double mu1X,
			double mu1Y, double mu2X, double mu2Y, double sigma2X, double sigma2Y, int n1, int n2)
		{
			var xCen = 0.0;
			var yCen = 0.0;
			var setP1 = set1.get_list_point();
			var setP2 = set2.get_list_point();
			var xyzDataSeries3D1 = new XyzDataSeries3D<double>();
			var xyzDataSeries3D2 = new XyzDataSeries3D<double>();
			var ro1 = MathTpr.CalculationOfCorrelationCoefficient(setP1, mu1X, mu1Y, sigma1X, sigma1X);
			var ro2 = MathTpr.CalculationOfCorrelationCoefficient(setP2, mu2X, mu2Y, sigma2X, sigma2X);
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

			
			// построение плотностей проекций множеств S1 и S2
			// данные полученные из практически заданных СВ величин
			var averageX1 = set1.calculate_Average_Value_x();
			var averageY1 = set1.calculate_Average_Value_y();
			var averageX2 = set2.calculate_Average_Value_x();
			var averageY2 = set2.calculate_Average_Value_y();
			// построение плотности проекции f(x/S1)
			var dispersionX1 = MathTpr.DispersionX(setP1, averageX1);
			var sigmaX1 = Math.Sqrt(dispersionX1);
			var xyzDataSeries3D3 = MathTpr.f_x_Si(averageX1, sigmaX1, n1, MainWindow.fXS1);
			xyzDataSeries3D3.SeriesName = "f(x/S1)";
			// построение плотности проекции f(x/S2)
			var dispersionX2 = MathTpr.DispersionX(setP2, averageX2);
			var sigmaX2 = Math.Sqrt(dispersionX2);
			var xyzDataSeries3D4 = MathTpr.f_x_Si(averageX2, sigmaX2, n2, MainWindow.fXS2);
			xyzDataSeries3D4.SeriesName = "f(x/S2)";
			// построение плотности проекции f(y/S1)
			var dispersionY1 = MathTpr.DispersionY(setP1, averageY1);
			var sigmaY1 = Math.Sqrt(dispersionY1);
			var xyzDataSeries3D5 = MathTpr.f_y_Si(averageY1, sigmaY1, n1, MainWindow.fYS1);
			xyzDataSeries3D5.SeriesName = "f(y/S1)";
			// построение плотности проекции f(y/S2)
			var dispersionY2 = MathTpr.DispersionY(setP2, averageY2);
			var sigmaY2 = Math.Sqrt(dispersionY2);
			var xyzDataSeries3D6 = MathTpr.f_y_Si(averageY2, sigmaY2, n2, MainWindow.fYS2);
			xyzDataSeries3D6.SeriesName = "f(y/S2)";
			// нахождение центра между образами
			xCen = Math.Abs(mu1X - mu2X) / 2;
			yCen = Math.Abs(mu1Y - mu2Y) / 2;
			/* Вычисление расстояния до центра от одного из образов, т.к. это центр – следовательно
			 расстояние от первого и второго образа одинаково. Берем данные первого образа.*/
			var distance = Math.Sqrt(Math.Pow(xCen - mu1X, 2) + Math.Pow(yCen - mu1Y, 2));
			// определяем, что это расстояние больше чем среднеквадратическое отклонение любого образа
			// если меньше, то берем значение большего среднеквадратического отклонения
			if (sigma1X > distance)
			{
				distance = sigma1X;
			}
			if (sigma1Y > distance)
			{
				distance = sigma1Y;
			}
			if (sigma2X > distance)
			{
				distance = sigma2X;
			}
			if (sigma2Y > distance)
			{
				distance = sigma2Y;
			}
			// данное значение нам необходимо для построения поверхности определённого размера
			// чтобы её отображение на диаграмме было корректным и без смещения относительно других объектов
			// данное значение было определено эмпирическим путем
			var meshDataSeries1 = MathTpr.ConstructionSurfaceDistribution(xCen, yCen, distance, sigma1X, sigma1Y, mu1X, mu1Y, ro1);
			meshDataSeries1.SeriesName = "f(x,y/S1)";

			var meshDataSeries2 = MathTpr.ConstructionSurfaceDistribution(xCen, yCen, distance, sigma2X, sigma2Y, mu2X, mu2Y, ro2);
			meshDataSeries2.SeriesName = "f(x,y/S2)";

			ScatterSeries3D_1.DataSeries = xyzDataSeries3D1;
			ScatterSeries3D_2.DataSeries = xyzDataSeries3D2;
			PointLineSeries3D.DataSeries = xyzDataSeries3D3;
			PointLineSeries3D_1.DataSeries = xyzDataSeries3D4;
			PointLineSeries3D_2.DataSeries = xyzDataSeries3D5;
			PointLineSeries3D_3.DataSeries = xyzDataSeries3D6;
			surfaceMeshRenderableSeries.DataSeries = meshDataSeries1;
			surfaceMeshRenderableSeries_2.DataSeries = meshDataSeries2;
		}
	}
}
