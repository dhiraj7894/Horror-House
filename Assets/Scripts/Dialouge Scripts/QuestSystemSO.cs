using UnityEngine;

[CreateAssetMenu(fileName = "QuestSystemSO", menuName = "HH/QuestInfoSO")]
public class QuestSystemSO : ScriptableObject
{
    public string Id { get; private set; }

    [Header("General")]
    public string displayQuestName;
    [Header("Requirements")]
    public int levelRequirement;
    public QuestSystemSO[] questRequirement;

    [Header("Steps")]
    public GameObject[] questSteps;

    [Header("Rewards")]
    public int XPPointsReward;
    public int currencyReward;
    public GameObject objectReward;

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
