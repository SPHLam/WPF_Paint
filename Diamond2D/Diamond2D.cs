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

            // Tính toán điểm trung tâm
            double centerX = (_leftTop.X + _rightBottom.X) / 2;
            double centerY = (_leftTop.Y + _rightBottom.Y) / 2;

            // Di chuyển lại điểm trung tâm
            _leftTop.X = centerX - (_rightBottom.X - centerX);
            _leftTop.Y = centerY;
            _rightBottom.X = centerX;
            _rightBottom.Y = centerY + (_rightBottom.Y - centerY);

            // Đảm bảo rằng cả hai điểm đầu vào nằm ở trên hoặc dưới
            if (_leftTop.Y > _rightBottom.Y)
            {
                double temp = _leftTop.Y;
                _leftTop.Y = _rightBottom.Y;
                _rightBottom.Y = temp;
            }
        }

        public UIElement Draw(SolidColorBrush brush, int thickness, DoubleCollection dash)
        {
            double width = Math.Abs(_rightBottom.X - _leftTop.X);
            double height = Math.Abs(_rightBottom.Y - _leftTop.Y);

            var diamond = new Polygon();
            diamond.Stroke = brush;
            diamond.StrokeThickness = thickness;
            diamond.StrokeDashArray = dash;

            diamond.Points.Add(new Point(_leftTop.X + (width / 2), _leftTop.Y));
            diamond.Points.Add(new Point(_rightBottom.X, _leftTop.Y + (height / 2)));
            diamond.Points.Add(new Point(_leftTop.X + (width / 2), _rightBottom.Y));
            diamond.Points.Add(new Point(_leftTop.X, _leftTop.Y + (height / 2)));

            return diamond;
        }

        public IShape Clone()
        {
            return new Diamond2D();
        }

        override public CShape deepCopy()
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
