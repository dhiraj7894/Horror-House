using HorroHouse.Player;
using UnityEngine;
using UnityEngine.AI;

public class CaretakerManager : MonoBehaviour
{

    public Animator anim;
    public NavMeshAgent agent;
    public Transform targetPosition;

    [Space(5)]
    public float speed;

    public bool isChase = false;
    private void Start()
    {        
    }

    private void Update()
    {
        InputPosition();
        CharacterAnimation();
    }

    public void InputPosition()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            agent.SetDestination(CaretakerWayPointBank.Instance.GetWaypoint().position);   
        }
    }

    public void CharacterAnimation()
    {
        if(agent.velocity.magnitude!= 0f){
            anim.SetFloat(AnimHash.SPEED, 1);
        }
        else
        {
            anim.SetFloat(AnimHash.SPEED, 0);
        }
    }

    private void OnAnimatorMove()
    {
        if (anim.GetFloat(AnimHash.SPEED) != 0)
        {
            agent.speed = (anim.deltaPosition / Time.deltaTime).magnitude / speed;
        }
    }
}
