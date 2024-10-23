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
        [HideInInspector] public float playerLevel = 99999;        
        public string[] Tasks;
        public int currentTask;
        
        public MainPlayer _PlayerObject;
        public CaretakerManager _CaretakerManager;
        public int element;
        public GameObject _TarraceKey;
        public GameObject _CarKey;
        public Transform[] _RandomPositionsForCarAndTarraceKey;

        public bool isPlayerInteracting = false;
        public bool isCutScenePlaying = false;

        public void CollectElement(int type)
        {
            element++;
        }

        private void Start()
        {
            UIManager.Instance._task.text = Tasks[currentTask];
            EventManager.Instance.eventForTask.CutSceneCompleted?.Invoke();
        }
        private void Update()
        {
            MouseCursorUpdate();
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

        public void CutSceneStatus(bool isTrue)
        {
            isCutScenePlaying = isTrue;
        }

        public void MouseCursorUpdate()
        {
            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKeyDown(KeyCode.LeftAlt))
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
