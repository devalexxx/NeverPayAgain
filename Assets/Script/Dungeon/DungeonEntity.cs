using UnityEngine;

public class DungeonEntity : MonoBehaviour
{
    public DungeonInstance Instance;

    private GameObject _mainCamera;
    private GameObject _camera;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");

        _camera = Instantiate(_mainCamera, Instance.PlayerEntity.transform);
        _camera.transform.position = new Vector3(6, 8, -8);
        _camera.transform.LookAt(Instance.PlayerEntity.transform);
        _mainCamera.SetActive(false);

        _camera.tag = "MainCamera";
        _camera.SetActive(true);
    }

    private void OnDestroy()
    {
        _mainCamera.tag = "MainCamera";
        _mainCamera.SetActive(true);
    }
}