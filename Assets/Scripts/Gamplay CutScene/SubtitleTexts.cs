using UnityEngine;

[CreateAssetMenu(fileName = "Sutitle Texts", menuName = "HH/SubtitleInfo")]
public class SubtitleTexts : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
    public AudioClip audioClip;
    public SubtitleTexts nextStep;
    public bool isContinue = false;
    public bool isCutscene = false;
}
