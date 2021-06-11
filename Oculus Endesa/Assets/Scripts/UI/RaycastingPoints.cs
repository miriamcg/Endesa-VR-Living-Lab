using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

namespace Endesa.Showroom.UI {

    public class RaycastingPoints : MonoBehaviour
    {
        [Header("XR Controller")]
        public XRRayInteractor leftInteractor;
        public XRRayInteractor RightInteractor;

        [Header("Message Components")]
        public Image messageImage;
        public TextMeshProUGUI messageText;
        public TextMeshProUGUI debugText;

        private RaycastHit leftHit;
        private RaycastHit rightHit;

        private string[] colliderTags;
        private bool getTag;

        void Start() {
            colliderTags = new string[2];
            getTag = false;
        }

        void Update()
        {
            bool leftRay = leftInteractor.GetCurrentRaycastHit(out leftHit);
            bool rightRay = RightInteractor.GetCurrentRaycastHit(out rightHit);

            //colliderTags[0] = leftHit.collider.tag;
            //colliderTags[1] = rightHit.collider.tag;

            Debug.Log(leftRay);
            debugText.text = colliderTags[0] + colliderTags[1];

            //ShowMessage(colliderTags);
        }

        private void ShowMessage(string[] tags) {

            for(int i = 0; i < tags.Length; i++)
            {
                SetMessage(tags[i]);
                if (getTag) {
                    messageImage.gameObject.SetActive(true);
                    messageText.gameObject.SetActive(true);
                }
            }
        }

        private void SetMessage(string tag) {

            switch (tag) {
                case "Red":
                    messageText.text = "Centro transformación sensorizado con Circutor";
                    messageImage.color = new Color(1f, 0.8f, 0.8f);
                    messageImage.rectTransform.localPosition = new Vector2(15.33f, 3.42f);
                    getTag = true;
                    break;
                case "Yellow":
                    messageText.text = "Centro transformación sensorizado con Ormazábal (Anillos)";
                    messageImage.color = new Color(1f, 1f, 0.7f);
                    messageImage.rectTransform.localPosition = new Vector2(-7.1f, 5.06f);
                    getTag = true;
                    break;
                case "Green":
                    messageText.text = "Centro transformación sensorizado con Merytronic";
                    messageImage.color = new Color(0.8f, 1f, 0.8f);
                    messageImage.rectTransform.localPosition = new Vector2(18.24f, 12f);
                    getTag = true;
                    break;
                case "Blue":
                    messageText.text = "Centro transformación sensorizado con Ormazábal";
                    messageImage.color = new Color(0.8f, 0.8f, 1f);
                    messageImage.rectTransform.localPosition = new Vector2(18.24f, 12f);
                    getTag = true;
                    break;
                case "Orange":
                    messageText.text = "Centro transformación sensorizado con Pronutec";
                    messageImage.color = new Color(1f, 0.8f, 0.6f);
                    messageImage.rectTransform.localPosition = new Vector2(2.02f, 10.15f);
                    getTag = true;
                    break;
                default:
                    messageImage.gameObject.SetActive(false);
                    messageText.gameObject.SetActive(false);
                    getTag = false;
                    break;
            }
        }
    }
}
