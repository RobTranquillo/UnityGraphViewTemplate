using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;

    protected override void OnStart()
    {
        Debug.Log($"<color=green>OnStart</color> " + message);
    }

    protected override void OnStop()
    {
        Debug.Log($"<color=lightgreen>OnStop</color> " + message);
    }

    protected override State OnUpdate()
    {
        Debug.Log($"<color=yellow>OnUpdate</color> " + message);

        return State.Success;
    }
}
