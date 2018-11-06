using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using Classes;

namespace Predmetni_zadatak_1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static BindingList<Komponenta> Komponente { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			if (Komponente == null)
			{
				Komponente = new BindingList<Komponenta>();
			}

			DataContext = this;
		}

		private void ExitButtonClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void AddButtonClick(object sender, RoutedEventArgs e)
		{
			AddComponentWindow window = new AddComponentWindow();
			window.ShowDialog();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			Komponente.RemoveAt(gridObjects.SelectedIndex);
		}

		private void Details_Click(object sender, RoutedEventArgs e)
		{
			int i = gridObjects.SelectedIndex;
			DetailWindow detailWindow = new DetailWindow(Komponente[i]);
			detailWindow.ShowDialog();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			int i = gridObjects.SelectedIndex;
			AddComponentWindow window = new AddComponentWindow(Komponente[i], true);
			window.Show();
		}
	}
}
