using System;
using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace TPR2
{
	/// <summary>
	/// Логика взаимодействия для Instructions.xaml
	/// </summary>
	public partial class Instructions : System.Windows.Window
	{
		public Instructions()
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

