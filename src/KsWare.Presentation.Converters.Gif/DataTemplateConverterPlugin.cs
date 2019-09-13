using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;
using WpfAnimatedGif;

namespace KsWare.Presentation.Converters.Gif
{
	public class DataTemplateConverterPlugin
	{
		public DataTemplate CreateDataTemplate(Uri locationUri)
		{
			// REQUIRES: PM> Install-Package WpfAnimatedGif

			ImageBehavior.SetAnimatedSource(new Image(), new BitmapImage()); //WORKAROUND

			var dataTemplateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:gif=""http://wpfanimatedgif.codeplex.com"">
	<Image gif:ImageBehavior.AnimatedSource=""{locationUri}"" Stretch=""Uniform"" />
</DataTemplate>";

			var sr = new StringReader(dataTemplateXaml);
			var xr = XmlReader.Create(sr);
			return (DataTemplate)XamlReader.Load(xr);
		}
	}
}
