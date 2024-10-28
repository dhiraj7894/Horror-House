using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CaretakerBase 
{
    public CaretakerManager manager;
    
    protected NavMeshAgent agent;
    protected Animator anim;
    public CaretakerBase(CaretakerManager mainManager)
    {
        manager = mainManager;
        agent = mainManager.agent;
        anim = mainManager.anim;
    }

    public virtual void EnterState() {     
    agent.speed = manager.speed;
    
    }
    public virtual void LogicUpdateState() { }
    public virtual void ExitState() { }

    public void chage(Transform target,float speed, bool isMove = false)
    {
        agent.SetDestination(target.position);
        if(agent.velocity.magnitude > 0)
        {
            anim.SetFloat(AnimHash.SPEED, 1);
        }
        else
        {
            anim.SetFloat(AnimHash.SPEED, 0);
        }
    }
}
