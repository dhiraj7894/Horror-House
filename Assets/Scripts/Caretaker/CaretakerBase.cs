using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CaretakerBase 
{
    public CaretakerManager manager;
    public CaretakerBase(CaretakerManager mainManager)
    {
        manager = mainManager;
    }
    public virtual void EnterState() { }
    public virtual void LogicUpdateState() { }
    public virtual void ExitState() { }
}
