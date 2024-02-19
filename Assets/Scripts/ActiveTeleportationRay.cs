using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveTeleportationRay : MonoBehaviour
{
    public GameObject rightTeleportation;
    public InputActionProperty rightActivate;
    
    // Update is called once per frame
    void Update()
    {
        rightTeleportation.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
