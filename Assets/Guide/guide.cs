using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class GuideScript : MonoBehaviour
{

    [SerializeField] private GameObject petal;
    [SerializeField] private int initialPetalCount = 6;
    [SerializeField] private int layerCount = 4;
    [SerializeField] private float layerGap = 0;

    [SerializeField] private float freq = 0.75f;


    private float MIN_SCALE = 0.5f;


    private List<GameObject> GuideLayers = new();
    private Dictionary<int, List<GameObject>> GuidePetals = new();

    // Start is called before the first frame update
    void Start()
    {



        for (int layer = 1; layer < (layerCount + 1); layer++)
        {

            GameObject layerObject = Instantiate(new GameObject($"Layer{layer}"), this.transform);

            GuideLayers.Add(layerObject);


            int layerPetalCount = layer * initialPetalCount;
            float angleGap = 2 * Mathf.PI / layerPetalCount;
            float radius = layerGap * layer;


            List<GameObject> layerPetals = new();


            for (int j = 0; j < layerPetalCount; j++)
            {
                float angle = j * angleGap;

                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);


                float angleDegree = angle * Mathf.Rad2Deg;


                Vector3 petalPosition = new(x, y, -(layer * 0.15f));
                Quaternion petalRotation = Quaternion.Euler(90 - angleDegree, 90, 0);

                GameObject PetalObject = Instantiate(petal, layerObject.transform);
                PetalObject.transform.localPosition = petalPosition;
                PetalObject.transform.localRotation = petalRotation;
                // petalPosition, petalRotation,

                PetalObject.transform.localScale *= Mathf.Max(layer * 0.3f, MIN_SCALE);


                layerPetals.Add(PetalObject);
            }

            GuidePetals.Add(layer, layerPetals);


        }



    }

    // Update is called once per frame
    void Update()
    {
        float sinX = Mathf.Sin(Time.time * freq);


        foreach (int layer in GuidePetals.Keys)
        {

            int coeff = layer % 2 == 0 ? 1 : -1;

            GuideLayers[layer - 1].transform.Rotate(0, 0, sinX * coeff * 0.0001f * 360);
            GuideLayers[layer - 1].transform.Translate(0, 0, -1 * (layer + 1) * sinX * 0.0025f);


            int i = 1;
            foreach (GameObject petal in GuidePetals[layer])
            {
                Debug.Log(layer);
                // Vector3 position = petal.transform.position;
                // position.y += sinX * 0.001f; // Oscillate position on X axis
                // petal.transform.position = position;
                petal.transform.Translate(0, sinX * 0.0005f * layer, 0, Space.Self); // Oscillate position on local Y axis

                float randomFactor = UnityEngine.Random.Range(0f, 1f);

                petal.transform.Rotate(sinX * 0.001f * randomFactor, sinX * randomFactor * 0.005f * i * layer, 0, Space.Self); // Oscillate position on local Y axis
                i++;
            }


        }



    }
}
