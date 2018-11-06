using Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
