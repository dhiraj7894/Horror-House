using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerCutscene : QuestStep
{
    public TextAsset inkJSON;
    private void Start()
    {        
        DialogueManager.Instance.EnterFullScreenDialogueMode(inkJSON, this);
    }
}
