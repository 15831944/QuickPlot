

namespace QuickPrint.Model
{
    struct EPoint2D
    {
        public double X;
        public double Y;
        public EPoint2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(EPoint2D p1, EPoint2D p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(EPoint2D p1, EPoint2D p2)
        {
            return p1.X != p2.X && p1.Y != p2.Y;
        }
    }
}
