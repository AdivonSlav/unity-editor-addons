using System;
using System.IO;

public static class DLLResolver
{
    static DLLResolver()
    {
        var CurrentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        var DLLPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins";
        if(CurrentPath.Contains(DLLPath) == false)
        {
            Environment.SetEnvironmentVariable("PATH", CurrentPath + Path.PathSeparator + DLLPath, EnvironmentVariableTarget.Process);
        }
    }
}