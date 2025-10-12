using System.Text.Json.Serialization;

namespace TrainMonitor.application.LoadTrains;

public class Root
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("data")]
    public List<DataItem>? Data { get; set; }

    [JsonPropertyName("timeStamp")]
    public string? TimeStamp { get; set; } // era uneori șir, nu dată ISO
}

public class DataItem
{
    [JsonPropertyName("stopCoordArray")]
    public List<List<string>>? StopCoordArray { get; set; }

    [JsonPropertyName("returnValue")]
    public ReturnValue? ReturnValue { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class ReturnValue
{
    [JsonPropertyName("pixelsTraveled")]
    public double? PixelsTraveled { get; set; }

    [JsonPropertyName("i")]
    public int? I { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("train")]
    public string? Train { get; set; }

    [JsonPropertyName("stopObjArray")]
    public List<StopObj>? StopObjArray { get; set; }

    [JsonPropertyName("animatedCoord")]
    public List<double>? AnimatedCoord { get; set; }

    [JsonPropertyName("nextTime")]
    public int? NextTime { get; set; }

    [JsonPropertyName("stopped")]
    public bool? Stopped { get; set; }

    [JsonPropertyName("currentStop")]
    public List<double>? CurrentStop { get; set; }

    [JsonPropertyName("position")]
    public List<double>? Position { get; set; }

    [JsonPropertyName("waitingTime")]
    public int? WaitingTime { get; set; }

    [JsonPropertyName("arrivingTime")]
    public int? ArrivingTime { get; set; }

    [JsonPropertyName("nextStopObj")]
    public StopObj? NextStopObj { get; set; }

    [JsonPropertyName("currentStopIndex")]
    public int? CurrentStopIndex { get; set; }

    [JsonPropertyName("updaterTimeStamp")]
    public string? UpdaterTimeStamp { get; set; }

    [JsonPropertyName("departureTime")]
    public string? DepartureTime { get; set; }

    [JsonPropertyName("arrivalTime")]
    public string? ArrivalTime { get; set; }

    [JsonPropertyName("finished")]
    public bool? Finished { get; set; }

    [JsonPropertyName("currentI")]
    public int? CurrentI { get; set; }

    [JsonPropertyName("isGpsActive")]
    public bool? IsGpsActive { get; set; }

    [JsonPropertyName("customArrivingTime")]
    public string? CustomArrivingTime { get; set; }
}

public class StopObj
{
    [JsonPropertyName("coords")]
    public List<double>? Coords { get; set; }

    [JsonPropertyName("workingTime")]
    public List<WorkingTime>? WorkingTime { get; set; }

    [JsonPropertyName("_id")]
    public string? InternalId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    // Corecție principală: "departure" e uneori șir gol, nu dată
    [JsonPropertyName("departure")]
    public string? Departure { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("gps_id")]
    public string? GpsId { get; set; }

    [JsonPropertyName("routes_id")]
    public string? RoutesId { get; set; }

    [JsonPropertyName("waitingRoom")]
    public string? WaitingRoom { get; set; }

    [JsonPropertyName("wc")]
    public string? Wc { get; set; }

    [JsonPropertyName("coffeMachine")]
    public string? CoffeMachine { get; set; }

    [JsonPropertyName("stationNotes")]
    public string? StationNotes { get; set; }

    [JsonPropertyName("adress")]
    public string? Adress { get; set; }

    [JsonPropertyName("pvID")]
    public string? PvId { get; set; }

    [JsonPropertyName("i")]
    public int? I { get; set; }
}

public class WorkingTime
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("periods")]
    public List<string>? Periods { get; set; }
}