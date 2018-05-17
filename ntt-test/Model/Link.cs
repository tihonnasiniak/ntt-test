using System;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntt_test.Model
{
    public enum Direction
    {
        In,
        Out
    }

    public class Link
    {
        //Для EntityFramework
        public int id { get; set; }
        
        public DateTime date { get; set; }
        public string objectA { get; set; }
        public string typeA { get; set; }
        public string objectB { get; set; }
        public string typeB { get; set; }
        public Direction direction { get; set; }
        [NotMapped]
        public Color color { get; set; }
        //Парс из строки для Entity Framework
        public string colorString
        {
            get { return color.ToString(); }
            set { color = (Color)ColorConverter.ConvertFromString(value); }
        }
        public int intensity { get; set; }
        public double latitudeA { get; set; }
        public double longitudeA { get; set; }
        public double latitudeB { get; set; }
        public double longitudeB { get; set; }
    }
}
