using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerVisualizer : MonoBehaviour
{
    public InputDeviceCharacteristics characteristics;
    public List<GameObject> controllerPrefabs;

    private InputDevice targetDevice;
    private GameObject spawnedController;

    // Start is called before the first frame update
    void Start() {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count > 0) {

            targetDevice = devices[0];

            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            if (prefab) {
                spawnedController = Instantiate(prefab, transform);
            } else {
                Debug.Log("Controller not found");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }

    void Update() 
    {
    }
}
