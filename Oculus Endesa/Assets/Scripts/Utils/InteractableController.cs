using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Endesa.Showroom.Utils {

    public class InteractableController : MonoBehaviour
    {
        public GameObject[] interactableObjects;

        public void ActivateInteractable() {
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                bool enabled = interactableObjects[i].GetComponent<XRGrabInteractable>().enabled;
                if (!enabled)
                {
                    interactableObjects[i].GetComponent<XRGrabInteractable>().enabled = true;
                }
            }
        }
    }
}
