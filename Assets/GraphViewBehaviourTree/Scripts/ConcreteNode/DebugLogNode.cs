using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public bool debug = false;
    public string message;

    protected override void OnStart()
    {
        Debug.Log($"<color=green>{message}</color>");
    }

    protected override void OnStop()
    {
        if (debug)
            Debug.Log($"<color=lightgreen>OnStop</color> {message}");
    }

    protected override State OnUpdate()
    {
        if (debug)
            Debug.Log($"<color=yellow>OnUpdate</color> {message}");

        return State.Success;
    }
}
