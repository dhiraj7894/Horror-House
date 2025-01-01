using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HorroHouse
{
    [System.Serializable]
    public struct LightData {
        public Texture2D[] darkLightmapDir, darkLightmapColor;
        public Texture2D[] brightLightmapDir, brightLightmapColor;

        public GameObject[] lights;
        public Material[] mat;

        public LightmapData[] darkLightmap, brightLightmap;
        public bool lightChange;
    }

    public class GameManager : Singleton<GameManager>
    {

        [SerializeField] private LightData lightData;

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
            EventManager.Instance.eventForTask.CutSceneCompleted?.Invoke();
            LightChange();
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
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftAlt))
                Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKey(KeyCode.LeftAlt))
                Cursor.lockState = CursorLockMode.None;
        }

        public void SetCurrentTask(int taskNumber)
        {
            if (currentTask > taskNumber)
                return;


            currentTask = taskNumber;
            //Audio to be played
            AudioManager.Instance.PlayPlayerAudio(AudioManager.Instance.audioSource.sfxSource,
            AudioManager.Instance.GetAudio(AudioManager.Instance.audioBank.taskSelected
            ));
            UIManager.Instance._taskAnimator.Play("TaskUpdated");
            UIManager.Instance._task.text = $"{Tasks[taskNumber]}";
        }

        
        [ContextMenu("StartLight")]
        public void LightmapChange()
        {
            if (lightData.lightChange)
            {
                LightmapSettings.lightmaps = lightData.darkLightmap;
                lightData.lightChange = false;
                LightsOnOff(lightData.lightChange);
            }
            else
            {
                LightmapSettings.lightmaps = lightData.brightLightmap;
                lightData.lightChange = true;
                LightsOnOff(lightData.lightChange);
            }
        }
        private void LightChange()
        {
            List<LightmapData> dlightmap = new List<LightmapData>();

            for (int i = 0; i < lightData.darkLightmapDir.Length; i++)
            {
                LightmapData lmdata = new LightmapData();
                lmdata.lightmapDir = lightData.darkLightmapDir[i];
                lmdata.lightmapColor = lightData.darkLightmapColor[i];

                dlightmap.Add(lmdata);
            }
            lightData.darkLightmap = dlightmap.ToArray();


            List<LightmapData> blightmap = new List<LightmapData>();

            for (int i = 0; i < lightData.darkLightmapDir.Length; i++)
            {
                LightmapData lmdata = new LightmapData();
                lmdata.lightmapDir = lightData.brightLightmapDir[i];
                lmdata.lightmapColor = lightData.brightLightmapColor[i];

                blightmap.Add(lmdata);
            }
            lightData.brightLightmap = blightmap.ToArray();

        }
        public void LightsOnOff(bool isTrue)
        {
            foreach (GameObject item in lightData.lights)
            {
                item.SetActive(isTrue);
            }
            if (isTrue)
            {
                foreach (Material item in lightData.mat)
                {
                    item.EnableKeyword("_EMISSION");
                    //item.SetColor("_EmissionColor", emissionColor);
                }
            }
            else
            {
                foreach (Material item in lightData.mat)
                {
                    item.DisableKeyword("_EMISSION");
                }
            }
        }

    }
}
