using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Endesa.Showroom.Utils {

    public class ImagesController : MonoBehaviour
    {
        public GameObject[] images;

        public void DesactivateImages()
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].activeSelf == true)
                {
                    images[i].SetActive(false);
                }
            }
        }
    }
}
