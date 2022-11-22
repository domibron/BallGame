using UnityEngine;

public class TestTouch : MonoBehaviour
{
    private InputManager inputManager;
    private Camera cameraMain;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPos, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPos.x, screenPos.y, cameraMain.nearClipPlane);
        Vector3 worldCords = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCords.z = 0;
        transform.position = worldCords;
    }
}
