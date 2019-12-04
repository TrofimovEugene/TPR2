using System;
using System.IO;
using System.Reflection;
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
				string RunningPath = Environment.CurrentDirectory;
				XpsDocument doc = new XpsDocument(RunningPath, FileAccess.Read);
				documentViewer.Document = doc.GetFixedDocumentSequence();
				doc.Close();
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
