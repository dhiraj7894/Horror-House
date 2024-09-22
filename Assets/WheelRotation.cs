using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    void Update()
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
