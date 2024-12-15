using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Events;

namespace HorroHouse
{
    public class GameManager : Singleton<GameManager>
    {
        // Player-related variables
        [HideInInspector] public float playerLevel = 99999;
        public MainPlayer _PlayerObject;

        // Task-related variables
        public string[] Tasks;
        public int currentTask;
        public TextMeshProUGUI progressText;

        // Key-related variables
        public GameObject _TarraceKey;
        public GameObject _CarKey;
        public Transform[] _RandomPositionsForCarAndTarraceKey;

        // Manager-related variables
        public CaretakerManager _CaretakerManager;
        public int element;

        // Interaction and cutscene states
        public bool isPlayerInteracting = false;
        public bool isCutScenePlaying = false;

        //Event Related
        public UnityEvent[] onSubtitleStarts; 
        public void CollectElement(int type)
        {
            element++;
        }

        private void Start()
        {
            EventManager.Instance.eventForTaskComplete.Fuse2Connected?.Invoke();
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

        public void SetCurrentTask(int taskNumber)
        {
            if (currentTask > taskNumber)
                return;

            currentTask = taskNumber;
            //Audio to be played
            UIManager.Instance._taskAnimator.Play("TaskUpdated");
            UIManager.Instance._task.text = $"{Tasks[taskNumber]}";
        }
    }
}
