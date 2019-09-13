using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using XamlAnimatedGif;

namespace KsWare.Presentation.Converters.Gif
{
	public class DataTemplateConverterPlugin
	{
		public DataTemplate CreateDataTemplate(Uri locationUri)
		{
			// REQUIRES: PM> Install-Package XamlAnimatedGif
			// REQUIRES: PM> Install-Package KsWare.XamlAnimatedGif.Wpf.StrongName.1.2.2 (temporary)

			AnimationBehavior.SetSourceUri(new Image(), new Uri("pack://application,,,")); //WORKAROUND

			var dataTemplateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:gif=""https://github.com/XamlAnimatedGif/XamlAnimatedGif"">
	<Image gif:AnimationBehavior.SourceUri=""{locationUri}"" Stretch=""Uniform"" />
</DataTemplate>";

			var sr = new StringReader(dataTemplateXaml);
			var xr = XmlReader.Create(sr);
			return (DataTemplate)XamlReader.Load(xr);
		}
	}
}
