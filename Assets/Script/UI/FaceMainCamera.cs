using UnityEngine;

// This script makes the GameObject face the main camera at all times.
public class FaceMainCamera : MonoBehaviour
{
    private GameObject _camera;

    private void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(_camera.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
