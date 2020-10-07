using System;
using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace TPR2.Forms
{
	/// <summary>
	/// Логика взаимодействия для InformationAboutProgram.xaml
	/// </summary>
	public partial class InformationAboutProgram : Window
	{

		public InformationAboutProgram()
		{
			InitializeComponent();
			try
			{
				string runningPath = Environment.CurrentDirectory + @"\InformationAboutProgram.xps";
				XpsDocument doc = new XpsDocument(runningPath, FileAccess.Read);
				documentViewer.Document = doc.GetFixedDocumentSequence();
				doc.Close();
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
