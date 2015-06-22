using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace GridSplitterApp.Controls
{
	[ContentProperty ("Type")]
	public class StyleExtension : IMarkupExtension
	{
		public string Type { get; set; }

		#region IMarkupExtension implementation

		public object ProvideValue (IServiceProvider serviceProvider)
		{
			IXamlTypeResolver resolver = serviceProvider.GetService (typeof(IXamlTypeResolver)) as IXamlTypeResolver;
			if (resolver == null)
				return null;
			var type = resolver.Resolve (Type);

			var s = Application.Current.Resources [type.FullName] as Style;
			return s;
		}

		#endregion
	}
}

