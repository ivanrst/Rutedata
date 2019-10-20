using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rutedata.Models
{
    public class EstimatedCall
    {
        public string RealTime { get; set; }

        public string AimedArrivalTime { get; set; }

        public string ExpectedArrivalTime { get; set; }

        public Boolean Late { get; set; }

        public string Date { get; set; }

        public DestinationDisplay DestinationDisplay { get; set; }
    }
}
