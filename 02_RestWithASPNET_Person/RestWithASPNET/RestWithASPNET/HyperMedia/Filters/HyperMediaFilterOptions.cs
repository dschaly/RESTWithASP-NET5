using RestWithASPNET.HyperMedia.Abstract;
using System.Collections.Generic;

namespace RestWithASPNET.HyperMedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();

    }
}
