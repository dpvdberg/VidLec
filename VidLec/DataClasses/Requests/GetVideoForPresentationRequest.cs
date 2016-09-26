using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec.DataClasses.Requests
{
    class GetVideoForPresentationRequest
    {
        public GetPlayerOptionsRequest getPlayerOptionsRequest { get; set; }
    }

    public class GetPlayerOptionsRequest
    {
        public string ResourceId { get; set; }
        public string QueryString { get; set; }
        public bool UseScreenReader { get; set; }
    }
}
