using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

public class AvoidRaycast : MonoBehaviour
{
    public XRController LeftInteractor;
    public XRController RightInteractor;

    void Update()
    {
        if (!RaycastWorldUI()) Debug.Log("not ui");
    }

    bool RaycastWorldUI()
    {

        int counter = 0;

        if (LeftInteractor || RightInteractor)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            for (int i = 0; i < results.Count; i++) {
                if (results[i].gameObject.layer == LayerMask.NameToLayer("WorldUI")) {
                    results.RemoveAt(i);
                    i--;
                }
            }

            counter = results.Count;
        }

        return counter > 0;
    }
}
