using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent TriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(AnimHash.Player))
        {
            TriggerEvent?.Invoke();
        }
    }
}
