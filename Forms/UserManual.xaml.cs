using System;
using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace TPR2.Forms
{
	/// <summary>
	/// Логика взаимодействия для UserManual.xaml
	/// </summary>
	public partial class UserManual : Window
	{
		public UserManual()
		{
			InitializeComponent();
			try
			{
				string p = Directory.GetCurrentDirectory();
				p = p.Replace("bin", "");
				p = p.Replace("Debug", "");
				p += @"Files\UserManual.xps";
				XpsDocument doc = new XpsDocument(p, FileAccess.Read);
				documentViewer.Document = doc.GetFixedDocumentSequence();
				doc.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
