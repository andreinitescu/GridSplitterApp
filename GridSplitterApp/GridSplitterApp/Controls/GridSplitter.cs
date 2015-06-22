using System;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace GridSplitterApp.Controls
{
	public class GridSplitter : ContentView
	{
		#region ControlTemplateProperty

		public static readonly BindableProperty ControlTemplateProperty =
			BindableProperty.Create<GridSplitter, DataTemplate> (
				p => p.ControlTemplate, default(DataTemplate), BindingMode.Default, null, (bo, oldCT, newCT) => {
				(bo as GridSplitter).Content = (View)newCT.CreateContent ();
			});

		public DataTemplate ControlTemplate {
			get {
				return (DataTemplate)GetValue (ControlTemplateProperty);
			}
			set {
				SetValue (ControlTemplateProperty, value);
			}
		}

		#endregion

		public void UpdateGrid (double dragOffsetX, double dragOffsetY)
		{
			if (Parent as Grid == null) {
				return;
			}

			if (IsRowSplitter ()) {
				UpdateRow (dragOffsetY);
			} else
				UpdateColumn (dragOffsetX);
		}

		private bool IsRowSplitter ()
		{
			return HorizontalOptions.Alignment == LayoutAlignment.Fill;
		}

		private void UpdateRow (double offsetY)
		{
			if (offsetY == 0) {
				return;
			}

			var grid = Parent as Grid;
			var row = Grid.GetRow (this);
			int rowCount = grid.RowDefinitions.Count ();
			if (rowCount <= 1 ||
			    row == 0 ||
			    row == rowCount - 1 ||
			    row + Grid.GetRowSpan (this) >= rowCount) {
				return;
			}

			RowDefinition rowAbove = grid.RowDefinitions [row - 1];
			var actualHeight = GetRowDefinitionActualHeight (rowAbove) + offsetY;
			if (actualHeight < 0) {
				actualHeight = 0;
			}

			rowAbove.Height = new GridLength (actualHeight);
		}

		private void UpdateColumn (double offsetX)
		{
			if (offsetX == 0) {
				return;
			}

			var grid = Parent as Grid;
			var column = Grid.GetColumn (this);
			int columnCount = grid.ColumnDefinitions.Count ();
			if (columnCount <= 1 ||
				column == 0 ||
				column == columnCount - 1 ||
				column + Grid.GetColumnSpan (this) >= columnCount) {
				return;
			}

			ColumnDefinition columnLeft = grid.ColumnDefinitions [column - 1];
			var actualWidth = GetColumnDefinitionActualWidth (columnLeft) + offsetX;
			if (actualWidth < 0) {
				actualWidth = 0;
			}

			columnLeft.Width = new GridLength (actualWidth);
		}

		static private double GetRowDefinitionActualHeight (RowDefinition row)
		{
			double actualHeight;
			if (row.Height.IsAbsolute) {
				actualHeight = row.Height.Value;
			} else {
				var property = row.GetType ().GetRuntimeProperties ().First ((p) => p.Name == "ActualHeight");
				actualHeight = (double)property.GetValue (row);
			}
			return actualHeight;
		}

		static private double GetColumnDefinitionActualWidth (ColumnDefinition column)
		{
			double actualWidth;
			if (column.Width.IsAbsolute) {
				actualWidth = column.Width.Value;
			} else {
				var property = column.GetType ().GetRuntimeProperties ().First ((p) => p.Name == "ActualWidth");
				actualWidth = (double)property.GetValue (column);
			}
			return actualWidth;
		}
	}
}

