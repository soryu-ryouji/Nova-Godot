using System;
using Godot;
namespace Nova;

public class Debug
{
    public static void Log(string log)
    {
        GD.Print(log);
    }

    public static void LogErr(string log)
    {
        GD.PrintErr(log);
    }

    public static void LogErr(Exception e)
    {
        GD.Print(e.Message);
    }

    public static void LogWarning(string log)
    {
        GD.Print(log);
    }
}