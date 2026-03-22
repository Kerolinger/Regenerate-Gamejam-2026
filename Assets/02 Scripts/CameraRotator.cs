using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    //<>

    [SerializeField] private float speed;
    [SerializeField] private Transform st01_camera;
    [SerializeField] private int maxRotAngle =45;
    private bool mouseInputBlocked;

    private float cameraYRotationStartingPosition;
    public static CameraRotator instance;
    // Update is called once per frame

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    private void Start()
    {
        cameraYRotationStartingPosition = st01_camera.eulerAngles.y;
    }
    void Update()
    {
        if (mouseInputBlocked)
            return;

        var mouseInput = Mouse.current.position.ReadValue();

        //if mouse is on the left side, move to the left
        if (Screen.width / 7 > mouseInput.x && st01_camera.eulerAngles.y > cameraYRotationStartingPosition -45)
            st01_camera.eulerAngles -= speed * new Vector3(0, mouseInput.y, 0) * Time.deltaTime;
        else if (Screen.width / 7 *6  < mouseInput.x && st01_camera.eulerAngles.y < cameraYRotationStartingPosition + 45)
            st01_camera.eulerAngles += speed * new Vector3(0, mouseInput.y, 0) *Time.deltaTime;
    }

    public void ChangeMouseRotation(bool willEnabled)
    {
        mouseInputBlocked = willEnabled;
        
    }
}
