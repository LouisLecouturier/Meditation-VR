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
    }

    // Variable pour stocker l'état actuel
    private NeckJointState currentState = NeckJointState.Waiting;

    void NeckJointStart() {
        currentState = NeckJointState.TurningLeft;
        Debug.Log("Turning Left");

    }


    float GetAngle()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360; 
        angle = Mathf.Abs(angle);
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
            if (textObject.text != "Turning Left")
        {
            textObject.text = "Turning Left";
        }

        }
        yield return null;
    }

    private IEnumerator TurnRight()
    {
        
        if (GetAngle() < -45)
        {
            yield return new WaitForSeconds(2);

            currentState = NeckJointState.Waiting;
        }
        else {
            if (textObject.text != "Turning Right")
        {
            textObject.text = "Turning Right";
        }

        }
        yield return null;
    }

    private IEnumerator Waiting()
    {
        // if (textObject.text != "Waiting")
        // {
        //     textObject.text = "Waiting";
        // }
        yield return null;
    }


    // void Start()
    // {
    //     NeckJointStart();
    // }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case NeckJointState.TurningLeft:
                StartCoroutine(TurnLeft());
                break;
            case NeckJointState.TurningRight:
                StartCoroutine(TurnRight());
                break;
        
            case NeckJointState.Waiting:
                StartCoroutine(Waiting());
                break;
            
        }
        
        
        
    }
}
