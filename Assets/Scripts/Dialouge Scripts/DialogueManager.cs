using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using HorroHouse.Player;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Full Screen Dialogue UIs")]
    public GameObject fullScreenDialogueUI; // UI for full-screen dialogue
    public GameObject fullScreenContinueImage; // Continue button image for full-screen mode
    public TextMeshProUGUI fullScreenDialogueText; // Text field for displaying dialogue in full-screen mode

    [Header("Dialogue UIs")]
    public GameObject dialogueUI; // UI for standard dialogue
    public GameObject continueImage; // Continue button image for standard dialogue
    public TextMeshProUGUI dialogueText; // Text field for displaying dialogue

    public List<GameObject> uIToDesableOnDialogue = new List<GameObject>(); // List of UI elements to disable during dialogue
    public MainPlayer mainPlayer; // Reference to the main player script

    [Header("Dialogue Audio Elements")]
    public VoiceLineSO voiceLineSO; // Scriptable object holding voice line audio clips
    public AudioSource audioSource; // Audio source for playing voice lines

    public float typingSpeed = 0.04f; // Speed of subtitleText typing effect
    public bool canContinueToNextLine = false; // Flag to check if the player can proceed to the next line
    public Story currentStory; // Current Ink story being processed
    public bool isDialoguePlaying { get; private set; } // Is dialogue currently active
    public bool isFullScreen = false; // Is full-screen mode enabled

    [Header("Quest Objects")]
    public QuestStep step; // Current quest step associated with the dialogue
    private Coroutine displayLineCoroutine; // Coroutine reference for displaying lines

    private void Start()
    {
        isDialoguePlaying = false;
        SetActiveUIObjects(); // Ensure UI elements are properly set on start
        SetActiveFullScreenUIObjects(); // Initialize full-screen UI state
    }

    #region Dialogue System
    public void EnterDialogueMode(TextAsset inkJSON, QuestStep step = null, bool isFullScreen = false)
    {
        this.isFullScreen = isFullScreen;
        currentStory = new Story(inkJSON.text); // Load the Ink story file

        if (step != null)
        {
            this.step = step; // Assign quest step if provided
        }

        isDialoguePlaying = true; // Enable dialogue mode
        SetActiveUIObjects(true); // Activate dialogue UI
        ContinueStory(); // Start the story
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue) // Check if the story has more lines
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine); // Stop any ongoing line display
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue())); // Display the next line
            HandleTags(currentStory.currentTags); // Process tags in the line
        }
        else
        {
            ExitDialogueMode(); // Exit dialogue if no lines remain           
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = ""; // Clear existing subtitleText
        canContinueToNextLine = false; // Disable continue button
        continueImage.SetActive(false);

        int i = 0;
        foreach (char letter in line)
        {
            if (InputActions._submit.triggered && i >= (line.Length / 2))
            {
                dialogueText.text = line; // Skip typing effect if submit is triggered midway
                break;
            }

            dialogueText.text += letter; // Add character to dialogue subtitleText
            i++;
            yield return new WaitForSeconds(typingSpeed); // Wait for typing speed interval
        }

        canContinueToNextLine = true; // Enable continue button
        continueImage.SetActive(true);
    }
    #endregion

    #region Full Screen Dialogue System
    public void EnterFullScreenDialogueMode(TextAsset inkJSON, QuestStep step = null, bool isFullScreen = true)
    {
        this.isFullScreen = isFullScreen;
        currentStory = new Story(inkJSON.text); // Load the Ink story file

        if (step != null)
        {
            this.step = step; // Assign quest step if provided
        }

        isDialoguePlaying = true; // Enable dialogue mode
        SetActiveFullScreenUIObjects(true); // Activate full-screen dialogue UI
        FullScreenContinueStory(); // Start the full-screen story
    }

    private void FullScreenContinueStory()
    {
        if (currentStory.canContinue) // Check if the story has more lines
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine); // Stop any ongoing line display
            }
            displayLineCoroutine = StartCoroutine(FullScreenDisplayLine(currentStory.Continue())); // Display the next line
            HandleTags(currentStory.currentTags); // Process tags in the line
        }
        else
        {
            ExitDialogueMode(); // Exit dialogue if no lines remain
        }
    }

    private IEnumerator FullScreenDisplayLine(string line)
    {
        fullScreenDialogueText.text = ""; // Clear existing subtitleText
        canContinueToNextLine = false; // Disable continue button
        fullScreenContinueImage.SetActive(false);

        int i = 0;
        foreach (char letter in line)
        {
            if (InputActions._submit.triggered && i >= (line.Length / line.Length)) // Devide this with 2 for better performance
            {
                fullScreenDialogueText.text = line; // Skip typing effect if submit is triggered midway
                break;
            }

            fullScreenDialogueText.text += letter; // Add character to full-screen dialogue subtitleText
            i++;
            yield return new WaitForSeconds(typingSpeed); // Wait for typing speed interval
        }

        canContinueToNextLine = true; // Enable continue button
        fullScreenContinueImage.SetActive(true);
    }
    #endregion

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTags = tag.Split(':'); // Split tag into key-value pair
            if (splitTags.Length != 2)
            {
                Debug.LogError($"Tag could not be correctly written: {tag}");
                continue;
            }

            string tagKey = splitTags[0].Trim(); // Extract key
            string tagValue = splitTags[1].Trim(); // Extract value

            switch (tagKey)
            {
                case INKTags.SPEAKER:
                    // Handle speaker tag (future implementation)
                    break;
                case INKTags.VOICELINE:
                    GetVoiceClipFromSO(tagValue); // Play voice line
                    break;
                default:
                    Debug.Log("Unhandled tag: " + tagKey); // Log unhandled tags
                    break;
            }
        }
    }

    public void GetVoiceClipFromSO(string vcName)
    {
        foreach (AudioClip clip in voiceLineSO.voiceClips)
        {
            if (clip.name.Contains(vcName)) // Match clip name with tag value
            {
                audioSource.clip = clip; // Assign and play clip
                audioSource.Play();
                break;
            }
        }
    }

    public void ExitDialogueMode()
    {
        audioSource.Stop(); // Stop any playing audio
        audioSource = null; // Reset audio source
        isDialoguePlaying = false; // Disable dialogue mode
        dialogueText.text = ""; // Clear standard dialogue subtitleText
        fullScreenDialogueText.text = ""; // Clear full-screen dialogue subtitleText
        SetActiveUIObjects(); // Reset standard UI elements
        SetActiveFullScreenUIObjects(); // Reset full-screen UI elements
        
        if (step != null)
        {
            step.FinishedQuestStep(); // Mark quest step as completed
        }

        mainPlayer.anim.Play(AnimHash.GettingUp); // Trigger player getting up animation
    }

    private void Update()
    {
        if (!isDialoguePlaying)
        {
            return; // Skip update if dialogue is not active
        }

        if (InputActions._submit.triggered && canContinueToNextLine) // Check for continue input
        {
            if (!isFullScreen)
            {
                ContinueStory(); // Continue standard story
            }
            else
            {
                FullScreenContinueStory(); // Continue full-screen story
            }
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
