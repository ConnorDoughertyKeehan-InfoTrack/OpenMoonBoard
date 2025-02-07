using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;

public class MoonBoardConfiguration
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string LowGrade { get; set; }
    public string HighGrade { get; set; }
}

public class SetterResponse
{
    public string Id { get; set; }
    public string Nickname { get; set; }
}

public class Move
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd { get; set; }
}

public class Holdsetup
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Setby { get; set; }
    public bool IsLocked { get; set; }
    public bool IsMini { get; set; }
    public bool Active { get; set; }
    public object Holdsets { get; set; }
    public object MoonBoardConfigurations { get; set; }
    public int HoldLayoutId { get; set; }
    public bool AllowClimbMethods { get; set; }
}

public class Problem
{
    public string Method { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }
    public string UserGrade { get; set; }
    public MoonBoardConfiguration MoonBoardConfiguration { get; set; }
    public int MoonBoardConfigurationId { get; set; }
    public SetterResponse Setter { get; set; }
    public bool FirstAscender { get; set; }
    public int Rating { get; set; }
    public int UserRating { get; set; }
    public int Repeats { get; set; }
    public int Attempts { get; set; }
    public Holdsetup Holdsetup { get; set; }
    public bool IsBenchmark { get; set; }
    public bool IsMaster { get; set; }
    public bool IsAssessmentProblem { get; set; }
    public string ProblemType { get; set; }
    public List<Move> Moves { get; set; }
    public object Holdsets { get; set; }
    public bool Downgraded { get; set; }
    public int Id { get; set; }
    public int ApiId { get; set; }
}

public class ProblemData
{
    public int Id { get; set; }
    public Problem Problem { get; set; }
}