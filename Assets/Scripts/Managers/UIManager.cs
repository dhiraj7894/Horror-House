using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HorroHouse
{
    public class UIManager : Singleton<UIManager>
    {
        public TextMeshProUGUI _itemInHand;
        public TextMeshProUGUI _task;
        public InteractionUI _interactionUI;
        public StoryPanal _storyPanal;

        public void SetInHandItemString(string text="")
        {
            if (text.Length > 0)
            {
                _itemInHand.text = text;
            }
            else
            {
                _itemInHand.text = "";
            }
        }
    }
}