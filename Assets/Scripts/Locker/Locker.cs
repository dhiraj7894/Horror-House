using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.InputSystem;
public class Locker : InteractBase, Interacter
{
    public List<Transform> lockerKeys;    
    public List<TextMeshPro> passWordNumbers;
    public List<Collider> cols;

    public GameObject lockerCamera;


    private MainPlayer player;
    private Transform currentKey;
    private TextMeshPro curretNumberText;
    private int selectedIndex;
    private int previusIndex;

    private int currentNumberOnKey;
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

    }
    public void Drop()
    {

    }

    public void UpdateOnInteract() {
        // Camera switch to Locker facing
        selectedIndex = (lockerKeys.Count - lockerKeys.Count);
    }
    private void Update()
    {
        if(isInteracted) PasswordUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MatchThePassword();
            player.enabled = true;
            player.playerController.enabled = true;
            lockerCamera.SetActive(false);
            player.playerCamera.SetActive(true);
            isInteracted = false;
        }
    }
    public void PasswordUpdate()
    {
        SelectWheel();
        RotateWheel();
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
        }
    }

    public void RotateWheel()
    {
        // rotate selected cylender on up down key
        currentNumberOnKey = int.Parse(currentKey.GetComponentInChildren<TextMeshPro>().text);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentNumberOnKey >= 9) currentNumberOnKey = 0;else currentNumberOnKey++;
            currentKey.GetComponentInChildren<TextMeshPro>().text = currentNumberOnKey.ToString();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentNumberOnKey <= 0) currentNumberOnKey = 9;else currentNumberOnKey--;
            currentKey.GetComponentInChildren<TextMeshPro>().text = currentNumberOnKey.ToString();
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
            key.GetComponentInChildren<TextMeshPro>().text = Random.Range(0, 10).ToString();
        }

        for (int i = 0; i < password.Length; i++)
        {
            password[i] = Random.Range(0, 10);
            passWordNumbers[i].text = password[i].ToString();
        }
    }

    bool MatchThePassword()
    {
        // Check if all the number matches or not accordingly
        for(int i=0; i< lockerKeys.Count;i++)
        {
            if (int.Parse(lockerKeys[i].GetComponentInChildren<TextMeshPro>().text) != password[i])
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
