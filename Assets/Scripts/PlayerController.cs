using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float gravityScale = 9.8f, 
                        speedScale = 5f,
                        jumpForce = 8f,
                        turnSpeed = 90f;
    private CharacterController controller;
    [SerializeField] private Camera goPro;
    private float verticalSpeed = 0f, mouseX = 0f, mouseY = 0f, 
                        currentAngle = 0f;
    
    [SerializeField] private GameObject particleObj;
    // private GameObject currentEquipItem;
    public const string EQUIP_NOT_SELECTED_TEXT = "EquipeNotSelected";
    [HideInInspector]public string itemYourCanEquipmqnt = EQUIP_NOT_SELECTED_TEXT;
    [SerializeField] private GameObject[] equipebleItem;
    private GameObject cureEquipedItem;
    private float hitScaleSpeed = 15f;
    private float hitLastTime = 0f;
    private RaycastHit hit;
    public List<ItemData> inventoryItems, currentChestItems;
    private Transform itemParent;
    private bool canMove;
    private InventoryManager inventoryManager;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        controller = GetComponent<CharacterController>();
        inventoryManager = 
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        itemParent = GameObject.Find("InventoryContent").transform;
        inventoryManager.CreateItem(0, inventoryItems);
    }
    void Update()
    {
        if(canMove)
        {
            RotateCharacter();
            MoveCharacter();
            if (Physics.Raycast(goPro.transform.position, goPro.transform.forward, out hit, 5f))
            {
                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }                
            }
        }
       if (Input.GetKeyDown(KeyCode.E))
       {
            if (!inventoryManager.inventoryPanel.activeSelf)
            {
                OpenInventory();
            }
            else
            {
                ClosePanels();
            }
           
       }
       else  if(Input.GetKeyDown(KeyCode.R) && inventoryManager.inventoryPanel.activeSelf && itemYourCanEquipmqnt != EQUIP_NOT_SELECTED_TEXT) {
            EquipItem(itemYourCanEquipmqnt);
       }
       
    }

     private void ObjectInteraction(GameObject tempObj)
    {
        switch(tempObj.tag) 
        {
            case "Block":
                Dig(tempObj.GetComponent<Block>());
                break;
            case "Enemy":
                break;
            case "Chest":
                currentChestItems = tempObj.GetComponent<Chest>().chestItems;
                OpenChest();
                break;
        }
    }
    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f,mouseX*turnSpeed*Time.deltaTime, 0f));
        currentAngle += mouseY*turnSpeed*Time.deltaTime* -1f;
        currentAngle = Mathf.Clamp(currentAngle, -60f, 60f);
        goPro.transform.localEulerAngles = new Vector3(currentAngle, 0f, 0f);
    }
    void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity)*speedScale;
        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }
           
        }
         verticalSpeed -= gravityScale*Time.deltaTime;
         velocity.y = verticalSpeed;
         controller.Move(velocity*Time.deltaTime);
    
    }
    

    private void Dig(Block block)
    {
        if(Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            cureEquipedItem.GetComponent<Animator>().SetTrigger("attack");
            hitLastTime = Time.time;
            Tool currToolInfo;
            if(cureEquipedItem.TryGetComponent<Tool>(out currToolInfo)){
                block.Health -= currToolInfo.damageToBlock;
            }else {
                block.Health =- 1;
            }
            
            GameObject go = Instantiate(particleObj, block.gameObject.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material = block.gameObject.GetComponent<MeshRenderer>().material;
            if (block.Health <= 0)
            {
                block.DestroyBehaviour();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.StartsWith("mini"))
        {
            inventoryManager.CreateItem(0, inventoryItems);
            Destroy(other.gameObject);
        }
    }

    void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;

        inventoryManager.inventoryPanel.SetActive(true);
        if(inventoryItems.Count > 0)
        {
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(inventoryItems[i], 
                                                itemParent, 
                                                inventoryManager.inventorySlots);
            }
        }
    }
    void OpenChest()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;
        if(!inventoryManager.chestPanel.activeSelf)
        {
            inventoryManager.chestPanel.SetActive(true);
            Transform itemParent = GameObject.Find("ChestContent").transform;
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(currentChestItems[i],
                                                itemParent,
                                                inventoryManager.currentChestSlots);
            }
        }
    }
    void ClosePanels()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        foreach (GameObject slot in inventoryManager.currentChestSlots)
        {
            Destroy(slot);
        }
        foreach (GameObject slot in inventoryManager.inventorySlots)
        {
            Destroy(slot);
        }
        inventoryManager.currentChestSlots.Clear();
        inventoryManager.inventorySlots.Clear();

        inventoryManager.inventoryPanel.SetActive(false);
        inventoryManager.chestPanel.SetActive(false);
    }



    void EquipItem(string toolName){
        foreach(GameObject tool in equipebleItem){
            if(tool.name == toolName){
                tool.SetActive(true);
                cureEquipedItem = tool;
                toolName = EQUIP_NOT_SELECTED_TEXT;
            }
            else {
                tool.SetActive(false);
            }
        }
    }
   
   void Start (){
        EquipItem("Pickaxe");
   }
}
