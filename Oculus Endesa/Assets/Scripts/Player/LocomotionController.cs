using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController LeftTeleport;
    public XRController RightTeleport;
    public InputHelpers.Button teleportActivation;
    public float activationThreshold = 0.1f;

    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    void Update()
    {
        if (LeftTeleport) 
        {
            LeftTeleport.gameObject.SetActive(EnableLeftTeleport && CheckActivate(LeftTeleport));
        }

        if (RightTeleport) {
            RightTeleport.gameObject.SetActive(EnableRightTeleport && CheckActivate(RightTeleport));
        }
    }

    public bool CheckActivate(XRController controller) 
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivation, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
