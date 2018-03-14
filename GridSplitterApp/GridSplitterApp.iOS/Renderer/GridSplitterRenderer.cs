using CoreGraphics;
using Foundation;
using GridSplitterApp.iOS.Renderer;
using GridSplitterApp.Controls;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GridSplitter), typeof(GridSplitterRenderer))]

namespace GridSplitterApp.iOS.Renderer
{
    public class GridSplitterRenderer : VisualElementRenderer<GridSplitter>
    {
        private UIPanGestureRecognizer _panGestureRecognizer;
        private CGPoint? _oldPt;

        protected override void OnElementChanged(ElementChangedEventArgs<GridSplitter> e)
        {
            base.OnElementChanged(e);

            _panGestureRecognizer = new UIPanGestureRecognizer(OnPanGesture) { MaximumNumberOfTouches = 1, MinimumNumberOfTouches = 1 };

            if (e.NewElement == null)
            {
                if (_panGestureRecognizer != null)
                {
                    RemoveGestureRecognizer(_panGestureRecognizer);
                }
            }

            if (e.OldElement == null)
            {
                AddGestureRecognizer(_panGestureRecognizer);
                UserInteractionEnabled = true;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            _oldPt = (touches.First() as UITouch).LocationInView(UIApplication.SharedApplication.KeyWindow);
        }

        void OnPanGesture()
        {
            Point ptOffset = Point.Zero;
            CGPoint pt = _panGestureRecognizer.LocationInView(UIApplication.SharedApplication.KeyWindow);

            if (_oldPt != null)
            {
                ptOffset = new Point(pt.X - _oldPt.Value.X, pt.Y - _oldPt.Value.Y);
            }

            _oldPt = pt;

            Element.UpdateGrid(ptOffset.X, ptOffset.Y);
        }
    }
}