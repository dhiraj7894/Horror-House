using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HorroHouse
{
    public class UIManager : Singleton<UIManager>
    {
        public TextMeshProUGUI _currentProgressTxt;
        public InteractionUI _interactionUI;
        public StoryPanal _storyPanal;
    }
}