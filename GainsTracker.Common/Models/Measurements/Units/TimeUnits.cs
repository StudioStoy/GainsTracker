﻿#region

using System.Text.Json.Serialization;

#endregion

namespace GainsTracker.Common.Models.Measurements.Units;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TimeUnits
{
    Hours,
    Minutes,
    Seconds,
}
