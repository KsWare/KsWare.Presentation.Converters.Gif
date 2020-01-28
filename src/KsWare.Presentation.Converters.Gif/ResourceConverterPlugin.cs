using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using KsWare.Presentation.Interfaces.Plugins.ResourceConverter;
using XamlAnimatedGif;

// AnimationBehavior.SetSourceUri REQUIRES: PM> Install-Package XamlAnimatedGif
// AnimationBehavior.SetSourceUri REQUIRES: PM> Install-Package KsWare.XamlAnimatedGif.Wpf.StrongName.1.2.2 (temporary)

namespace KsWare.Presentation.Converters.Gif {

	[Export(typeof(IResourceConverterPlugin)), ResourceConverterPluginExportMetadata("image/gif")]
	public sealed class ResourceConverterPlugin : IResourceConverterPlugin {

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (typeof(DataTemplate).IsAssignableFrom(targetType)) return CreateDataTemplate(value);
			if (typeof(ControlTemplate).IsAssignableFrom(targetType)) return CreateControlTemplate(value);
			return CreateImage(value);
			throw new NotImplementedException("Conversion not supported.");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}

		private DataTemplate CreateImage(object content) {
			var locationUri = (Uri)content;
			AnimationBehavior.SetSourceUri(new Image(), new Uri("pack://application,,,")); //WORKAROUND

			var templateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Image xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:gif=""https://github.com/XamlAnimatedGif/XamlAnimatedGif"" 
       gif:AnimationBehavior.SourceUri=""{locationUri}"" Stretch=""Uniform"" />
";

			var sr = new StringReader(templateXaml);
			var xr = XmlReader.Create(sr);
			return (DataTemplate)XamlReader.Load(xr);
		}

		private DataTemplate CreateDataTemplate(object content) {
			var locationUri = (Uri) content;
			AnimationBehavior.SetSourceUri(new Image(), new Uri("pack://application,,,")); //WORKAROUND

			var templateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:gif=""https://github.com/XamlAnimatedGif/XamlAnimatedGif"">
	<Image gif:AnimationBehavior.SourceUri=""{locationUri}"" Stretch=""Uniform"" />
</DataTemplate>";

			var sr = new StringReader(templateXaml);
			var xr = XmlReader.Create(sr);
			return (DataTemplate) XamlReader.Load(xr);
		}

		private ControlTemplate CreateControlTemplate(object content) {
			var locationUri = (Uri) content;
			AnimationBehavior.SetSourceUri(new Image(), new Uri("pack://application,,,")); //WORKAROUND

			var templateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<ControlTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:gif=""https://github.com/XamlAnimatedGif/XamlAnimatedGif"">
	<Image gif:AnimationBehavior.SourceUri=""{locationUri}"" Stretch=""Uniform"" />
</ControlTemplate>";

			var sr = new StringReader(templateXaml);
			var xr = XmlReader.Create(sr);
			return (ControlTemplate) XamlReader.Load(xr);
		}
	}

}
