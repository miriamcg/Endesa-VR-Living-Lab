using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR;

public class InitialMovementManager : MonoBehaviour
{
    public GameObject player;

    private Vector3 initialPosition;
    private Vector3 initialRotation;


    [Header("Trajectory")]
    public Vector3[] targetsPosition;
    public Vector3[] targetsRotation;
    private int index;

    public float totalDuration = 15f;

    private float travelDuration;
    private float currentTimePosition = 0f;

    private bool move = true;
    private float totalDistance;


    [Header("Waiting time")]
    public bool waitForNextMove = true;
    public float secondsForNextMove = 1f;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = player.transform.position;
        initialRotation = player.transform.eulerAngles;

        totalDistance = getTotalDistance();
        travelDuration = getLineDuration();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            MultipleLineTimeWithRotation();
        }
    }

    void MultipleLineTimeWithRotation()
    {
        currentTimePosition += Time.deltaTime;
        if (currentTimePosition < travelDuration)
        {
            player.transform.position = Vector3.Lerp(initialPosition, targetsPosition[index], currentTimePosition / travelDuration);
            player.transform.eulerAngles = Vector3.Lerp(initialRotation, targetsRotation[index], currentTimePosition / travelDuration);
        }
        else
        {
            player.transform.position = targetsPosition[index];
            player.transform.eulerAngles = targetsRotation[index];

            index++;
            move = CheckIfContinue();

        }
    }

    bool CheckIfContinue()
    {
        if (index < targetsPosition.Length && index < targetsRotation.Length)
        {
            initialPosition = targetsPosition[index - 1];
            initialRotation = targetsRotation[index - 1];
            travelDuration = getLineDuration();
            currentTimePosition = 0f;

            if (waitForNextMove) StartCoroutine(SecondsForNextMove(secondsForNextMove));
            return !waitForNextMove;
        }
        else
            return false;
        
    }
    float getLineDuration()
    {
        float duration = totalDuration * Vector3.Distance(initialPosition, targetsPosition[index]) / totalDistance;
        return duration;
    }

    float getTotalDistance()
    {
        float distance = Vector3.Distance(initialPosition, targetsPosition[0]);
        for (int i = 1; i < targetsPosition.Length; i++)
        {
            distance += Vector3.Distance(targetsPosition[i - 1], targetsPosition[i]);
        }
        return distance;
    }

    IEnumerator SecondsForNextMove(float seconds)
    {
        move = false;
        yield return new WaitForSeconds(seconds);
        move = true;
    }
}