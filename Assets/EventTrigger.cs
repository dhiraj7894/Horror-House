using HorroHouse;
using HorroHouse.Player;
using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent TriggerEvent;
    private MainPlayer mainPlayer;

    public float time;
    public Vector3 rotationAngle = Vector3.zero;
    public LeanTweenType type;
    private void Start()
    {
        mainPlayer = GameManager.Instance._PlayerObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(AnimHash.Player))
        {
            TriggerEvent?.Invoke();
        }
    }

    public void LookTowardPlayer(Transform item)
    {
        //item.gameObject.SetActive(true);
        LeanTween.move(item.gameObject, rotationAngle, time).setEase(type).setOnComplete(() => {
            //item.gameObject.SetActive(false);
        });
    }

    public void LeanToPlayer(Transform item)
    {        
        LeanTween.rotate(item.gameObject, rotationAngle, time).setEase(type).setOnComplete(() => {
            
        }); 
    }


    public void CaretakerEncounter(float delay)
    {
        LeanTween.delayedCall(delay, () => {
            // Caretaker starts chasing you
            });
        GetComponent<Collider>().enabled = false;
    }



}
