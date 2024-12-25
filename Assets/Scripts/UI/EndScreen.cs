using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private MainPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance._PlayerObject;
    }


    public void OnEndOfScreen()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
