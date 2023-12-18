using UnityEngine;
using UnityEngine.UI;

//Interacting with physics objects and interactive objects
namespace Highlands
{
    public class PlayerInteractions : MonoBehaviour
    {
        [Header("Interaction variables")]
        [Tooltip("Layer mask for interactive objects")]
        [SerializeField] private LayerMask interactionLayer;
        [Tooltip("Maximum distance from player to object of interaction")]
        [SerializeField] private float interactionDistance = 3f;
        [Tooltip("Tag for interactive object")]
        [SerializeField] private string interactiveTag = "Interactive";
        [Tooltip("Tag for pickable object")]
        [SerializeField] private string itemTag = "Item";
        [Tooltip("Tag for UI object")]
        [SerializeField] private string UITag1 = "UI1";
        [Tooltip("Tag for UI object")]
        [SerializeField] private string UITag2 = "UI2";
        [Tooltip("Tag for UI object")]
        [SerializeField] private string UITag3 = "UI3";
        [Tooltip("The player's main camera")]
        [SerializeField] private string Return = "ReturnUI";
        [Tooltip("The player's main camera")]
        [SerializeField] private Camera mainCamera;
        [Tooltip("Parent object where the object to be lifted becomes")]
        [SerializeField] private Transform pickupParent;

        [Header("Keybinds")]
        [Tooltip("Interaction key")]
        [SerializeField] private KeyCode interactionKey = KeyCode.E;
        [Tooltip("UI key")]
        [SerializeField] private KeyCode UIKey = KeyCode.F;

        [Header("Object Following")]
        [Tooltip("Minimum speed of the lifted object")]
        [SerializeField] private float minSpeed = 0;
        [Tooltip("Maximum speed of the lifted object")]
        [SerializeField] private float maxSpeed = 3000f;

        [Header("UI")]
        [Tooltip("Background object for text")]
        [SerializeField] private Image uiPanel;
        [Tooltip("Text holder")]
        [SerializeField] private Text panelText;
        [Tooltip("Text when an object can be lifted")]
        [SerializeField] private string itemPickUpText;
        [Tooltip("Text when an object can be drop")]
        [SerializeField] private string itemDropText;
        [Tooltip("Text when an interactive object can be opened")]
        [SerializeField] private string interactiveOpenText;
        [Tooltip("Text when an interactive object can be closed")]
        [SerializeField] private string interactiveCloseText;


        //Private variables.
        private PhysicsObject _physicsObject;
        private PhysicsObject _currentlyPickedUpObject;
        private PhysicsObject _lookObject;
        private Quaternion _lookRotation;
        private Vector3 _raycastPosition;
        private Rigidbody _pickupRigidBody;
        private Interactive _lookInteractive;
        private float _currentSpeed = 0f;
        private float _currentDistance = 0f;
        private CharacterController _characterController;
        [HideInInspector] public bool UIContacted1 = false;
        [HideInInspector] public bool UIContacted2 = false;
        [HideInInspector] public bool UIContacted3 = false;
        [HideInInspector] public bool UIContacted4 = false;
        [HideInInspector] public string SceneName;


        private void Start()
        {
            mainCamera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Interactions();
            LegCheck();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneName = "Hiphop1";
                GameObject.Find("HiphopAlbum1").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneName = "Hiphop2";
                GameObject.Find("HiphopAlbum2").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneName = "Pop1";
                GameObject.Find("PopAlbum1").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneName = "Pop2";
                GameObject.Find("PopAlbum2").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneName = "Acoustic1";
                GameObject.Find("AcousticAlbum1").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneName = "Acoustic2";
                GameObject.Find("AcousticAlbum2").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SceneName = "Christmas1";
                GameObject.Find("ChristmasAlbum1").GetComponent<UiManager>().ChangeScene(SceneName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SceneName = "Christmas2";
                GameObject.Find("ChristmasAlbum2").GetComponent<UiManager>().ChangeScene(SceneName);
            }

        }

