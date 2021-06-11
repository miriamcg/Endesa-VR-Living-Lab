using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using TMPro;

public class SelectPoints : MonoBehaviour
{
    enum BallsType { Red, Yellow, Green, Blue, Orange, None }

    RectTransform canvasRectTransform;
    RectTransform imageRectTransform;

    BallsType currentBallSelected;

    [Header("Image to check balls")]
    public Texture2D mapWithBalls;

    [Header("Images when pulse color ball")]
    Image[] images;
    int currentImage;

    [Header("Messages")]
    public Image messageImage;
    public TextMeshProUGUI messageText;

    [Header("Other")]
    public float colorDesviation = 0.075f;
    public int pixelRadiusCheck = 2;
    public float transparencyValue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        imageRectTransform = this.GetComponent<RectTransform>();
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        mapWithBalls = DuplicateTexture(mapWithBalls);


        // Guardamos los paneles con imagenes
        images = new Image[5];
        images[0] = this.gameObject.transform.Find("Red Balls").GetComponentInChildren<Image>();
        images[1] = this.gameObject.transform.Find("Yellow Balls").GetComponentInChildren<Image>();
        images[2] = this.gameObject.transform.Find("Green Balls").GetComponentInChildren<Image>();
        images[3] = this.gameObject.transform.Find("Blue Balls").GetComponentInChildren<Image>();
        images[4] = this.gameObject.transform.Find("Orange Balls").GetComponentInChildren<Image>();

        // La transparencia por defecto es 0.5
        foreach (Image image in images)
        {
            image.color = new Color(1f, 1f, 1f, transparencyValue);
        }

        // Inicialmente no hemos seleccionado ninguna
        messageText.gameObject.SetActive(false);
        messageImage.gameObject.SetActive(false);
        currentImage = -1;
    }

    // Update is called once per frame
    void Update()
    {

        // Obtener las coordenadas del ratón
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRectTransform,
            Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localpoint);
        Vector2 norm = Rect.PointToNormalized(imageRectTransform.rect, localpoint);


        // Comprobamos si el puntero se encontraba dentro de la imagen
        if (norm.x != 100 && norm.y != 100 && norm.x != 0 && norm.y != 0)
        {
            Debug.Log("In da canvas");
            if (AreaColorCheck(norm))
            {
                messageImage.gameObject.SetActive(false);
                messageText.gameObject.SetActive(false);
                // Si teniamos una bola seleccionada la quitamos
                if (currentImage != -1) images[currentImage].color = new Color(1f, 1f, 1f, transparencyValue);
                currentImage = (int)currentBallSelected;

                // Cambiamos transparencia
                images[currentImage].color = new Color(1f, 1f, 1f, 1f);

                ShowMessage();
            }
            else if (currentImage != -1)
            {
                messageImage.gameObject.SetActive(false);
                messageText.gameObject.SetActive(false);
                images[currentImage].color = new Color(1f, 1f, 1f, transparencyValue);
                currentImage = -1;
            }
        }
    }

    private void ShowMessage()
    {
        Vector2 localpoint;

        // Activamos el mensaje
        messageImage.gameObject.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform,
            Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localpoint);

        // Mostramos un mensaje distinto dependiendo del color
        if (currentBallSelected == BallsType.Red)
        {
            messageText.text = "Centro transformación sensorizado con Circutor";
            messageImage.color = new Color(1f, 0.8f, 0.8f);
            messageImage.rectTransform.localPosition = new Vector2(15.33f, 3.42f);
        }
        else if (currentBallSelected == BallsType.Blue)
        {
            messageText.text = "Centro transformación sensorizado con Ormazábal";
            messageImage.color = new Color(0.8f, 0.8f, 1f);
            messageImage.rectTransform.localPosition = new Vector2(18.24f, 12f);
        }
        else if (currentBallSelected == BallsType.Green)
        {
            messageText.text = "Centro transformación sensorizado con Merytronic";
            messageImage.color = new Color(0.8f, 1f, 0.8f);
            messageImage.rectTransform.localPosition = new Vector2(18.24f, 12f);
        }
        else if (currentBallSelected == BallsType.Orange)
        {
            messageText.text = "Centro transformación sensorizado con Pronutec";
            messageImage.color = new Color(1f, 0.8f, 0.6f);
            messageImage.rectTransform.localPosition = new Vector2(2.02f, 10.15f);
        }
        else if (currentBallSelected == BallsType.Yellow)
        {
            messageText.text = "Centro transformación sensorizado con Ormazábal (Anillos)";
            messageImage.color = new Color(1f, 1f, 0.7f);
            messageImage.rectTransform.localPosition = new Vector2(-7.1f, 5.06f);
        }

        messageImage.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
    }

    // Buscamos los colores dentro de un area alrededor de un punto (norm)
    private bool AreaColorCheck(Vector2 norm)
    {
        for (int i = (int)(norm.x * mapWithBalls.width) - pixelRadiusCheck; i < (int)(norm.x * mapWithBalls.width) + pixelRadiusCheck; i++)
        {
            for (int j = (int)(norm.y * mapWithBalls.height) - pixelRadiusCheck; j < (int)(norm.y * mapWithBalls.height) + pixelRadiusCheck; j++)
            {
                currentBallSelected = ColorCheck(mapWithBalls.GetPixel(i, j));
                Debug.Log("Ball selected " + currentBallSelected);
                if (currentBallSelected != BallsType.None) return true;
            }
        }

        return false;
    }


    // ColorCheck se utiliza para comprobar el color que se pasa como parámetro
    // Si se encuentra dentro de una serie de rangos será uno de los colores que buscamos
    private BallsType ColorCheck(Color col)
    {
        float red = col.r * 100;
        float green = col.g * 10;
        float blue = col.b * 10;

        //Debug.Log("Red " + red + " green " + green + " blue " + blue);

        if (red <= 0.78)
        {
            if (green > 0.5 - colorDesviation && green < 0.5 + colorDesviation && blue > 0.5)
            {
                return BallsType.Orange;
            }
            else if (green > 0.1 - colorDesviation / 2 && green < 0.5 + colorDesviation && blue > 0.5 - colorDesviation / 2 && blue < 1.5f + colorDesviation)
            {
                return BallsType.Yellow;
            }
            else if (blue > 1 - colorDesviation)
            {
                return BallsType.Blue;
            }
            else if (green > 0.5 - colorDesviation && blue > 1 - colorDesviation) {
                return BallsType.Green;
            }
        }
        else if (red > 0.78) {
            if (green > 1 - colorDesviation) {
                return BallsType.Red;
            }
        }
        return BallsType.None;
    }


    // Duplicamos la textura para poder acceder a los pixeles y comprobar si estamos encima de una bola
    Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
