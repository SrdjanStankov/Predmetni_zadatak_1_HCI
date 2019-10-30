using Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
	/// Interaction logic for AddComponentWindow.xaml
	/// </summary>
	public partial class AddComponentWindow : Window
	{
		private static int secretNumber = 1;

		readonly Brush textBoxFrameBrushColor;
		readonly Brush buttonFrameBrushColor;

		private BitmapSource bitmapSource;
		private Komponenta komponenta;

		private bool isEditing = false;
		private int index = -1;


		public AddComponentWindow()
		{
			InitializeComponent();

			komponenta = new Komponenta();

			textBoxFrameBrushColor = nameTextBox.BorderBrush;
			buttonFrameBrushColor = addButton.BorderBrush;
		}

		private AddComponentWindow(Komponenta k) : this()
		{
			komponenta = k;

			FillAllFields(komponenta);

			SetForegroundOfFields(Brushes.Black);
		}

		public AddComponentWindow(Komponenta k, bool change) : this(k)
		{
			isEditing = change;
			index = MainWindow.Komponente.IndexOf(k);
			if (change)
			{
				addButton.Content = "Izmeni";
			}
		}

		private void FillAllFields(Komponenta k)
		{
			nameTextBox.Text = k.Naziv;
			descriptionTextBox.Text = k.Opis;
			moneyTextBox.Text = k.Cena.ToString();
			datePicker.SelectedDate = k.DatumDodavanja;
			imageHolder.Source = k.ImageSource;
		}


		private void SetForegroundOfFields(SolidColorBrush brush)
		{
			nameTextBox.Foreground = brush;
			descriptionTextBox.Foreground = brush;
			moneyTextBox.Foreground = brush;
			datePicker.Foreground = brush;
		}

		private bool Validate()
		{
			bool result = true;

			if (nameTextBox.Text == "")
			{
				result = false;
				nameTextBox.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.Naziv = nameTextBox.Text;
			}
			if (descriptionTextBox.Text == "")
			{
				result = false;
				descriptionTextBox.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.Opis = descriptionTextBox.Text;
			}
			if (moneyTextBox.Text == "")
			{
				result = false;
				moneyTextBox.BorderBrush = Brushes.Red;
			}
			else
			{
				try
				{
					komponenta.Cena = Convert.ToInt32(moneyTextBox.Text);
					if (komponenta.Cena < 0)
					{
						throw new Exception();
					}
				}
				catch (Exception)
				{
					result = false;
					moneyTextBox.BorderBrush = Brushes.Red;
				}
			}
			if (datePicker.SelectedDate == null)
			{
				result = false;
				datePicker.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.DatumDodavanja = datePicker.SelectedDate.Value;
			}
			if (imageHolder.Source == null)
			{
				result = false;
				fileDialogButton.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.ImageSource = (BitmapSource)imageHolder.Source;
			}

			return result;
		}

		private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (nameTextBox.Text == "")
			{
				nameTextBox.Text = "";
				nameTextBox.Foreground = Brushes.Black;
			}
			nameTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (descriptionTextBox.Text == "")
			{
				descriptionTextBox.Text = "";
				descriptionTextBox.Foreground = Brushes.Black;
			}
			descriptionTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void MoneyTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (moneyTextBox.Text == "")
			{
				moneyTextBox.Text = "";
				moneyTextBox.Foreground = Brushes.Black;
			}
			moneyTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void DatePicker_GotFocus(object sender, RoutedEventArgs e)
		{
			if (datePicker.SelectedDate != null)
			{
				datePicker.Foreground = Brushes.Black;
			}
		}

		private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (nameTextBox.Text == "")
			{
				nameTextBox.Text = "";
				nameTextBox.Foreground = Brushes.LightSlateGray;
			}
		}
		private void DescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (descriptionTextBox.Text == "")
			{
				descriptionTextBox.Text = "";
				descriptionTextBox.Foreground = Brushes.LightSlateGray;
			}
		}
		private void MoneyTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (moneyTextBox.Text == "")
			{
				moneyTextBox.Text = "";
				moneyTextBox.Foreground = Brushes.LightSlateGray;
			}
		}
		private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
		{
			if (datePicker.SelectedDate == null)
			{
				datePicker.Foreground = Brushes.LightSlateGray;
			}
		}

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			datePicker.Foreground = Brushes.Black;
			datePicker.BorderBrush = textBoxFrameBrushColor;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (Validate())
			{
				if (isEditing)
				{
					MainWindow.Komponente[index] = komponenta;
				}
				else
				{
					MainWindow.Komponente.Add(komponenta);
				}
				Close();
			}
			else
			{
				MessageBox.Show("Polja nisu dobro popunjena!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void FileDialogButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
			{
				Filter = "Images |*.png;*.jpg;*.bmp;*.gif;*.tif;*.wmp;*.ico",
				CheckFileExists = true,
				CheckPathExists = true,
				Multiselect = false,
				Title = "Odaberi sliku",
				
			};
			fileDialogButton.BorderBrush = buttonFrameBrushColor;

			// Kad se selektuje fajl
			if (fileDialog.ShowDialog() == true)
			{
				BitmapCreateOptions bco = BitmapCreateOptions.PreservePixelFormat;
				BitmapCacheOption bcco = BitmapCacheOption.Default;

				string extension = fileDialog.SafeFileName.Split('.').Last();
				switch (extension)
				{
					case "bmp":
						BmpBitmapDecoder bmpDecoder = new BmpBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = bmpDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "gif":
						GifBitmapDecoder gifDecoder = new GifBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = gifDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "ico":
						IconBitmapDecoder icoDecoder = new IconBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = icoDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "jpg":
						JpegBitmapDecoder jpgDecoder = new JpegBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = jpgDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "png":
						PngBitmapDecoder pngDecoder = new PngBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = pngDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "tif":
						TiffBitmapDecoder tiffDecoder = new TiffBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = tiffDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "wmp":
						WmpBitmapDecoder wmpDecoder = new WmpBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = wmpDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					default:
						MessageBox.Show("Image format not suported", "Extension Error", MessageBoxButton.OK, MessageBoxImage.Error);
						break;
				}
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void SecretButton_Click(object sender, RoutedEventArgs e)
		{
			if (secretNumber < 4)
			{
				using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\komponenta" + secretNumber + ".txt"))
				{
					var imgcon = new ImageSourceConverter();
					datePicker.SelectedDate = DateTime.Now;

					nameTextBox.Text = reader.ReadLine();
					descriptionTextBox.Text = "";
					if (secretNumber == 1) // ucitavanje za mis
					{
						for (int i = 0; i < 7; i++)
						{
							descriptionTextBox.Text += reader.ReadLine();
							descriptionTextBox.Text += Environment.NewLine;
						}
						BitmapImage img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\mis.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}
					else if (secretNumber == 2) // ucitavanje za tastaturu
					{
						for (int i = 0; i < 3; i++)
						{
							descriptionTextBox.Text += reader.ReadLine();
							descriptionTextBox.Text += Environment.NewLine;
						}
						BitmapImage img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\tastatura.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}
					else if (secretNumber == 3)
					{
						for (int i = 0; i < 12; i++)
						{
							descriptionTextBox.Text += reader.ReadLine();
							descriptionTextBox.Text += Environment.NewLine;
						}
						BitmapImage img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\slusalice.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}

					moneyTextBox.Text = reader.ReadLine();

					SetForegroundOfFields(Brushes.Black);
					secretNumber++; 
				}
			}
		}
	}
}
