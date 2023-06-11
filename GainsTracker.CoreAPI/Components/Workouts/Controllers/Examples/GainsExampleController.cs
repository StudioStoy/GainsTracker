using System.Text.Json;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Workouts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Workouts.Controllers.Examples;

[ApiController]
[Route("catalog/measurement")]
public class GainsExampleController : ControllerBase
{
    [HttpGet]
    public IActionResult GetExampleMeasurementRequests()
    {
        Dictionary<string, JsonDocument> examples = new();

        examples.Add("strengthMeasurement", GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto()));
        examples.Add("runningEnduranceMeasurement", GenericJsonSerializer.SerializeObjectToJson(new RunningEnduranceMeasurementDto()));
        examples.Add("simpleRepMeasurement", GenericJsonSerializer.SerializeObjectToJson(new SimpleRepMeasurementDto()));
        examples.Add("SimpleEnduranceMeasurement", GenericJsonSerializer.SerializeObjectToJson(new SimpleEnduranceMeasurementDto()));

        return Ok(examples);
    }
}
