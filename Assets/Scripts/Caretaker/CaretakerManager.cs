using HorroHouse;
using HorroHouse.Player;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CaretakerManager : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    public NavMeshAgent agent;
    private MainPlayer player;

    [Header("Raycast")]
    public LayerMask interactableLayer;
    public Transform raycastPosition;
    public float raycastDistance = 1f;
    private float _raycastDistance;

    [Header("Settings")]
    public float interactDelay = 2f;
    public float speed;
    public float idleWaitTime;

    [Header("Chasing Data")]
    public float distanceTillChase = 5;
    public float isPlayerHidden = 2.5f;
    private float currentDistanceFromPlayer;

    private bool isInteracting = false;
    private bool isChasing = false;
    private bool isChasingPlayer = false;
    private bool isMoving = false;
    private bool isIdle = false;
    private bool isAttacking = false;



    private void Start()
    {
        agent.speed = speed;
        _raycastDistance = raycastDistance;
        player = GameManager.Instance._PlayerObject;
    }

    private void Update()
    {
        UpdateAnimation();
        HandleMovementLoop();
        CheckDoorInFront();
        ChaseThePlayer();
    }

    public void MoveToCustomPosition(int positionNumber)
    {
        if (positionNumber == 1)
        {
            isChasingPlayer = true;
            speed = 1.5f;
        }
        else
        {
            speed = 2f;
        }
        SetAgentDestination(CaretakerWayPointBank.Instance.GetCustomWaypoint(positionNumber).position);
        
        agent.speed = speed;
        isChasing = true;
    }

    bool storyChase = false;
    public void ChaseThePlayer()
    {
        currentDistanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (isChasingPlayer && currentDistanceFromPlayer > distanceTillChase)
        {
            if (!storyChase)
            {
                LeanTween.delayedCall(10,()=> GameManager.Instance.SetCurrentTask(3));
                storyChase = true;
            }
            isChasingPlayer = false;
            isChasing = false;
        }
        if(isChasingPlayer && currentDistanceFromPlayer > isPlayerHidden && player.isHideden)
        {
            if (!storyChase)
            {
                LeanTween.delayedCall(10, () => GameManager.Instance.SetCurrentTask(3));
                CaretakerWayPointBank.Instance.AddWaypoints(0);
                storyChase = true;
            }
            isChasingPlayer = false;
            isChasing = false;
        }
        if (!isChasingPlayer && currentDistanceFromPlayer < (distanceTillChase/2.5f) && !player.isHideden )
        {
            isChasingPlayer = true;
            isChasing = true;
        }
        if(isChasingPlayer && currentDistanceFromPlayer <= 1)
        {
            if (!isAttacking)
            {
                anim.SetTrigger("attack");
                player.isDead = true;
                isAttacking = true;
                //Game Over
                //Play game over Cutscene 
            }
        }

        if (isChasingPlayer)SetAgentDestination(CaretakerWayPointBank.Instance.GetCustomWaypoint(1).position);

    }

    private void HandleMovementLoop()
    {
        if (isChasing) return;

        isMoving = agent.velocity.magnitude > 0f;

        if (isMoving)
        {
            ExitIdleState();
        }
        else
        {            
            EnterIdleState();

            if (idleWaitTime > 0)
            {
                idleWaitTime -= Time.deltaTime;
                if (idleWaitTime <= 0)
                {
                    SetAgentDestination(CaretakerWayPointBank.Instance.GetWaypoint().position);
                }
            }
            
        }
    }

    private void EnterIdleState()
    {
        if (!isIdle)
        {
            Debug.Log("Entering idle state.");
            anim.SetTrigger("isIdle");
            isIdle = true;
            idleWaitTime = anim.runtimeAnimatorController.animationClips.First(clip => clip.name == "Zombie Idle").length;
        }
    }

    private void ExitIdleState()
    {
        if (isIdle)
        {
            Debug.Log("Exiting idle state.");
            anim.ResetTrigger("isIdle");
            isIdle = false;
        }
    }

    private void CheckDoorInFront()
    {
        if (isInteracting) return;
        raycastDistance = isMoving ? _raycastDistance : 0f;
        if (Physics.Raycast(raycastPosition.position, raycastPosition.forward, out RaycastHit hit, raycastDistance, interactableLayer))
        {
            if (hit.transform.TryGetComponent(out Interacter interacter) && hit.transform.TryGetComponent(out InteractBase interactBase))
            {
                if (interactBase._CanCTInteract == CanCTInteract.no)
                {
                    Debug.Log($"agent.isStopped : {agent.isStopped}");                    
                    agent.isStopped = true;
                    return;
                }

                StartCoroutine(InteractWithDelay(interacter));
            }
        }
    }

    private IEnumerator InteractWithDelay(Interacter interacter)
    {
        isInteracting = true;
        interacter.Interact();
        yield return new WaitForSeconds(interactDelay);
        isInteracting = false;
    }

    private void UpdateAnimation()
    {
        float currentSpeed = agent.velocity.magnitude;
        anim.SetFloat(AnimHash.SPEED, currentSpeed > 0f ? speed : 0f);
    }

    private void SetAgentDestination(Vector3 destination)
    {
        agent.isStopped = false; // Ensure the agent starts moving
        agent.SetDestination(destination);
    }
}
