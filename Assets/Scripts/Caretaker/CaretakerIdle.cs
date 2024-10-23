using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerIdle : CaretakerBase
{
    public CaretakerIdle(CaretakerManager mainManager) : base(mainManager)
    {
        manager = mainManager;
    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void LogicUpdateState()
    {
        base.LogicUpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
