using Contract;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diamond2D
{
    public class Diamond2D : CShape, IShape
    {
        public DoubleCollection StrokeDash { get; set; }
        public SolidColorBrush Brush { get; set; }
        public string Name => "Diamond";
        public string Icon => "Images/diamond.png";
        public int Thickness { get; set; }

        public void HandleStart(double x, double y)
        {
            _leftTop.X = x;
            _leftTop.Y = y;
        }

        public void HandleEnd(double x, double y)
        {
            _rightBottom.X = x;
            _rightBottom.Y = y;
        }

        public UIElement Draw(SolidColorBrush brush, int thickness, DoubleCollection dash)
        {
            PointCollection points = new PointCollection();
            points.Add(new Point(_leftTop.X + (_rightBottom.X - _leftTop.X) / 2, _leftTop.Y));
            points.Add(new Point(_rightBottom.X, _leftTop.Y + (_rightBottom.Y - _leftTop.Y) / 2));
            points.Add(new Point(_leftTop.X + (_rightBottom.X - _leftTop.X) / 2, _rightBottom.Y));
            points.Add(new Point(_leftTop.X, _leftTop.Y + (_rightBottom.Y - _leftTop.Y) / 2));

            var diamond = new Polygon()
            {
                Points = points,
                StrokeThickness = thickness,
                Stroke = brush,
                StrokeDashArray = dash,
            };

            return diamond;
        }

        public IShape Clone()
        {
            return new Diamond2D();
        }

        public override CShape deepCopy()
        {
            Diamond2D temp = new Diamond2D();

            temp.LeftTop = this._leftTop.deepCopy();
            temp.RightBottom = this._rightBottom.deepCopy();
            temp._rotateAngle = this._rotateAngle;
            temp.Thickness = this.Thickness;

            if (this.Brush != null)
                temp.Brush = this.Brush.Clone();

            if (this.StrokeDash != null)
                temp.StrokeDash = this.StrokeDash.Clone();

            return temp;
        }
    }
}
