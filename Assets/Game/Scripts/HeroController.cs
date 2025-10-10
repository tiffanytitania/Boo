using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HeroController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float jumpHeight = 2f;
    public float gravity = -12f;
    public Transform cameraTransform; // referensi kamera di dalam Hero
    public float lookSpeed = 2f;
    public float lookXLimit = 80f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float rotationX = 0;
    private bool isLookingBack = false;
    public float lookYLimit = 90f; // batas rotasi kiri-kanan
    private float totalRotationY = 0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor di tengah layar (optional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ===== Gerakan dasar =====
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // ===== Rotasi Hero (horizontal) =====
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        // Tambahkan batas rotasi kiri-kanan
        totalRotationY += mouseX;
        totalRotationY = Mathf.Clamp(totalRotationY, -lookYLimit, lookYLimit);
        transform.localRotation = Quaternion.Euler(0, totalRotationY, 0);


        // ===== Rotasi Kamera (vertikal) =====
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // ===== Look Back dengan Q =====
        if (Input.GetKeyDown(KeyCode.Q))
            isLookingBack = true;
        else if (Input.GetKeyUp(KeyCode.Q))
            isLookingBack = false;

        if (isLookingBack)
        {
            // Kamera menghadap ke belakang karakter
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 180f, 0);
        }
        else
        {
            // Kamera normal (menghadap depan)
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }

        // ===== Loncat =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // ===== Gravitasi =====
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
