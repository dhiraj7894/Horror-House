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
        public Animator _taskAnimator;

        public InteractionUI _interactionUI;
        public StoryPanal _storyPanal;

        [Header("Cut Scene Items"), Space(5)]
        public CanvasGroup backScreenCutOut;
        public bool isBackScreenFadeActive = false;

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
        public void CutSceneFadeOutIn(float cooldown)
        {
            /*LeanTween.value(this.gameObject, 0, 1, 0.4f).
                setOnUpdate((float val) => {
                    backScreenCutOut.alpha = val;
                    isBackScreenFadeActive = true;
                }).
                setOnComplete(() => {
                    LeanTween.delayedCall(cooldown, () => {
                        LeanTween.value(this.gameObject, 1, 0, 0.4f).setOnUpdate((float val) =>
                        {
                            backScreenCutOut.alpha = val;

                        }).setOnComplete(() => isBackScreenFadeActive = false);
                    });
                });*/
        }
    }
}