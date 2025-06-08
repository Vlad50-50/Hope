using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Playser_Tux_Controller : MonoBehaviour
{
    public AudioClip clip;          // Назначь аудиоклип в инспекторе
    private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI HP_top_Text;

    [SerializeField] private int HP_top = 1000000;
    private float gravityScale = 9.8f,
                        speedScale = 15f,
                        jumpForce = 5f,
                        turnSpeed = 90f;

    private CharacterController controller;
    [SerializeField] private Camera goPro;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce = 250f;

    private float verticalSpeed = 0f, mouseX = 0f, mouseY = 0f,
                        currentAngle = 0f;

    public const string EQUIP_NOT_SELECTED_TEXT = "EquipeNotSelected";
    public int coins = 0;
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

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Если AudioSource нет — добавим автоматически
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        if (!canMove || controller == null) return;
        if (HP_top <= 0) GameOver();

        RotateCharacter();
        MoveCharacter();
        Fire();
        UpdateUI();
        VictoryMethod();
    }

    void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("GameOver");
    }

    void VictoryMethod()
    {
        if (coins >= 5)
        {
            SceneManager.LoadScene("Vicroty");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Collection"))
        {
            coins++;
            UpdateUI();
            Debug.Log("Collected" + coins);
        }
        else if (obj.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entered to enemy");
            HP_top = HP_top - 500;
        }
    }

    private void UpdateUI()
    {
        roundText.text = $"Coins:::{coins}";
        HP_top_Text.text = $"HP::: {HP_top}";
    }

    void MoveCharacter()
    {
        float currentSpeed = speedScale;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2f;
        }

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * currentSpeed;

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

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем камеру
            Camera cam = Camera.main;

            // Направление взгляда камеры
            Vector3 direction = cam.transform.forward;

            // Позиция перед камерой (можно подстроить под нужную высоту и отступ)
            Vector3 spawnPosition = cam.transform.position + cam.transform.forward * 1.4f + cam.transform.up * -0.1f;

            // Поворот снаряда — в направлении взгляда камеры
            Quaternion spawnRotation = Quaternion.LookRotation(direction);

            // Создаём снаряд
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Добавляем силу в направлении взгляда камеры
            rb.AddForce(direction * projectileForce, ForceMode.Impulse);
        }
    }

    public void PlaySound()
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Аудиоклип не назначен!");
        }
    }

}
