using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorroHouse;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class QuestManager : Singleton<QuestManager>
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            QuestEvent.QuestStateChange(quest);
        }
    }
    private Dictionary<string, Quest> CreateQuestMap()
    {        
        QuestSystemSO[] allQuest = Resources.LoadAll<QuestSystemSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestSystemSO questInfo in allQuest)
        {            
            if (idToQuestMap.ContainsKey(questInfo.Id))
            {
                Debug.LogError($"Duplicate Quest ID: {questInfo.Id}. Skipping addition.");
                continue;

            }
            idToQuestMap.Add(questInfo.Id, new Quest(questInfo)); 
            
        }        
        return idToQuestMap;
    }

    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
       
        if (quest == null)
        {
            Debug.Log($"ID does not exist in quest map : {id}");
        }
        return quest;
    }

    void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        QuestEvent.QuestStateChange(quest);
    }

    private void OnEnable()
    {
        QuestEvent.onStartQuest += StartQuest;
        QuestEvent.onAdvanceQuest += AdvacneQuest;
        QuestEvent.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        QuestEvent.onStartQuest -= StartQuest;
        QuestEvent.onAdvanceQuest -= AdvacneQuest;
        QuestEvent.onFinishQuest -= FinishQuest;
    }

    bool CheckRequirementMet(Quest quest)
    {
        bool meetRequirement = true;
        if (GameManager.Instance.playerLevel < quest.info.levelRequirement)
        {
            meetRequirement = false;
        }
        foreach (QuestSystemSO preRequirementInfo in quest.info.questRequirement)
        {
            if (GetQuestByID(preRequirementInfo.Id).state != QuestState.FINISHED)
            {
                meetRequirement = false;
            }
        }
        return meetRequirement;
    }

    private void Update()
    {

        foreach (Quest quest in questMap.Values)
        {
            //Debug.Log($"WHY ?");
            if (quest.state == QuestState.REQUIREMENT_NOT_MET && CheckRequirementMet(quest))
            {
                ChangeQuestState(quest.info.Id, QuestState.CAN_START);
            }
        }
    }


    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.SpwanCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.Id, QuestState.IN_PROGRESS);
    }
    private void AdvacneQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.MoveToNextStep();
        if (quest.CurrentStepExists())
        {
            quest.SpwanCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.Id, QuestState.CAN_FINISH);
        }

    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimReward(quest);
    }


    void ClaimReward(Quest quest)
    {
        //Add XP to player level
        //Add Object reward to player invetory 
        //Add In Jelly Currency as reward
        Debug.Log($"<color=green>Jelly Reward Added to you inventory</color>");
        ChangeQuestState(quest.info.Id, QuestState.FINISHED);
    }
}
