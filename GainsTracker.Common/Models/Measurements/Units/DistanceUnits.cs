﻿using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Measurements.Units;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DistanceUnits
{
    Kilometers,
    Meters,
}
