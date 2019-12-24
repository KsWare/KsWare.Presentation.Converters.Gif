using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using KsWare.Presentation.Interfaces.Plugins.TemplateConverter;
using XamlAnimatedGif;

// AnimationBehavior.SetSourceUri REQUIRES: PM> Install-Package XamlAnimatedGif
// AnimationBehavior.SetSourceUri REQUIRES: PM> Install-Package KsWare.XamlAnimatedGif.Wpf.StrongName.1.2.2 (temporary)

namespace KsWare.Presentation.Converters.Gif {

	[Export(typeof(ITemplateConverterPlugin)), TemplateConverterPluginExportMetadata("image/gif")]
	public sealed class TemplateConverterPlugin : ITemplateConverterPlugin {

		public DataTemplate CreateDataTemplate(object content) {
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

		public ControlTemplate CreateControlTemplate(object content) {
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
