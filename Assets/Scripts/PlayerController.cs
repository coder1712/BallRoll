using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 100.0f;
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletMissDistance = 25f;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private Transform cameraTransform;

    private bool grounded = false;
    private float movementX;
    private float movementY;

    public Health health;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int score;
    public int key;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        score = 0;
        key = 0;
        SetCountText();
        winTextObject.SetActive(false);
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }
    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        playerBulletController bulletController = bullet.GetComponent<playerBulletController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = false;
        }
    }

    private void Update()
    {
        isGrounded();
        if (jumpAction.triggered && grounded)
        {
            Jump();
        }

    }
    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        rb.AddForce(move * playerSpeed);
        rb.freezeRotation = true;
        Rotation();

    }

    void Rotation()
    {
        Quaternion targetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
    }

    private void isGrounded()
    {
        float extraHeight = 0.1f;
        if (Physics.Raycast(sphereCollider.bounds.center, Vector3.down, sphereCollider.bounds.extents.y + extraHeight))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // PowerUps and collectibles
        if (other.gameObject.CompareTag("GoldBar"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            key++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBody") || collision.gameObject.CompareTag("EnemyFire"))
        {
            //Player Damage
            health.health -= 1;
            score = 0;
            SetCountText();
        }
    }
    // Track Score
    void SetCountText()
    {
        countText.text = " " + score.ToString();
    }

}
