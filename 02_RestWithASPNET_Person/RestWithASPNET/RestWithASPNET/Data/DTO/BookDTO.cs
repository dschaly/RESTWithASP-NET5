using RestWithASPNET.HyperMedia;
using RestWithASPNET.HyperMedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithASPNET.Data.DTO
{
    public class BookDTO : ISupportHyperMedia
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
