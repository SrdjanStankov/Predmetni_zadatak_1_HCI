using Classes;
using System.ComponentModel;
using System.Windows;

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
			var window = new AddComponentWindow
			{
				Owner = this
			};
			window.ShowDialog();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			Komponente.RemoveAt(gridObjects.SelectedIndex);
		}

		private void Details_Click(object sender, RoutedEventArgs e)
		{
			var detailWindow = new DetailWindow(Komponente[gridObjects.SelectedIndex])
			{
				Owner = this
			};
			detailWindow.ShowDialog();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			var window = new AddComponentWindow(Komponente[gridObjects.SelectedIndex], true)
			{
				Owner = this
			};
			window.Show();
		}
	}
}
