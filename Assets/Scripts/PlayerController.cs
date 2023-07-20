using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 15.0f;
    [SerializeField]
    private float jumpForce = 5.0f;
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
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        score = 0;
        key = 0;
        SetCountText();
        winTextObject.SetActive(false);
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
            bulletController.hit = true;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void Update()
    {
        isGrounded();
        if (jumpAction.triggered && grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = movement.x * cameraTransform.right.normalized + movement.z * cameraTransform.forward.normalized;
        movement.y = 0.0f;
        rb.AddForce(movement * playerSpeed);
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
