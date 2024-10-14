using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public QuestPoint QuestPoint;

    public void StartQuest()
    {
        GameManager.Instance.CutSceneStatus(true);
        QuestPoint.ActivateQuest();
    }
    private void Start()
    {
        LeanTween.delayedCall(1, () => StartQuest()); 
    }
    public void Update()
    {
        QuestCollisionChecking();
    }

    void QuestCollisionChecking()
    {
        switch (QuestPoint.currentQuestState)
        {
            case QuestState.REQUIREMENT_NOT_MET:                
                break;
            case QuestState.CAN_START:               
                break;
            case QuestState.IN_PROGRESS:                
                break;
            case QuestState.CAN_FINISH:                
                break;
            case QuestState.FINISHED:                
                GameManager.Instance.CutSceneStatus(false);
                break;

        }
    }
}
