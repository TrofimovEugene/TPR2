using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
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
			string p = Directory.GetCurrentDirectory();
			p = p.Replace("bin", "");
			p = p.Replace("Debug", "");
			p += @"Files\TPR_Lektsia_4.xps";
			XpsDocument doc = new XpsDocument(p, FileAccess.Read);
			documentViewer.Document = doc.GetFixedDocumentSequence();
			doc.Close();
		}
	}
}

