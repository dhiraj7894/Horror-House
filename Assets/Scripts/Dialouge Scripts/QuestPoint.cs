using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{    

    [Header("Quest")]
    [SerializeField] private QuestSystemSO questInfoForPoint;
    private string questId;
    public QuestState currentQuestState;    
    public bool isStartPoint = false;
    public bool isFinishPoint = false;

    [Header("Quest Releted Item")]
    public List<GameObject> QuestItem = new List<GameObject>();


    private void Awake()
    {
        questId = questInfoForPoint.Id;
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {

    }

    private void OnEnable()
    {
        QuestItemData();
        QuestEvent.onQuestStateChange += QuestStateChange;
        //EventManager.Instance.PressFButton += ActivateQuest;
    }
    private void OnDisable()
    {
        QuestEvent.onQuestStateChange -= QuestStateChange;
        //EventManager.Instance.PressFButton -= ActivateQuest;
    }

    public void QuestStateChange(Quest quest)
    {
        if (quest.info.Id.Equals(questId))
        {
            currentQuestState = quest.state;
            //QuestIcon.SetState(currentQuestState, isStartPoint, isFinishPoint);
        }
    }

    public void ActivateQuest()
    {

        if (currentQuestState.Equals(QuestState.CAN_START) && isStartPoint)
        {

            UIManager.Instance.CutSceneFadeOutIn(0.5f);
            LeanTween.delayedCall(.4f, () => {
                QuestEvent.StartQuest(questId);
                QuestItemData(true);
            });

        }
        if (currentQuestState.Equals(QuestState.CAN_FINISH) && isFinishPoint)
        {
            QuestItemData();
            UIManager.Instance.CutSceneFadeOutIn(0.5f);
            LeanTween.delayedCall(.4f, () => {
                QuestEvent.FinishQuest(questId);
            });

        }

    }

    public void QuestItemData(bool isTrue = false)
    {
        if (QuestItem.Count > 0)
        {
            foreach (GameObject item in QuestItem)
            {
                item.SetActive(isTrue);
            }
        }
    }
}
