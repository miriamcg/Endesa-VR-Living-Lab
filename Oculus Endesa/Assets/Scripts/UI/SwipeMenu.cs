using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public RectTransform scrollingPanel;
    [Tooltip("Buttons")]
    public Button[] buttons;
    [Tooltip("Center display area")]
    public RectTransform center;
    //public int startButton;

    private int buttonDistance;
    private int minButton;
    private float panelSize;

    private float[] distance;
    private float[] distanceReposition;

    private void Start()
    {
        int buttonCount = buttons.Length;
        distance = new float[buttonCount];
        distanceReposition = new float[buttonCount];

        RectTransform buttonRect = buttons[1].GetComponent<RectTransform>();
        RectTransform buttonRect2 = buttons[0].GetComponent<RectTransform>();

        buttonDistance = (int)Mathf.Abs(buttonRect.anchoredPosition.x - buttonRect2.anchoredPosition.x);
        //panel.anchoredPosition = new Vector2((startButton - 1) * 320f, 0f);
    }

    private void Update()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            distanceReposition[i] = center.GetComponent<RectTransform>().position.x - buttons[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distanceReposition[i]);

            //reduce and disable buttons
            buttons[i].GetComponent<RectTransform>().transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            buttons[i].interactable = false;

            ScrollLeft(distanceReposition[i], buttons[i]);
            ScrollRight(distanceReposition[i], buttons[i]);
        }

        float minDistance = Mathf.Min(distance);

        MagnifyingButton(minDistance);
    }

    private void ScrollLeft(float distanceRepos, Button button)
    {
        if (distanceRepos < -scrollingPanel.rect.width + 10f)
        {
            float curX = button.GetComponent<RectTransform>().anchoredPosition.x;
            float curY = button.GetComponent<RectTransform>().anchoredPosition.y;

            Vector2 newAnchoredPosition = new Vector2(curX - (buttons.Length * buttonDistance), curY);
            button.GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
        }
    }

    private void ScrollRight(float distanceRepos, Button button)
    {
        if (distanceRepos > scrollingPanel.rect.width + 10f)
        {
            float curX = button.GetComponent<RectTransform>().anchoredPosition.x;
            float curY = button.GetComponent<RectTransform>().anchoredPosition.y;

            Vector2 newAnchoredPosition = new Vector2(curX + (buttons.Length * buttonDistance), curY);
            button.GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
        }
    }

    private void MagnifyingButton(float minDistance)
    {
        for (int j = 0; j < buttons.Length; j++)
        {
            if (minDistance == distance[j])
            {
                minButton = j;
                buttons[j].GetComponent<RectTransform>().transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                buttons[j].interactable = true;
            }
        }
    }
}
