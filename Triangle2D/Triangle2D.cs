using Contract;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Triangle2D
{
    public class Triangle2D : CShape, IShape
    {
        public DoubleCollection StrokeDash { get; set; }
        public SolidColorBrush Brush { get; set; }
        public string Name => "Triangle";
        public string Icon => "Images/triangle.png";
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

            double width = Math.Abs(_rightBottom.X - _leftTop.X);
            double height = Math.Abs(_rightBottom.Y - _leftTop.Y);
            if (width < height)
            {
                if (_rightBottom.Y < _leftTop.Y)
                    _rightBottom.Y = _leftTop.Y - width;
                else
                    _rightBottom.Y = _leftTop.Y + width;
            }
            else
            {
                if (_rightBottom.X < _leftTop.X)
                    _rightBottom.X = _leftTop.X - height;
                else
                    _rightBottom.X = _leftTop.X + height;
            }
        }

        public UIElement Draw(SolidColorBrush brush, int thickness, DoubleCollection dash)
        {
            PointCollection points = new PointCollection();
            points.Add(new Point(_leftTop.X + (_rightBottom.X - _leftTop.X) / 2, _leftTop.Y));
            points.Add(new Point(_rightBottom.X, _rightBottom.Y));
            points.Add(new Point(_leftTop.X, _rightBottom.Y));

            var triangle = new Polygon()
            {
                Points = points,
                StrokeThickness = thickness,
                Stroke = brush,
                StrokeDashArray = dash,
            };

            return triangle;
        }

        public IShape Clone()
        {
            return new Triangle2D();
        }

        public override CShape deepCopy()
        {
            Triangle2D temp = new Triangle2D();

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
