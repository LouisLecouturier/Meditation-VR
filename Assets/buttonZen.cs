using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonZen : MonoBehaviour
{
    
    public Camera cam;

    public void OnClick()
    {
        cam.GetComponent<ZenMode>().ZenModeStart();
        
    }

}

 
