namespace MKE.Models
{
    public class Node
    {
        // Private fields
        private double _x;
        private double _y;

        // Public properties
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        // Constructor
        public Node(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}
