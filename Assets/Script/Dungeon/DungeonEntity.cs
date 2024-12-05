using UnityEngine;

// Class responsible for managing the camera and camera behavior within the dungeon instance.
public class DungeonEntity : MonoBehaviour
{
    public DungeonInstance Instance;    // Reference to the DungeonInstance this entity represents.

    private GameObject _mainCamera; // The main camera in the scene.
    private GameObject _camera;     // The camera specifically created for the dungeon instance.

    private void Awake()
    {
        // Find the main camera in the scene.
        _mainCamera = GameObject.FindWithTag("MainCamera");

        // Get the player entity (assumes player is the first child).
        var playerEntity = transform.GetChild(0);

        // Instantiate a copy of the main camera as the dungeon camera.
        _camera = Instantiate(_mainCamera, playerEntity.transform);

        // Set a new position for the camera.
        _camera.transform.position = new Vector3(6, 7, -6);

        // Make the camera look at the player entity.
        _camera.transform.LookAt(playerEntity.transform.position + new Vector3(0, 1, 0));

        // Disable the main camera to use the new camera.
        _mainCamera.SetActive(false);

        // Set the new camera's tag to "MainCamera" to allow UI Raycasting
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
