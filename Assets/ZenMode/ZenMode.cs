using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;



public class ZenMode : MonoBehaviour
{
   public TextMeshProUGUI textObject;

   public Transform leftHand;
    public Transform rightHand;
    public XRBaseController leftController;
    public XRBaseController rightController;



      // Enumération pour définir les différents états
    public enum NeckJointState
    {
        Waiting,
        ZenMode,
    }

    // Variable pour stocker l'état actuel
    private NeckJointState currentState = NeckJointState.Waiting;

    void NeckJointStart() {
        currentState = NeckJointState.ZenMode;
        Debug.Log("Turning Left");

    }


    float GetAngle()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360; 
        angle = Mathf.Abs(angle);
       return angle;
    }

    private IEnumerator ZenModeCoroutine()
    {
        if (leftHand.position.y > transform.position.y && rightHand.position.y > transform.position.y && leftController.activateInteractionState.value > 0 && rightController.activateInteractionState.value > 0){
            yield return new WaitForSeconds(2);

            currentState = NeckJointState.Waiting;
        }
        else {
            if (textObject.text != "ZenMode")
            {
                textObject.text = "ZenMode";
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


    void Start()
    {
        // NeckJointStart();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case NeckJointState.ZenMode:
                StartCoroutine(ZenModeCoroutine());
                break;
            case NeckJointState.Waiting:
                StartCoroutine(Waiting());
                break; 
        }                 
    }
}
