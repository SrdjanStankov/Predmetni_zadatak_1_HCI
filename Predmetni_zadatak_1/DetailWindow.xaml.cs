using Classes;
using System.Windows;

namespace Predmetni_zadatak_1
{
	/// <summary>
	/// Interaction logic for DetailWindow.xaml
	/// </summary>
	public partial class DetailWindow : Window
	{
		public DetailWindow()
		{
			InitializeComponent();
		}

		public DetailWindow(Komponenta k)
		{
			InitializeComponent();

			nameLabel.Content = k.Naziv;
			descLabel.Text = k.Opis;
			moneyLabel.Content = k.Cena.ToString() + " RSD";
			dateLabel.Content = k.DatumDodavanja.ToShortDateString();
			image.Source = k.ImageSource;
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
