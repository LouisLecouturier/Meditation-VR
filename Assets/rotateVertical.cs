using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateVertical : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float rotationSpeed = 10.0f; // Vous pouvez ajuster cette valeur pour changer la vitesse de rotation
        transform.Rotate(-mouseY * rotationSpeed, 0, 0);
        
    }
}
