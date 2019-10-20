using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rutedata.Models
{
    public class StopPlace
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public List<EstimatedCall> EstimatedCalls { get; set; }

    }
}
