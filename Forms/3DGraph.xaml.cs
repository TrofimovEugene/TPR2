using SciChart.Charting3D.Model;
using System.Windows;
using SciChart.Charting3D;
using SciChart.Charting3D.Axis;
using SciChart.Charting3D.Modifiers;

namespace TPR2.Forms
{
	/// <summary>
	/// Логика взаимодействия для _3DGraph.xaml
	/// </summary>
	public partial class _3DGraph : Window
	{
		public _3DGraph(XyzDataSeries3D<double> xyzDataSeries3D_1, XyzDataSeries3D<double> xyzDataSeries3D_2,
						XyzDataSeries3D<double> xyzDataSeries3D_3, XyzDataSeries3D<double> xyzDataSeries3D_4,
						XyzDataSeries3D<double> xyzDataSeries3D_5, XyzDataSeries3D<double> xyzDataSeries3D_6,
						UniformGridDataSeries3D<double> uniformGridDataSeries_1, UniformGridDataSeries3D<double> uniformGridDataSeries_2)
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
			ScatterSeries3D_1.DataSeries = xyzDataSeries3D_1;
			ScatterSeries3D_2.DataSeries = xyzDataSeries3D_2;
			PointLineSeries3D.DataSeries = xyzDataSeries3D_3;
			PointLineSeries3D_1.DataSeries = xyzDataSeries3D_4;
			PointLineSeries3D_2.DataSeries = xyzDataSeries3D_5;
			PointLineSeries3D_3.DataSeries = xyzDataSeries3D_6;
			surfaceMeshRenderableSeries.DataSeries = uniformGridDataSeries_1;
			surfaceMeshRenderableSeries_2.DataSeries = uniformGridDataSeries_2;
		}
	}
}
