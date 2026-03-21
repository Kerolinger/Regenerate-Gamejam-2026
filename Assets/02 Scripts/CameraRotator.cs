using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    //<>
    [SerializeField] private float speed;
    [SerializeField] private Transform st01_camera;
    private bool mouseInputBlocked;


    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        if (mouseInputBlocked)
            return;

        var mouseInput = Mouse.current.position.ReadValue();

        Debug.Log(st01_camera.eulerAngles.z);
        //if mouse is on the left side, move to the left
        if (Screen.width / 2 > mouseInput.x && st01_camera.eulerAngles.y > 45f)
            st01_camera.eulerAngles -= speed * new Vector3(0, mouseInput.y, 0) * Time.deltaTime;
        else if (Screen.width / 2 < mouseInput.x && st01_camera.eulerAngles.y < 170f)
            st01_camera.eulerAngles += speed * new Vector3(0, mouseInput.y, 0) *Time.deltaTime;
    }

    public void ChangeMouseRotation(bool willEnabled)
    {
        mouseInputBlocked = willEnabled;

        
    }
}
