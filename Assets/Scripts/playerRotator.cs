using UnityEngine.InputSystem;
using UnityEngine;

public class playerRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Transform cameraTransform;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        if (((int)input.y) > 0)
        {
            transform.Rotate(new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime * rotationSpeed);
        }
        if (((int)input.y) < 0)
        {
            transform.Rotate(new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime * -rotationSpeed);
        }
        if (((int)input.x) < 0)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 2.0f) * Time.deltaTime * rotationSpeed);
        }
        if (((int)input.x) > 0)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 2.0f) * Time.deltaTime * -rotationSpeed);
        }

    }
}
