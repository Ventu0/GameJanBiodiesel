using UnityEngine;
using UnityEngine.InputSystem;

public class MenuCameraFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public float maxRotationX = 5f;
    public float maxRotationY = 8f;
    public float smoothSpeed = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        initialRotation = cameraTransform.localRotation;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float mouseX = (mousePosition.x / Screen.width) * 2f - 1f;
        float mouseY = (mousePosition.y / Screen.height) * 2f - 1f;

        float rotY = mouseX * maxRotationY;
        float rotX = -mouseY * maxRotationX;

        Quaternion targetRotation =
            initialRotation * Quaternion.Euler(rotX, rotY, 0f);

        cameraTransform.localRotation = Quaternion.Lerp(
            cameraTransform.localRotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}