using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Mvc;
using Rutedata.Models;

namespace Rutedata.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var query = @"query($id:String!, $time:DateTime) { stopPlace (id: $id) { id, name, estimatedCalls (startTime:$time, timeRange: 72100, numberOfDepartures: 10) {realtime, aimedArrivalTime
            expectedArrivalTime, date, destinationDisplay {frontText} } } }";

            var request = new GraphQLRequest()
            {
                Query = query,
                Variables = new { id = "NSR:StopPlace:4000", time = "2019-10-20T14:30:00+0200" }

            };

            var graphQLClient = new GraphQLClient("https://api.entur.io/journey-planner/v2/graphql");

            graphQLClient.DefaultRequestHeaders.Add("ET-Client-Name", "test");

            var graphQLResponse = await graphQLClient.PostAsync(request);

            var temp = graphQLResponse.GetDataFieldAs<StopPlace>("stopPlace");

            for (int i = 0; i < temp.EstimatedCalls.Count; i++)
            {
                DateTime aimedTime = DateTime.Parse(temp.EstimatedCalls[i].AimedArrivalTime);

                DateTime expectedTime = DateTime.Parse(temp.EstimatedCalls[i].ExpectedArrivalTime);

                if (expectedTime > aimedTime)
                {
                    temp.EstimatedCalls[i].Late = true;
                }
                else
                {
                    temp.EstimatedCalls[i].Late = false;
                }
            }

            return View(temp);
        }

    }
}
