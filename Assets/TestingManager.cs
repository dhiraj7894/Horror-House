using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    public List<Transform> gameObjects;
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Transform obj in gameObjects)
            {
                obj.gameObject.SetActive(true);
                obj.parent = null;
                obj.position = player.position + new Vector3(0,1,0);
            }
        }
    }
}
