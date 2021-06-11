using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Endesa.Showroom.Utils {

    public class OutlineController : MonoBehaviour
    {
        [Header("List of Outlines")]
        public GameObject[] outlineObjects;

        public void ActiveOutline() {
            for (int i = 0; i < outlineObjects.Length; i++) {
                bool enabled = outlineObjects[i].GetComponent<Outline>().enabled;
                if (!enabled) {
                    outlineObjects[i].GetComponent<Outline>().enabled = true;
                }
            }
        }
    }
}
