using System;
using System.Windows.Media;

namespace ntt_test.Model
{
    public enum Direction
    {
        In,
        Out
    }

    public class Link
    {
        public DateTime date { get ; set; }
        public string objectA { get; set; }
        public string typeA { get; set; }
        public string objectB { get; set; }
        public string typeB { get; set; }
        public Direction direction { get; set; }
        public Color color { get; set; }
        public int intensity { get; set; }
        public double latitudeA { get; set; }
        public double longitudeA { get; set; }
        public double latitudeB { get; set; }
        public double longitudeB { get; set; }
    }
}
