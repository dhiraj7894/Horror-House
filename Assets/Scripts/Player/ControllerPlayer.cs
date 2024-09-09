using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorroHouse.Player
{
    public class ControllerPlayer : MonoBehaviour
    {
        
        public LayerMask interactableLayer;
        [Space(10)]
        public Item itemData;
        [SerializeField] InteractBase interactBase;

        [Space(5)]
        public MainPlayer mainPlayer;
        public Transform _camera;
        public Transform _targetPlace;

        [Space(5)]
        public float _distance = 10;
        public float time = .1f;
        private void Start()
        {
            EventManager.Instance.PressFButton += PressFButton;
            EventManager.Instance.PressGButton += PressGButton;
        }

        private void PressFButton()
        {
            CheckRaycaster();
            if (interactBase) interactBase = null;
            if (itemData && _targetPlace.childCount<=0) itemData = null;
        }
        private void PressGButton()
        {
            DropItem();
        }

        public void DropItem()
        {
            if (_targetPlace.childCount >= 1)
            {
                _targetPlace.GetChild(0).GetComponent<Interacter>().Drop();
                if (itemData)
                    itemData = null;
            }
        }

        private void Update()
        {
            RaycastUpdate();
        }
        public void CheckRaycaster()
        {
            if(Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _distance, interactableLayer))
            {
                if(hit.transform.TryGetComponent<Interacter>(out Interacter inT))
                {
                    
                    if (hit.transform.GetComponent<InteractBase>())
                    {
                        InteractBase interactBase = hit.transform.GetComponent<InteractBase>();
                        if(interactBase.isLocked && itemData) {
                            if (interactBase._Heighlight) interactBase._Heighlight.enabled = false;
                            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                            inT.Interact();
                        }else if (!interactBase.isLocked)
                        {
                            if (interactBase._Heighlight) interactBase._Heighlight.enabled = false;
                            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                            inT.Interact();
                        }
                        
                    }                    
                }
            }

            if (!interactBase)
            {
                if (UIManager.Instance._interactionUI._canvasGroup.alpha == 1) UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
            }
        }

        
        public void RaycastUpdate()
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _distance, interactableLayer))
            {
                if (hit.transform.TryGetComponent<InteractBase>(out InteractBase interactBase))
                {
                    this.interactBase = interactBase;
                    if (interactBase._Heighlight) interactBase._Heighlight.enabled = true;
                    UIManager.Instance._interactionUI._UIText.text = interactBase._UIText;
                    LeanTween.value(UIManager.Instance._interactionUI._canvasGroup.alpha, 1, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });                                      
                }
                else 
                {
                    if (this.interactBase)
                    {
                        NonInteractionCode();
                    }
                        
                }

            }
            else if(interactBase)
            {
                NonInteractionCode();
            }

            if (!interactBase)
            {
                if (UIManager.Instance._interactionUI._canvasGroup.alpha == 1) UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
            }

        }

        public void NonInteractionCode()
        {            
            if(interactBase._Heighlight) interactBase._Heighlight.enabled = false;
            interactBase = null;
            UIManager.Instance._interactionUI._UIText.text = "";
            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
            //LeanTween.value(UIManager.Instance._interactionUI._canvasGroup.alpha, 0, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(AnimHash.LIFT))
            {
                this.transform.parent = other.transform;
                mainPlayer.isInLift = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(AnimHash.LIFT))
            {
                this.transform.parent = null;
                mainPlayer.isInLift = false;
            }
        }
    }
}
