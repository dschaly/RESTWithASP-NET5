using System.Text;

namespace RestWithASPNET.HyperMedia
{
    public class HyperMediaLink
    {
        private string href;
        public string Href {
            get 
            {
                object _lock = new();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2f", "/").ToString();
                }
            }
            set 
            { 
                href = value; 
            } 
        }
        public string Rel { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
