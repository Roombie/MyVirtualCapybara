using UnityEngine;
using UnityEngine.InputSystem;

public class CapybaraController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update the position towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    // Method to handle both mouse click and touchscreen tap
    public void OnClickOrTapMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputPosition = Vector2.zero;

            // Check if it's a touchscreen tap
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.isInProgress)
            {
                inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            // Check if it's a mouse click
            else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            {
                inputPosition = Mouse.current.position.ReadValue();
            }

            MoveToPosition(inputPosition);
        }
    }

    // Move to the specified position
    private void MoveToPosition(Vector2 inputPosition)
    {
        // Convert the input position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 10f));

        // Set the target to the clicked/tapped position
        target = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
