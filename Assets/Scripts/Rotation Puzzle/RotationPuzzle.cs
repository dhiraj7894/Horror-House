using HorroHouse.Player;
using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationPuzzle : InteractBase, Interacter
{
    public List<Transform> lockerKeys;
    public List<SpriteRenderer> passWordNumbers;
    public List<Collider> cols;

    public GameObject lockerCamera;


    private MainPlayer player;
    private Transform currentKey;
    private TextMeshPro curretNumberText;
    private int selectedIndex;
    private int previusIndex;

    private float currentNumberOnKey;
    private int[] password = new int[5];

    private bool isInteracted = false;
    private bool isOpend = false;
    private void Start()
    {
        player = GameManager.Instance._PlayerObject;
        //OnSelectLocker();
        RandomizeTheNumber();
    }

    public void Interact()
    {
              
        OnSelectLocker();
        player.enabled = false;
        player.playerController.enabled = false;
        lockerCamera.SetActive(true);
        player.playerCamera.SetActive(false);
        isInteracted = true;
        if (!isOpend)
        {
            foreach (Collider col in cols)
            {
                col.enabled = false;
            }
        }        
    }
    public void Drop()
    {

    }

    public void UpdateOnInteract()
    {
        // Camera switch to Locker facing
        selectedIndex = (lockerKeys.Count - lockerKeys.Count);
    }
    private void Update()
    {
        if (isInteracted)
        {
            PasswordUpdate();
            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
        }

        }
        public void PasswordUpdate()
    {
        SelectWheel();
        RotateWheel();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MatchThePassword();
            player.playerCamera.SetActive(true);
            lockerCamera.SetActive(false);
            player.enabled = true;
            player.playerController.enabled = true;
            isInteracted = false;
            if (!isOpend)
            {
                foreach (Collider col in cols)
                {
                    col.enabled = true;
                }
            }
        }
    }

    public void SelectWheel()
    {
        // index select key cylinder on left right click (change index value)        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedIndex >= lockerKeys.Count - 1)
            {
                previusIndex = selectedIndex;
                selectedIndex = 0;
                currentKey = lockerKeys[selectedIndex];
                currentKey.GetComponent<Outline>().enabled = true;
                lockerKeys[previusIndex].GetComponent<Outline>().enabled = false;
            }
            else
            {
                previusIndex = selectedIndex;
                selectedIndex++;
                currentKey = lockerKeys[selectedIndex];
                currentKey.GetComponent<Outline>().enabled = true;
                lockerKeys[previusIndex].GetComponent<Outline>().enabled = false;
            }
            currentNumberOnKey = currentKey.eulerAngles.z;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedIndex <= 0)
            {

                previusIndex = selectedIndex;
                selectedIndex = lockerKeys.Count - 1;
                currentKey = lockerKeys[selectedIndex];
                currentKey.GetComponent<Outline>().enabled = true;
                lockerKeys[previusIndex].GetComponent<Outline>().enabled = false;
            }
            else
            {
                previusIndex = selectedIndex;
                selectedIndex--;
                currentKey = lockerKeys[selectedIndex];
                currentKey.GetComponent<Outline>().enabled = true;
                lockerKeys[previusIndex].GetComponent<Outline>().enabled = false;
            }
            currentNumberOnKey = currentKey.eulerAngles.z;
        }
        
    }

    public void RotateWheel()
    {
        // rotate selected cylender on up down key        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentNumberOnKey = currentNumberOnKey + 90;
            currentKey.eulerAngles = new Vector3(0, currentNumberOnKey, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentNumberOnKey = currentNumberOnKey - 90;
            currentKey.eulerAngles = new Vector3(0, currentNumberOnKey, 0);

        }
    }

    void OnSelectLocker()
    {
        currentKey = lockerKeys[selectedIndex];
        currentKey.GetComponent<Outline>().enabled = true;
    }

    void RandomizeTheNumber()
    {
        foreach (Transform key in lockerKeys)
        {
            // Generate a random multiple of 20 between -180 and 180
            int randomRotation = Random.Range(-9, 10) * 90; // -180 to 180 in multiples of 20

            // Apply the rotation to the object (assuming rotation along the Y axis here)
            key.eulerAngles = new Vector3(0, randomRotation, 0);
        }

        foreach (SpriteRenderer pass in passWordNumbers)
        {
            // Generate a random multiple of 20 between -180 and 180
            int randomRotation = Random.Range(-9, 10) * 90; // -180 to 180 in multiples of 20

            // Apply the rotation to the object (assuming rotation along the Y axis here)
            pass.transform.eulerAngles = new Vector3(0, 0, randomRotation);
        }
    }

    bool MatchThePassword()
    {
        // Check if all the number matches or not accordingly
        for (int i = 0; i < lockerKeys.Count; i++)
        {
            if (lockerKeys[i].eulerAngles.y != passWordNumbers[i].transform.eulerAngles.z)
            {
                Debug.Log("<color=red><b>Password Not Matched !! </b></color>");
                return false;
            }
        }
        Debug.Log("<color=green><b>Password Matched !! </b></color>");
        DisableCollider();
        return true;
    }

    void DisableCollider()
    {
        isOpend = true;
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
    }
}
