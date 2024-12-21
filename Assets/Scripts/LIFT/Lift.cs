using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Lift : MonoBehaviour
{
    public GameObject _leftDoor, _rightDoor;
    
    
    public float[] _floor = new float[3];
    public float time = 1;
    public float timeMul = 2;
    public float _rightDoorScaleOpen = 1.84f;
    public float _leftDoorScaleOpen = 1.84f;

    public bool isMoving = false;
    public Collider col;
    public MainPlayer player;

    public void SetHeight(int floor)
    {
        if (_floor[floor] == transform.localPosition.y)
        {
            Debug.Log("Same floor");
            LeanTween.scaleZ(_rightDoor, .5f, time).setEaseInOutExpo();
            LeanTween.scaleZ(_leftDoor, .5f, time).setEaseInOutExpo();
        }

        if (!isMoving && _floor[floor] != transform.localPosition.y)
        {
            LeanTween.scaleZ(_rightDoor, _rightDoorScaleOpen, time).setEaseInOutExpo();
            LeanTween.scaleZ(_leftDoor, _leftDoorScaleOpen, time).setEaseInOutExpo().setOnComplete(() => {                 
                LeanTween.moveLocalY(gameObject, _floor[floor], time* timeMul).setEaseInOutExpo().setOnComplete(() => {                    
                    LeanTween.scaleZ(_leftDoor, .5f, time).setEaseInOutExpo().setOnComplete(() =>
                    {
                        player.controller.enabled = true;
                        player.gameObject.layer = 8;
                        isMoving = false;
                        if(col)col.isTrigger = true;
                    });
                    LeanTween.scaleZ(_rightDoor, .5f, time).setEaseInOutExpo();
                }); 
            });                 
            isMoving = true;
        }
    }

    private void Start()
    {
        //SetHeight(2);
    }
}
