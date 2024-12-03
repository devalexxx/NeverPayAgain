using UnityEngine;

public class DungeonEntity : MonoBehaviour
{
    public DungeonInstance Instance;

    private GameObject _mainCamera;
    private GameObject _camera;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");

        var playerEntity = transform.GetChild(0);
        _camera = Instantiate(_mainCamera, playerEntity.transform);
        _camera.transform.position = new Vector3(6, 7, -6);
        _camera.transform.LookAt(playerEntity.transform.position + new Vector3(0, 1, 0));
        _mainCamera.SetActive(false);

        _camera.tag = "MainCamera";
        _camera.SetActive(true);
    }

    private void OnDestroy()
    {
        if (_mainCamera != null)
        {
            _mainCamera.tag = "MainCamera";
            _mainCamera.SetActive(true);
        }
    }
}
