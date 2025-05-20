using System.Collections.Generic;
using UnityEngine;

public class Playser_Tux_Controller : MonoBehaviour
{
    private const float gravityScale = 9.8f,
                        speedScale = 5f,
                        jumpForce = 8f,
                        turnSpeed = 90f;

    private CharacterController controller;
    [SerializeField] private Camera goPro;

    private float verticalSpeed = 0f, mouseX = 0f, mouseY = 0f,
                        currentAngle = 0f;

    public const string EQUIP_NOT_SELECTED_TEXT = "EquipeNotSelected";
    [HideInInspector] public string itemYourCanEquipmqnt = EQUIP_NOT_SELECTED_TEXT;

    [SerializeField] private GameObject[] equipebleItem;
    private GameObject cureEquipedItem;

    private RaycastHit hit;

    public List<ItemData> inventoryItems, currentChestItems;
    private Transform itemParent;
    private bool canMove;
    private InventoryManager inventoryManager;

    [SerializeField] private Transform cameraHolder;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;

        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found on " + gameObject.name);
        }
    }

    private float yaw = 0f;
    private float pitch = 0f;
    
    void Update()
    {
        if (!canMove || controller == null) return;
    
        RotateCharacter();
        MoveCharacter();
    }
    
    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    
        yaw += mouseX * turnSpeed * Time.deltaTime;
        pitch -= mouseY * turnSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -120f, 120f);
    
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        goPro.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }


    void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speedScale;

        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }
        }

        verticalSpeed -= gravityScale * Time.deltaTime;
        velocity.y = verticalSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}
