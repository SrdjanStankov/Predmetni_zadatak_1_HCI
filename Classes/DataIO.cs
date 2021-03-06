﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Classes
{
	public static class DataIO
	{
		public static void SerializeObject<T>(T serializableObject, string fileName)
		{
			if (serializableObject == null) { return; }

			try
			{
				var xmlDocument = new XmlDocument();
				var serializer = new XmlSerializer(serializableObject.GetType());
				using (var stream = new MemoryStream())
				{
					serializer.Serialize(stream, serializableObject);
					stream.Position = 0;
					xmlDocument.Load(stream);
					xmlDocument.Save(fileName);
					stream.Close();
				}
			}
			catch (Exception)
			{
				//Log exception here
			}
		}

		public static T DeSerializeObject<T>(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) { return default; }

			var objectOut = default(T);

			try
			{
				string attributeXml = string.Empty;

				var xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				string xmlString = xmlDocument.OuterXml;

				using (var read = new StringReader(xmlString))
				{
					var outType = typeof(T);

					var serializer = new XmlSerializer(outType);
					using (XmlReader reader = new XmlTextReader(read))
					{
						objectOut = (T)serializer.Deserialize(reader);
						reader.Close();
					}

					read.Close();
				}
			}
			catch (Exception)
			{
				//Log exception here
			}

			return objectOut;
		}
	}
}
