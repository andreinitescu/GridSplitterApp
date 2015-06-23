using GridSplitterApp.Controls;
using GridSplitterApp.WinPhone.Renderers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRendererAttribute(typeof(GridSplitter), typeof(GridSplitterRenderer))]

namespace GridSplitterApp.WinPhone.Renderers
{
    public class GridSplitterRenderer : ViewRenderer
    {
        private System.Windows.Point? _lastPt;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                this.MouseLeftButtonDown -= GridSplitterRenderer_MouseLeftButtonDown;
                this.MouseLeftButtonUp -= GridSplitterRenderer_MouseLeftButtonUp;
                this.MouseMove -= GridSplitterRenderer_MouseMove;
            }

            if (e.NewElement != null)
            {
                this.MouseLeftButtonDown += GridSplitterRenderer_MouseLeftButtonDown;
                this.MouseLeftButtonUp += GridSplitterRenderer_MouseLeftButtonUp;
                this.MouseMove += GridSplitterRenderer_MouseMove;
            }
        }

        void GridSplitterRenderer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastPt = e.GetPosition(null);
            this.CaptureMouse();
        }

        void GridSplitterRenderer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_lastPt != null)
            {
                System.Windows.Point pt = e.GetPosition(null);
                (Element as GridSplitter).UpdateGrid(pt.X - _lastPt.Value.X, pt.Y - _lastPt.Value.Y);
                _lastPt = pt;
            }
        }

        void GridSplitterRenderer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastPt = null;
        }
    }
}
