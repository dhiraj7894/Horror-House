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
                    inT.Interact();
                    UIManager.Instance._interactionUI._UIText.text = "";
                    LeanTween.value(1, 0, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
                }
            }
        }

        
        public void RaycastUpdate()
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _distance, interactableLayer))
            {
                if (hit.transform.TryGetComponent<InteractBase>(out InteractBase interactBase))
                {
                    if (!this.interactBase)
                    {
                        this.interactBase = interactBase;
                        interactBase._Heighlight.SetActive(true);
                        UIManager.Instance._interactionUI._UIText.text = interactBase._UIText;
                        LeanTween.value(0, 1, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
                    }                                       
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

        }

        public void NonInteractionCode()
        {            
            interactBase._Heighlight.SetActive(false);
            interactBase = null;
            UIManager.Instance._interactionUI._UIText.text = "";
            LeanTween.value(1, 0, time).setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
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
