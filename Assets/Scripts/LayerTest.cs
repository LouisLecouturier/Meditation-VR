using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTest : MonoBehaviour
{
    public GameObject prefabPetal;
    private List<GameObject> petals;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            var petal = Instantiate(prefabPetal, this.transform);
            petals.Add(petal);
            petal.transform.localPosition = Vector3.right * i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
