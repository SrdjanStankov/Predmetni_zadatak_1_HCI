using Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Predmetni_zadatak_1
{
	/// <summary>
	/// Interaction logic for AddComponentWindow.xaml
	/// </summary>
	public partial class AddComponentWindow : Window
	{
		private static int secretNumber = 1;
		private readonly Brush textBoxFrameBrushColor;
		private readonly Brush buttonFrameBrushColor;

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

		private void SetBorderBrushOfFields(Brush brush)
		{
			nameTextBox.BorderBrush = brush;
			descriptionTextBox.BorderBrush = brush;
			moneyTextBox.BorderBrush = brush;
			datePicker.BorderBrush = brush;
			fileDialogButton.BorderBrush = brush;
		}

		private bool Validate()
		{
			bool result = true;

			if (string.IsNullOrEmpty(nameTextBox.Text))
			{
				result = false;
				nameTextBox.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.Naziv = nameTextBox.Text;
			}
			if (string.IsNullOrEmpty(descriptionTextBox.Text))
			{
				result = false;
				descriptionTextBox.BorderBrush = Brushes.Red;
			}
			else
			{
				komponenta.Opis = descriptionTextBox.Text;
			}
			if (string.IsNullOrEmpty(moneyTextBox.Text))
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
			nameTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			descriptionTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void MoneyTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			moneyTextBox.BorderBrush = textBoxFrameBrushColor;
		}
		private void DatePicker_GotFocus(object sender, RoutedEventArgs e)
		{
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
			var fileDialog = new OpenFileDialog
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
				var bco = BitmapCreateOptions.PreservePixelFormat;
				var bcco = BitmapCacheOption.Default;

				string extension = fileDialog.SafeFileName.Split('.').Last();
				switch (extension)
				{
					case "bmp":
						var bmpDecoder = new BmpBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = bmpDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "gif":
						var gifDecoder = new GifBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = gifDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "ico":
						var icoDecoder = new IconBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = icoDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "jpg":
						var jpgDecoder = new JpegBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = jpgDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "png":
						var pngDecoder = new PngBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = pngDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "tif":
						var tiffDecoder = new TiffBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
						bitmapSource = tiffDecoder.Frames[0];
						imageHolder.Source = bitmapSource;
						break;
					case "wmp":
						var wmpDecoder = new WmpBitmapDecoder(new Uri(fileDialog.FileName), bco, bcco);
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
				using (var reader = new StreamReader(Environment.CurrentDirectory + "\\komponenta" + secretNumber + ".txt"))
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
						var img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\mis.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}
					else if (secretNumber == 2) // ucitavanje za tastaturu
					{
						for (int i = 0; i < 3; i++)
						{
							descriptionTextBox.Text += reader.ReadLine();
							descriptionTextBox.Text += Environment.NewLine;
						}
						var img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\tastatura.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}
					else if (secretNumber == 3)
					{
						for (int i = 0; i < 12; i++)
						{
							descriptionTextBox.Text += reader.ReadLine();
							descriptionTextBox.Text += Environment.NewLine;
						}
						var img = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\slusalice.png"));
						imageHolder.Source = (ImageSource)imgcon.ConvertFrom(img.UriSource);
					}

					moneyTextBox.Text = reader.ReadLine();

					secretNumber++;
				}

				SetBorderBrushOfFields(textBoxFrameBrushColor);
			}
		}

	}
}
