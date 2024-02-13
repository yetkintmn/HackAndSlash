using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
    private Camera _camera;
    private void Start()
    {
        _camera = FindObjectOfType<OverlayCamera>().GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = _camera;       
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }
}