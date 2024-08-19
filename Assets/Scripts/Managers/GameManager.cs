using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorroHouse
{
    public class GameManager : Singleton<GameManager>
    {
        public MainPlayer _PlayerObject;
        public int element;
        public void CollectElement(int type)
        {
            element++;
        }
    }
}
