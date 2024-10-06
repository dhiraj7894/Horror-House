using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
                        if(!interactBase.isLocked && itemData) {
                            if (interactBase._Heighlight) interactBase._Heighlight.enabled = false;
                            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                            inT.Interact();
                            if (_targetPlace.childCount <= 0)
                            {
                                Debug.Log("Child Count Null");
                                UIManager.Instance._itemInHand.text = "Null";
                            }
                        }
                        else if (!interactBase.isLocked)
                        {
                            if (interactBase._Heighlight) interactBase._Heighlight.enabled = false;
                            UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                            inT.Interact();
                            if (_targetPlace.childCount <= 0)
                            {
                                Debug.Log("Child Count Null");
                                UIManager.Instance._itemInHand.text = "Null";
                            }
                        }
                        
                    }                    
                }
            }

            if (!interactBase)
            {
                if (UIManager.Instance._interactionUI._canvasGroup.alpha == 1) 
                    UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
            }

        }        
        public void RaycastUpdate()
        {
            InteractBase previousInteractBase = interactBase; // Store the previous interactBase            
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _distance, interactableLayer))
            {
                if (hit.transform.TryGetComponent<InteractBase>(out InteractBase newInteractBase))
                {
                    interactBase = newInteractBase; // Set the new interactBase
                    
                    // Disable the previous interactBase's highlight, if it's different
                    if (previousInteractBase && previousInteractBase != interactBase)
                    {
                        if (previousInteractBase._Heighlight) previousInteractBase._Heighlight.enabled = false;                        
                    }
                    

                    // Enable the new interactBase's highlight
                    if (interactBase._Heighlight) interactBase._Heighlight.enabled = true;

                    // Update interaction UI
                    if (!interactBase.isLocked)
                        UIManager.Instance._interactionUI._UIText.text = interactBase._UIText;
                    else
                        UIManager.Instance._interactionUI._UIText.text = interactBase._LockedText;

                    LeanTween.value(UIManager.Instance._interactionUI._canvasGroup.alpha, 1, time)
                             .setOnUpdate((float val) => { UIManager.Instance._interactionUI._canvasGroup.alpha = val; });
                }
                else if(interactBase)
                {                    
                    UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                    interactBase = null;
                }
            }
            else if (interactBase)
            {
                // No hit, disable the current interactBase's highlight
                if (interactBase._Heighlight) interactBase._Heighlight.enabled = false;
                UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
                interactBase = null;                
            }

            if (!interactBase && UIManager.Instance._interactionUI._canvasGroup.alpha == 1)
            {
                UIManager.Instance._interactionUI._canvasGroup.alpha = 0;
            }            
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
