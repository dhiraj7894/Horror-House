using TMPro;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    public Outline _Heighlight;   
    public Animator _Animator;
    public bool isLocked = false;
    public string _UIText;
    public string _LockedText;

    public void DisableLock()
    {
        isLocked = false;
    }
}
