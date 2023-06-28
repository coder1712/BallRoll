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

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private PlayerInput playerInput;
    private InputAction jumpAction;

    private Transform cameraTransform;

    private bool grounded = false;
    private float movementX;
    private float movementY;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        jumpAction = playerInput.actions["Jump"];

        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        cameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            // Enemy Damage
            count += 5;
            SetCountText();
        }
        if (collision.gameObject.CompareTag("EnemyBody"))
        {
            //Player Damage
            count = 0;
            SetCountText();
        }
    }
    // Track Score
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

}
