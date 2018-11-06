using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Classes
{
	[Serializable]
	public class Komponenta
	{
		public string Naziv { get; set; }
		public string Opis { get; set; }
		public int Cena { get; set; }
		public DateTime DatumDodavanja { get; set; }
		[XmlIgnore]
		public BitmapSource ImageSource { get; set; }

		public Komponenta(string naziv, string opis, int cena, DateTime datumDodavanja, BitmapSource bitmapSource)
		{
			Naziv = naziv;
			Opis = opis;
			Cena = cena;
			DatumDodavanja = datumDodavanja;
			ImageSource = bitmapSource;
		}

		public Komponenta()
		{

		}
	}
}
