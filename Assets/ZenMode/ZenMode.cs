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
    public enum ZenModeState
    {
        Waiting,
        ZenMode,
        Congrats,
    }

    // Variable pour stocker l'état actuel
    private ZenModeState currentState = ZenModeState.Waiting;


    void ZenModeStart() {
        currentState = ZenModeState.ZenMode;
    }

    private IEnumerator ZenModeCoroutine()
    {
        if (leftHand.position.y > transform.position.y && rightHand.position.y > transform.position.y && leftController.activateInteractionState.value > 0 && rightController.activateInteractionState.value > 0){
            yield return new WaitForSeconds(2);

            currentState = ZenModeState.Congrats;
        }
        else {
            if (textObject.text != "Lotus position")
            {
                textObject.text = "Lotus position";
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
        currentState = ZenModeState.Waiting;    
    }

   

    private IEnumerator Waiting()
    {
        yield return null;
    }

    void Start()
    {
        ZenModeStart();
    }


    void Update()
    {
        switch (currentState) {
            case ZenModeState.ZenMode:
                StartCoroutine(ZenModeCoroutine());
                break;
            case ZenModeState.Congrats:
                StartCoroutine(Congrats());
                break;
            case ZenModeState.Waiting:
                StartCoroutine(Waiting());
                break; 
        }                 
    }
}
