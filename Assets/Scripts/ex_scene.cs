using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ex_scene : MonoBehaviour
{
    public Camera cam;

    public void OnClick()
    {
        cam.GetComponent<NeckJoint>().NeckJointStart();
        
    }
}