        //Determine which object we are now looking at, depending on the tag and component
        private void Interactions()
        {
            _raycastPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit interactionHit;
            if (Physics.Raycast(_raycastPosition, mainCamera.transform.forward,
                out interactionHit, interactionDistance, interactionLayer))
            {
                if (interactionHit.collider.CompareTag(itemTag))
                {
                    _lookObject = interactionHit.collider.GetComponentInChildren<PhysicsObject>();
                    ShowItemUI();
                }
                else if (interactionHit.collider.CompareTag(interactiveTag))
                {
                    _lookInteractive = interactionHit.collider.gameObject.GetComponentInChildren<Interactive>();
                    ShowInteractiveUI();
                    if (Input.GetKeyDown(interactionKey))
                    {
                        _lookInteractive.PlayInteractiveAnimation();
                    }
                }
                else if (interactionHit.collider.CompareTag(Return))
                {
                    uiPanel.gameObject.SetActive(true);
                    panelText.text = interactiveOpenText;
                    if (Input.GetKeyDown(UIKey))
                    {
                        GameObject.Find("ReturnToMainUI").GetComponent<ReturnToMain>().ChangeScene();
                    }
                }

                else if (interactionHit.collider.CompareTag(UITag1))
                {
                    uiPanel.gameObject.SetActive(true);
                    panelText.text = interactiveCloseText;
                    UIContacted1 = true;
                    UIContacted2 = false;
                    UIContacted3 = false;
                    UIContacted4 = false;
                }
                else if (interactionHit.collider.CompareTag(UITag2))
                {
                    ShowInteractiveUI();
                    UIContacted2 = true;
                    UIContacted1 = false;
                    UIContacted3 = false;
                    UIContacted4 = false;
                }
                else if (interactionHit.collider.CompareTag(UITag3))
                {
                    ShowInteractiveUI();
                    UIContacted3 = true;
                    UIContacted1 = false;
                    UIContacted2 = false;
                    UIContacted4 = false;
                }

            }
            else
            {
                _lookInteractive = null;
                _lookObject = null;
                UIContacted1 = false;
                UIContacted2 = false;
                UIContacted3 = false;
                UIContacted4 = false;
                uiPanel.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(interactionKey))
            {
                if (_currentlyPickedUpObject == null)
                {
                    if (_lookObject != null)
                    {
                        PickUpObject();
                    }
                }
                else
                {
                    BreakConnection();
                }
            }
        }

        //Disconnects from the object when the player attempts to step on the object, prevents flight on the object
        private void LegCheck()
        {
            Vector3 spherePosition = _characterController.center + transform.position;
            RaycastHit legCheck;
            if (Physics.SphereCast(spherePosition, 0.3f, Vector3.down, out legCheck, 2.0f))
            {
                if (legCheck.collider.CompareTag(itemTag))
                {
                    BreakConnection();
                }
            }
        }

        //Velocity movement toward pickup parent
        private void FixedUpdate()
        {
            if (_currentlyPickedUpObject != null)
            {
                _currentDistance = Vector3.Distance(pickupParent.position, _pickupRigidBody.position);
                _currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, _currentDistance / interactionDistance);
                _currentSpeed *= Time.fixedDeltaTime;
                Vector3 direction = pickupParent.position - _pickupRigidBody.position;
                _pickupRigidBody.velocity = direction.normalized * _currentSpeed;
            }
        }

        //Picking up an looking object
        public void PickUpObject()
        {
            _physicsObject = _lookObject.GetComponentInChildren<PhysicsObject>();
            _currentlyPickedUpObject = _lookObject;
            _lookRotation = _currentlyPickedUpObject.transform.rotation;
            _pickupRigidBody = _currentlyPickedUpObject.GetComponent<Rigidbody>();
            _pickupRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            _pickupRigidBody.transform.rotation = _lookRotation;
            _physicsObject.playerInteraction = this;
            StartCoroutine(_physicsObject.PickUp());
        }

        //Release the object
        public void BreakConnection()
        {
            if (_currentlyPickedUpObject)
            {
                _pickupRigidBody.constraints = RigidbodyConstraints.None;
                _currentlyPickedUpObject = null;
                _physicsObject.pickedUp = false;
                _currentDistance = 0;
            }
        }

        //Show interface elements when hovering over an object
        private void ShowInteractiveUI()
        {
            uiPanel.gameObject.SetActive(true);

            if (_lookInteractive.interactiveObjectOpen)
            {
                panelText.text = interactiveCloseText;
            }
            else
            {
                panelText.text = interactiveOpenText;
            }
        }

        private void ShowItemUI()
        {
            uiPanel.gameObject.SetActive(true);

            if (_currentlyPickedUpObject == null)
            {
                panelText.text = itemPickUpText;
            }
            else if (_currentlyPickedUpObject != null)
            {
                panelText.text = itemDropText;
            }

        }
    }
}