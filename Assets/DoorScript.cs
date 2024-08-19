using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractBase, Interacter
{
    public Transform interactableUI;
    public bool isPlayerNear = false;

    public float time = .1f;
    public float doorOpeningTime = .5f;

    private void Start()
    {
        // Interact();
        EventManager.Instance.PressFButton += PressFButton;
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

        if (transform.eulerAngles.y == 90)
        {
            LeanTween.rotateY(gameObject, 150, doorOpeningTime).setEaseInCirc();
            Debug.Log("OpenDoor");
        }
        else if(transform.eulerAngles.y == 150)
        {
            LeanTween.rotateY(gameObject, 90, doorOpeningTime).setEaseInCirc();
            Debug.Log("OpenDoor");
        }
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
