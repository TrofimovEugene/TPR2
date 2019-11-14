﻿using System;
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
				string p = Directory.GetCurrentDirectory();
				p = p.Replace("bin", "");
				p = p.Replace("Debug", "");
				p += @"Files\Theory.xps";
				XpsDocument doc = new XpsDocument(p, FileAccess.Read);
				documentViewer.Document = doc.GetFixedDocumentSequence();
				doc.Close();
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}

