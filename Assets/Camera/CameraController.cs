using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f;
    float curSpeed;

    [Space]
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;

    [Space]
    [SerializeField] float minZ = -18;
    [SerializeField] float maxZ = 8;

    [Space]
    [SerializeField] float minX = -4;
    [SerializeField] float maxX = 42;

    [SerializeField] bool clamp = true;

    [Space][SerializeField] bool shift = true;
    [Range(0.0f, 10.0f)][SerializeField] float shiftSpeed = 2f;

    float horizontal;
    float forward;
    float vertical;

    float xClamped;
    float yClamped;
    float zClamped;

    void FixedUpdate()
    {
        // TODO: Turn off camera controls when gameover/pause
        //if (GameManager.GameIsOver)
        //{
        //    this.enabled = false;
        //    return;
        //}

        curSpeed = panSpeed;

        if (shift && Input.GetKey(KeyCode.LeftShift))
            curSpeed = panSpeed * shiftSpeed;

        horizontal = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical");
        vertical = Input.GetAxis("UpDown");

        Vector3 horizontalMovement = curSpeed * horizontal * Time.deltaTime * Vector3.right;
        Vector3 forwardMovement = curSpeed * forward * Time.deltaTime * Vector3.forward;
        Vector3 verticalMovement = curSpeed * vertical * Time.deltaTime * Vector3.up;

        transform.Translate(horizontalMovement, Space.Self);
        transform.Translate(forwardMovement, Space.Self);
        transform.Translate(verticalMovement, Space.Self);

        if (clamp)
        {
            xClamped = Mathf.Clamp(transform.position.x, minX, maxX);
            yClamped = Mathf.Clamp(transform.position.y, minY, maxY);
            zClamped = Mathf.Clamp(transform.position.z, minZ, maxZ);
            transform.position = new Vector3(xClamped, yClamped, zClamped);
        }

        if (Input.GetMouseButton(1))
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
