using UnityEngine;

/// <summary>
/// Represents subtitle information used in cutscenes or gameplay events.
/// </summary>
[CreateAssetMenu(fileName = "Subtitle Texts", menuName = "HH/SubtitleInfo")]
public class SubtitleTexts : ScriptableObject
{
    [Header("Subtitle Content")]
    [TextArea(3, 10)]
    [Tooltip("The text to display for the subtitle.")]
    public string subtitleText;
    [Tooltip("The audio clip associated with this subtitle.")]
    public AudioClip audioClip;

    [Header("Subtitle Sequence")]
    [Tooltip("The next subtitle in the sequence.")]
    public SubtitleTexts nextSubtitle;
    [Tooltip("If true, the subtitle automatically continues to the next.")]
    public bool isAutoContinue = false;

    [Header("Event Configuration")]
    [Tooltip("Indicates whether an event should be triggered when this subtitle is active.")]
    public bool triggerEvent;
    [Tooltip("The event number to start.")]
    public int eventStartNumber;

    [Header("Player and Scene Settings")]
    [Tooltip("The desired player rotation when this subtitle is active.")]
    public Vector3 playerRotation;
    [Tooltip("Indicates whether this subtitle is part of a cutscene.")]
    public bool isCutscene = false;
}
