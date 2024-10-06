using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DoorScript : InteractBase, Interacter
{
    private MainPlayer _player;

    public Transform interactableUI;

    public bool isPlayerNear = false;

    public float time = .1f;
    public float doorOpeningTime = .5f;
    public float doorOpeningAngle = 45;

    [SerializeField]private float initialDoorAngle;

    [SerializeField] float currentEA;
    private void Start()
    {
        // Interact();
        _player = GameManager.Instance._PlayerObject;
        EventManager.Instance.PressFButton += PressFButton;
        _UIText = "Open Door";
        //initialDoorAngle = transform.eulerAngles.y;
    }

    private void PressFButton()
    {
        if (isPlayerNear)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (_UIText.Contains("Open Door"))
            _UIText = "Close Door";
        else _UIText = "Open Door";

        if (_UIText.Contains("Close Door"))
        {
            LeanTween.rotateY(gameObject, doorOpeningAngle, doorOpeningTime).setEaseInCirc();
            Debug.Log("OpenDoor");
        }
        else if(_UIText.Contains("Open Door"))
        {
            LeanTween.rotateY(gameObject, initialDoorAngle, doorOpeningTime).setEaseInCirc();
            Debug.Log("OpenDoor");
        }
    }

    private void Update()
    {
        currentEA = transform.localEulerAngles.y;
        if (!_Heighlight)
            return;
        if (_player.playerController.interactBase == null)
            _Heighlight.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
       /* if (other.CompareTag(AnimHash.Player))
        {
            isPlayerNear = true;
            UIManager.Instance._interactionUI._UIText.text = _UIText;
            LeanTween.value(0, 1, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag(AnimHash.Player))
        {
            isPlayerNear = false;
            UIManager.Instance._interactionUI._UIText.text = "";
            LeanTween.value(1, 0, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
        }*/
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }
}
