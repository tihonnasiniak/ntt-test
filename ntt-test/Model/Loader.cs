using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Media;
using System.Data.Entity;

namespace ntt_test.Model
{
    public class Loader
    {
        public event DoubleEventArgs ProgressChanged;
        public delegate void DoubleEventArgs(double percent);
        public event LinkListEventArgs FileLoaded;
        public delegate void LinkListEventArgs(List<Link> links);
        public event ErrorEventArgs Error;
        public delegate void ErrorEventArgs(string message);

        protected long _fileLength;
        protected StreamReader _file;
        protected bool _canceled = true;
        
        protected static readonly string[] expectedHeaders = 
        {
            "date", "object a", "type a", "object b", "type b",
            "direction", "color", "intensity", "latitudea",
            "longitudea", "latitudeb", "longitudeb"
        };

        public void LoadFile(string fileName)
        {
            try
            {
                _canceled = false;
                _file = File.OpenText(fileName);
                _fileLength = new FileInfo(fileName).Length;
                long fileLength = _file.BaseStream.Length;

                var doubleFormat = new NumberFormatInfo { CurrencyDecimalSeparator = "." };

                var listToReturn = new List<Link>();
                string headers;

                if (!_file.EndOfStream)
                {
                    headers = _file.ReadLine();
                    var headersOrder = ParseHeaders(headers);
                    while (!_file.EndOfStream && !_canceled)
                    {
                            string line = _file.ReadLine();
                            var splitted = line.Split(';');
                            listToReturn.Add(new Link
                            {
                                //Использовать абсолютные индексы не очень хорошо, лучше объявить константы, 
                                //но, т.к. набор заголовков фиксирован, пока можно оставить так
                                date = DateTime.Parse(splitted[headersOrder[0]]),
                                objectA = splitted[headersOrder[1]],
                                typeA = splitted[headersOrder[2]],
                                objectB = splitted[headersOrder[3]],
                                typeB = splitted[headersOrder[4]],
                                direction = (Direction)Enum.Parse(typeof(Direction), splitted[headersOrder[5]], true),
                                color = (Color)ColorConverter.ConvertFromString(splitted[headersOrder[6]]),
                                intensity = int.Parse(splitted[headersOrder[7]]),
                                latitudeA = double.Parse(splitted[headersOrder[8]], doubleFormat),
                                longitudeA = double.Parse(splitted[headersOrder[9]], doubleFormat),
                                latitudeB = double.Parse(splitted[headersOrder[10]], doubleFormat),
                                longitudeB = double.Parse(splitted[headersOrder[11]], doubleFormat)
                            });
                            ProgressChanged?.Invoke((double)_file.BaseStream.Position / fileLength);
                    }
                }
                else
                    throw new ArgumentException("Файл пуст");
                if (!_canceled)
                    FileLoaded?.Invoke(listToReturn);
            }
            catch(Exception e)
            {
                Error?.Invoke(e.Message);
            }
        }

        public void LoadFromDatabase()
        {
            try
            {
                var db = new ApplicationContext();
                db.Links.Load();
                FileLoaded?.Invoke(db.Links.Local.ToList());
            }
            catch (Exception e)
            {
                Error?.Invoke(e.Message);
            }
        }

        public int[] ParseHeaders(string headers)
        {
            //Приведение к нижнему регистру перед разделением для регистронезависимости
            var splitted = headers.ToLowerInvariant().Split(';');
            if (string.IsNullOrEmpty(splitted.Last()))
                splitted = splitted.Take(splitted.Length - 1).ToArray();
            if (splitted.Length != expectedHeaders.Length)
                throw new ArgumentException("Неверный формат заголовков");
            var indexesToReturn = new List<int>();
            foreach (var expectedHeader in expectedHeaders)
            {
                int index = Array.IndexOf(splitted, expectedHeader);
                if (index == -1)
                    throw new ArgumentException($"Среди заголовков нет {expectedHeader}");
                indexesToReturn.Add(index);
            }
            return indexesToReturn.ToArray();
        }

        public void Cancel()
        {
            _canceled = true;
        }
    }
}
