using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HorroHouse.Player
{
    public class CollisionDetector : MonoBehaviour
    {
        public UnityEvent onCollisionWithAnomaly;
        public UnityEvent onCollisionWithCaretaker;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(AnimHash.FlyingGhost))
            {
                onCollisionWithAnomaly?.Invoke();
            }
            if (other.CompareTag(AnimHash.Caretaker))
            {
                onCollisionWithCaretaker?.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(AnimHash.FlyingGhost))
            {
                onCollisionWithAnomaly?.Invoke();
            }
            if (collision.gameObject.CompareTag(AnimHash.Caretaker))
            {
                onCollisionWithCaretaker?.Invoke();
            }
        }

    }
}
