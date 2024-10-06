using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace HorroHouse
{
    public class GameManager : Singleton<GameManager>
    {
        [TextArea(15, 20)]
        public string Tasks;
        
        public MainPlayer _PlayerObject;
        public int element;
        public GameObject _TarraceKey;
        public GameObject _CarKey;
        public Transform[] _RandomPositionsForCarAndTarraceKey;

        public bool isPlayerInteracting = false;

        public void CollectElement(int type)
        {
            element++;
        }

        private void Start()
        {
            UIManager.Instance._task.text = Tasks;
        }

        public void SelectPositionForCarNTarraceKey()
        {
            int i = Random.Range(0, _RandomPositionsForCarAndTarraceKey.Length);
            int j = Random.Range(0, _RandomPositionsForCarAndTarraceKey.Length);

            if (i == j)
                SelectPositionForCarNTarraceKey();
            else
            {
                _TarraceKey.SetActive(true);
                _CarKey.SetActive(true);
                _TarraceKey.transform.position = _RandomPositionsForCarAndTarraceKey[i].position;
                _CarKey.transform.position = _RandomPositionsForCarAndTarraceKey[j].position;

            }
            Debug.Log("Frame Unlocked");
        }
    }
}
