using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Full Screen Dialogue UIs")]
    public GameObject fullScreenDialogueUI;
    public GameObject fullScreenContinueImage;
    public TextMeshProUGUI fullScreenDialogueText;

    [Header("Dialogue UIs")]
    public GameObject dialogueUI;
    public GameObject continueImage;
    public TextMeshProUGUI dialogueText;

    public List<GameObject> uIToDesableOnDialogue = new List<GameObject>();


    [Header("Dialouge Audio Elements")]
    public VoiceLineSO voiceLineSO;
    public AudioSource audioSource;


    public float typingSpeed = 0.04f;


    public bool canContinueToNextLine = false;

    public Story currentStory;
    public bool isDialoguePlaying { get; private set; }
    public bool isFullScreen = false;
    [Header("Quest Objects")]
    public QuestStep step;
    Coroutine displayLineCoroutine;

    private void Start()
    {
        isDialoguePlaying = false;
        SetActiveUIObjects();
        SetActiveFullScreenUIObjects();
    }

    #region Dialouge System
    public void EnterDialogueMode(TextAsset inkJSON, QuestStep step = null, bool isFullScreen = false)
    {
        this.isFullScreen = isFullScreen;
        currentStory = new Story(inkJSON.text);
        if (step != null)
        {
            this.step = step;
        }
        //displaySpeakerName.text = "";
        isDialoguePlaying = true;
        SetActiveUIObjects(true);
        ContinueStory();

    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //audioSource.Stop();
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
            //dialogueText.text = currentStory.Continue();

        }
        else
        {

            ExitDialogueMode();
        }
    }

    IEnumerator DisplayLine(string line)
    {
        //Before text line is shown in dialogue panal
        dialogueText.text = "";
        canContinueToNextLine = false;
        continueImage.SetActive(false);
        //PlayDialogueClip(dialogueSoundClip, true);
        ////

        int i = 0;
        foreach (char letter in line.ToCharArray())
        {
            if (InputActions._submit.triggered && i >= (line.Length / 2))
            {
                dialogueText.text = line.ToString();
                break;
            }

            dialogueText.text += letter;
            i++;
            yield return new WaitForSeconds(typingSpeed);
        }


        ///
        //After text line is shown in dialogue panal
        //PlayDialogueClip(dialogueSoundClip, false);
        canContinueToNextLine = true;
        continueImage.SetActive(true);
    }
    #endregion

    #region Full Screen Dialouge System
    public void EnterFullScreenDialogueMode(TextAsset inkJSON, QuestStep step = null, bool isFullScreen = true)
    {
        this.isFullScreen = isFullScreen;
        currentStory = new Story(inkJSON.text);
        if (step != null)
        {
            this.step = step;
        }
        //displaySpeakerName.text = "";
        isDialoguePlaying = true;
        SetActiveFullScreenUIObjects(true);
        FullScreenContinueStory();

    }

    void FullScreenContinueStory()
    {
        if (currentStory.canContinue)
        {
            audioSource.Stop();
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(FullScreenDisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
            //dialogueText.text = currentStory.Continue();

        }
        else
        {
            ExitDialogueMode();
        }
    }

    IEnumerator FullScreenDisplayLine(string line)
    {
        //Before text line is shown in dialogue panal
        fullScreenDialogueText.text = "";
        canContinueToNextLine = false;
        fullScreenContinueImage.SetActive(false);
        //PlayDialogueClip(dialogueSoundClip, true);
        ////

        int i = 0;
        foreach (char letter in line.ToCharArray())
        {
            if (InputActions._submit.triggered && i >= (line.Length / 2))
            {
                fullScreenDialogueText.text = line.ToString();
                break;
            }

            fullScreenDialogueText.text += letter;
            i++;
            yield return new WaitForSeconds(typingSpeed);
        }


        ///
        //After text line is shown in dialogue panal
        //PlayDialogueClip(dialogueSoundClip, false);
        canContinueToNextLine = true;
        fullScreenContinueImage.SetActive(true);
    }
    #endregion

    void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTags = tag.Split(':');
            if (splitTags.Length != 2)
            {
                Debug.LogError("Tag could not be correctly written: " + tag);
            }

            string tagKey = splitTags[0].Trim();
            string tagValue = splitTags[1].Trim();
            switch (tagKey)
            {
                case INKTags.SPEAKER:
                    //displaySpeakerName.text = tagValue;
                    break;
                case INKTags.VOICELINE:
                    GetVoiceClipFromSO(tagValue);
                    break;
                default:
                    Debug.Log("Nothing to show here");
                    break;
            }
        }

    }
    public void GetVoiceClipFromSO(string vcName)
    {

        foreach (AudioClip item in voiceLineSO.voiceClips)
        {
            if (item.name.Contains(vcName))
            {
                audioSource.clip = item;
                audioSource.Play();
            }
        }
    }

    public void ExitDialogueMode()
    {
        audioSource.Stop();
        audioSource = null;
        isDialoguePlaying = false;
        dialogueText.text = "";
        fullScreenDialogueText.text = "";
        SetActiveUIObjects();
        SetActiveFullScreenUIObjects();
        if (step != null)
        {
            step.FinishedQuestStep();
        }
        EventManager.Instance.eventForTask.CutSceneCompleted?.Invoke();
    }


    private void Update()
    {
        if (!isDialoguePlaying)
        {
            return;
        }

        if (InputActions._submit.triggered && canContinueToNextLine)
        {
            if(!isFullScreen) ContinueStory();
            else FullScreenContinueStory();
        }

    }




    /// <summary>
    /// This method handles Canavas object which need to active when we start dialogues
    /// List of objects will set active to false and the dialogue object will active true
    /// </summary>
    /// <param name="isTrue"></param>
    public void SetActiveUIObjects(bool isTrue = false)
    {
        dialogueUI.SetActive(isTrue);
        if (uIToDesableOnDialogue.Count > 0)
        {
            foreach (GameObject item in uIToDesableOnDialogue)
            {
                item.SetActive(!isTrue);
            }
        }
    }
    public void SetActiveFullScreenUIObjects(bool isTrue = false)
    {
        fullScreenDialogueUI.SetActive(isTrue);
        if (uIToDesableOnDialogue.Count > 0)
        {
            foreach (GameObject item in uIToDesableOnDialogue)
            {
                item.SetActive(!isTrue);
            }
        }
    }































}
