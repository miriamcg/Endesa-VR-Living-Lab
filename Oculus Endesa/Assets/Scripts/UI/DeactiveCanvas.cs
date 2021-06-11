using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Endesa.Showroom.UI {

    public class DeactiveCanvas : MonoBehaviour
    {
        [Header("List of Canvas")]
        public GameObject[] canvas;

        public void DeactivedCanvas()
        {
            for (int i = 0; i < canvas.Length; i++)
            {

                if (canvas[i].activeSelf == true)
                {
                    canvas[i].SetActive(false);
                }
            }
        }
    }
}
