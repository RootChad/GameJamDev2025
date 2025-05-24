using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target object to orbit around (your cube world)
    public float distance = 5.0f; // Default distance from the target
    public float xSpeed = 120.0f; // Horizontal rotation speed
    public float ySpeed = 120.0f; // Vertical rotation speed

    public float yMinLimit = -90f; // Minimum vertical angle (looking straight down)
    public float yMaxLimit = 90f;  // Maximum vertical angle (looking straight up)

    public float distanceMin = .5f; // Minimum zoom distance
    public float distanceMax = 15f; // Maximum zoom distance

    private float x = 0.0f;
    private float y = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        // if (GetComponent<Rigidbody>())
        // {
        //     GetComponent<Rigidbody>().freezeRotation = true;
        // }
    }

    // LateUpdate is called after all Update functions have been called.
    // This is good for camera updates, to ensure the target has moved before the camera updates.
    void LateUpdate()
    {
        if (target)
        {
            // Check for right mouse button press to orbit
            if (Input.GetMouseButton(1)) // 1 is for the right mouse button
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            // Raycast to check for obstructions (optional, but good for complex scenes)
            // RaycastHit hit;
            // if (Physics.Linecast(target.position, transform.position, out hit))
            // {
            //     distance -= hit.distance;
            // }

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
