using UnityEngine;

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
