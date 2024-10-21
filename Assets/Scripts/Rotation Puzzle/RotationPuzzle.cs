using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HorroHouse.Player;
using HorroHouse;

public class RotationPuzzle : InteractBase, Interacter
{
    public List<Transform> lockerKeys;
    public List<Transform> passWordNumbers;
    public List<Collider> cols;

    public GameObject lockerCamera;
    public GameObject _basementKey;

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
        RandomizeTheNumber();
        _basementKey.SetActive(false);
    }

    public void Interact()
    {       
        player.enabled = false;
        player.playerController.enabled = false;
        lockerCamera.SetActive(true);
        player.playerCamera.SetActive(false);
        isInteracted = true;
        player.torch.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            MatchThePassword();
            player.playerCamera.SetActive(true);
            lockerCamera.SetActive(false);
            player.enabled = true;
            player.playerController.enabled = true;
            player.torch.SetActive(true);
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
        // Rotate locker objects on selection (left-right navigation)
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            previusIndex = selectedIndex;
            selectedIndex = (selectedIndex >= lockerKeys.Count - 1) ? 0 : selectedIndex + 1;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previusIndex = selectedIndex;
            selectedIndex = (selectedIndex <= 0) ? lockerKeys.Count - 1 : selectedIndex - 1;
        }

        currentKey = lockerKeys[selectedIndex];
        currentNumberOnKey = currentKey.eulerAngles.y;
        lockerKeys[previusIndex].GetComponent<Outline>().enabled = false;
        currentKey.GetComponent<Outline>().enabled = true;
    }

    public void RotateWheel()
    {
        // Rotate currentKey by 90 degrees and wrap within 0-360 range
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentNumberOnKey = (currentNumberOnKey + 90) % 360;
            currentKey.eulerAngles = new Vector3(0, currentNumberOnKey, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentNumberOnKey = (currentNumberOnKey - 90 + 360) % 360;
            currentKey.eulerAngles = new Vector3(0, currentNumberOnKey, 0);
        }
    }

    void RandomizeTheNumber()
    {       
        foreach (Transform key in lockerKeys)
        {
            // Randomly rotate lockers within 0-360 range
            int randomRotation = Random.Range(0, 4) * 90; // 0, 90, 180, 270
            key.eulerAngles = new Vector3(0, randomRotation, 0);
        }

        foreach (Transform pass in passWordNumbers)
        {
            // Randomly set password numbers' rotation within 0-360 range
            int randomRotation = Random.Range(0, 4) * 90;
            pass.eulerAngles = new Vector3(0, randomRotation, 0);
        }
    }
    bool MatchThePassword()
    {
        // Check if all locker rotations match the password rotations (normalize angle comparisons)
        for (int i = 0; i < lockerKeys.Count; i++)
        {
            float lockerRotation = NormalizeAngle(lockerKeys[i].eulerAngles.y);
            float passwordRotation = NormalizeAngle(passWordNumbers[i].eulerAngles.y);

            if (!AnglesAreEqual(lockerRotation, passwordRotation))
            {
                Debug.Log("<color=red><b>Password Not Matched !! </b></color>");
                return false;
            }
        }

        Debug.Log("<color=green><b>Password Matched !! </b></color>");
        DisableCollider();
        return true;
    }

    float NormalizeAngle(float angle)
    {
        // Normalize any angle to the range [0, 360)
        angle = angle % 360;
        if (angle < 0) angle += 360;
        return angle;
    }

    bool AnglesAreEqual(float angle1, float angle2)
    {
        // Compare two angles, allowing for minor float inaccuracies
        return Mathf.Approximately(angle1, angle2);
    }

    /* bool MatchThePassword()
     {
         // Compare locker and password direction vectors
         for (int i = 0; i < lockerKeys.Count; i++)
         {
             Vector3 lockerDirection = GetDirectionVector(lockerKeys[i]);
             Vector3 passwordDirection = GetDirectionVector(passWordNumbers[i].transform);

             if (!DirectionVectorsAreEqual(lockerDirection, passwordDirection))
             {
                 Debug.Log("<color=red><b>Password Not Matched !! </b></color>");
                 return false;
             }
         }

         Debug.Log("<color=green><b>Password Matched !! </b></color>");
         DisableCollider();
         return true;
     }

     Vector3 GetDirectionVector(Transform obj)
     {
         // Get the forward direction vector based on Y rotation
         return obj.forward;
     }

     bool DirectionVectorsAreEqual(Vector3 dir1, Vector3 dir2)
     {
         // Compare direction vectors with a small tolerance
         return Vector3.Dot(dir1, dir2) > 0.99f; // Tolerance for direction comparison
     }*/

    void DisableCollider()
    {
        isOpend = true;
        _basementKey.SetActive(true);
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
    }
}
