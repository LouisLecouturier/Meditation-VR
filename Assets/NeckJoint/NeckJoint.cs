using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeckJoint : MonoBehaviour
{

    public TextMeshProUGUI textObject;


      // Enumération pour définir les différents états
    public enum NeckJointState
    {
        Waiting,
        TurningLeft,
        TurningRight,
        Congrats,
    }

    // Variable pour stocker l'état actuel
    private NeckJointState currentState = NeckJointState.Waiting;

    public void NeckJointStart() {
        currentState = NeckJointState.TurningLeft;
        Debug.Log("Turning Left");

    }


    float GetAngle()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360; 
        // angle = Mathf.Abs(angle);
        // Debug.Log(angle);
       return angle;
    }

    private IEnumerator TurnLeft()
    {
        if (GetAngle() > 45)
        {
            yield return new WaitForSeconds(2);

            currentState = NeckJointState.TurningRight;
        }
        else {
            if (textObject.text != "Tilt left")
        {
            textObject.text = "Tilt left";
        }

        }
        yield return null;
    }

    private IEnumerator TurnRight()
    {
        
        if (GetAngle() < -45)
        {
            yield return new WaitForSeconds(2);

            currentState = NeckJointState.Congrats;
        }
        else {
            if (textObject.text != "Tilt right")
        {
            textObject.text = "Tilt right";
        }

        }
        yield return null;
    }

    private IEnumerator Congrats()
    {
        if (textObject.text != "Well done!")
        {
            textObject.text = "Well done!";
        }
        yield return new WaitForSeconds(4);
        textObject.text = "";
        currentState = NeckJointState.Waiting;    
    }

    private IEnumerator Waiting()
    {
        yield return null;
    }

    // void Start()
    // {
    //     NeckJointStart();
    // }

    void Update()
    {
        switch (currentState) {
            case NeckJointState.TurningLeft:
                StartCoroutine(TurnLeft());
                break;
            case NeckJointState.TurningRight:
                StartCoroutine(TurnRight());
                break;
            case NeckJointState.Congrats:
                StartCoroutine(Congrats());
                break;
            case NeckJointState.Waiting:
                StartCoroutine(Waiting());
                break;
            
        }
    }
}
