using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    public GameObject buttomZoom;
    public Animator blurImage;
    public GameObject showroomTitle;
    public GameObject balls;
    public TextMeshProUGUI malagaTitle;
    public Camera cam;

    public List<GameObject> ImageBalls;
    public GameObject Balls;

    bool initZoom, mostrarBoton;
    Vector2 pointTo, zoomScale;
    SelectPoints selectPoints;
    Vector2 finalOffset = new Vector2(0.28f, 0.16f);
    Vector3 buttonPos, textPos;
    Vector2 finalTextPos = new Vector2(-6f, 3f);


    // Start is called before the first frame update
    void Start()
    {
        initZoom = false;
        mostrarBoton = false;
        zoomScale = new Vector2(1f, 1f);
        selectPoints = GetComponent<SelectPoints>();

        // NUEVO AGUSTIN
        Material mat = GetComponent<Image>().material;
        Vector2 offset = new Vector2(0f,0f);
        mat.SetTextureOffset("_MainTex", offset);
        Vector2 newZoom = new Vector2(1f,1f);
        mat.SetTextureScale("_MainTex", newZoom);

        buttonPos = buttomZoom.GetComponent<RectTransform>().localPosition;
        textPos = malagaTitle.gameObject.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (initZoom && zoomScale.x > 0.2f)
        {
            Material mat = GetComponent<Image>().material;
            // hacemos zoom
            Debug.Log("Zooooom");
            zoomScale = zoomScale * 0.97f;
            mat.SetTextureScale("_MainTex", zoomScale);
            // desplazamos la imagen
            //Vector2 offset = pointTo*(1-zoomScale.x)/0.8f;

            // NUEVO 
            Vector2 offset = (1f-zoomScale.x)/0.8f * finalOffset;
            buttomZoom.GetComponent<RectTransform>().localPosition = new Vector3(buttonPos.x * (zoomScale.x-0.2f)/0.8f,
                buttonPos.y * (zoomScale.y-0.2f)/0.8f, buttonPos.z);
            malagaTitle.gameObject.GetComponent<RectTransform>().localPosition = new Vector3((textPos.x-finalTextPos.x)*(zoomScale.x-0.2f)/0.8f+finalTextPos.x,
                (textPos.y-finalTextPos.y)*(zoomScale.y-0.2f)/0.8f+finalTextPos.y, textPos.z);
            mat.SetTextureOffset("_MainTex", offset);
            mostrarBoton = true;
        }
        else if (mostrarBoton)
        {
            //Ajustar posisión del botón
            buttomZoom.GetComponentInChildren<Button>().GetComponent<RectTransform>().sizeDelta = new Vector2(1f,1f);
            //buttomZoom.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f, buttomZoom.GetComponent<RectTransform>().localPosition.z);
            buttomZoom.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f, buttonPos.z);
            malagaTitle.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(finalTextPos.x, finalTextPos.y, textPos.z);
            buttomZoom.SetActive(true);
            malagaTitle.gameObject.SetActive(true);
            mostrarBoton = false;
        }
    }

    public void Zoom()
    {
        if (!initZoom)
        {
            initZoom = true;
            //Coger el punto exacto dentro de la imagen
            RectTransform rt = this.GetComponent<RectTransform>();
            Vector2 localpoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt,
                Input.mousePosition, cam, out localpoint);
            Vector2 norm = 100.0f * Rect.PointToNormalized(rt.rect, localpoint);

            if (norm.x != 100 && norm.y != 100 && norm.x != 0 && norm.y != 0)
            {
                //Centrar la imagen respecto al punto optenido
                //float x = (norm.x + 10) / 100;
                float x = (norm.x + 30) / 100;
                float y = (norm.y) / 100;
                pointTo = new Vector2(x - 0.05f, y + 0.05f);
            }
            Material mat = GetComponent<Image>().material;
            mat.SetTextureScale("_MainTex", new Vector2(0.8f, 0.8f));
            buttomZoom.SetActive(false);
            malagaTitle.gameObject.SetActive(false);
        }
        else
        {
            buttomZoom.SetActive(false);
            malagaTitle.gameObject.SetActive(false);
            buttomZoom.GetComponentInChildren<Button>().enabled = false;
            balls.SetActive(true);
            blurImage.enabled = true;
            showroomTitle.SetActive(true);
            foreach (GameObject img in ImageBalls) {
                img.GetComponent<Animator>().enabled = true;
            }
            selectPoints.enabled = true;
            initZoom = false;
        }
        
    }

    private void OnApplicationQuit()
    {
        Material mat = GetComponent<Image>().material;
        mat.SetTextureScale("_MainTex", new Vector2(1f, 1f));
        mat.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
    }
}
