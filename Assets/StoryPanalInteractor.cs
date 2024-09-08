using HorroHouse;
using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPanalInteractor : InteractBase, Interacter
{
    [TextArea(3, 10)]
    public string Story;

    private MainPlayer _player;
    private void Start()
    {
        _player = GameManager.Instance._PlayerObject;
    }
    public void Drop()
    {
        
    }

    public void Interact()
    {
        UIManager.Instance._storyPanal.SetData(true, Story);
        GetComponent<Collider>().enabled = false;
        _player.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Collider>().enabled = true;
            UIManager.Instance._storyPanal.SetData(false, Story);
            _player.enabled = true;
        }
    }
}
