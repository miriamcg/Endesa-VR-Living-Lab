using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{

    [SerializeField]
    private GameObject[] maps;

    public void ActivateLastMap() {
        ActivateMap();
    }

    private void ActivateMap() {
        for (int i = 0; i < maps.Length; i++) {
            if (maps[i].activeSelf) {
                switch (maps[i].name) {
                    case "Malaga":
                        maps[0].SetActive(true);
                        maps[i].SetActive(false);
                        break;
                    case "City":
                        maps[1].SetActive(true);
                        maps[i].SetActive(false);
                        break;
                    case "Showroom":
                        maps[1].SetActive(true);
                        maps[i].SetActive(false);
                        break;
                }
            }
        }
    }
}
