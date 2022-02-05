using System.Collections.Generic;

namespace RestWithASPNET.HyperMedia.Abstract
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
