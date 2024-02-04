
using System.Collections.Generic;
using UnityEngine;

public class GuideScript : MonoBehaviour
{

    [SerializeField] private GameObject petal;
    [SerializeField] private int initialPetalCount = 6;
    [SerializeField] private int layerCount = 4;
    [SerializeField] private float layerGap = 0;
    [SerializeField] private float spreadFactor = 0.5f;




    [SerializeField] private float inspirationTimeMs = 4000;
    [SerializeField] private float expirationTimeMs = 4000;
    [SerializeField] private float holdTimeMs = 2000;



    private readonly float MIN_SCALE = 0.25f;
    private readonly float MAX_SCALE = 0.75f;


    private List<GameObject> GuideLayers = new();
    private Dictionary<int, List<GameObject>> GuidePetals = new();
    private Dictionary<int, Vector3[]> InitialPetalPositions = new Dictionary<int, Vector3[]>();

    // Start is called before the first frame update
    void Start()
    {

        for (int layer = 1; layer < (layerCount + 1); layer++)
        {

            GameObject layerObject = Instantiate(new GameObject($"Layer{layer}"), this.transform);

            GuideLayers.Add(layerObject);


            int layerPetalCount = layer * initialPetalCount;
            float angleGap = 2 * Mathf.PI / layerPetalCount;
            // float radius = layerGap * layer;
            float radius = 0.05f;

            List<GameObject> layerPetals = new();
            Vector3[] initialPositions = new Vector3[layerPetalCount];

            for (int j = 0; j < layerPetalCount; j++)
            {
                float angle = j * angleGap;

                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);


                float angleDegree = angle * Mathf.Rad2Deg;


                Vector3 petalPosition = new(x, y, 0);
                initialPositions[j] = petalPosition;

                Quaternion petalRotation = Quaternion.Euler(90 - angleDegree, 90, 0);

                GameObject PetalObject = Instantiate(petal, layerObject.transform);
                PetalObject.transform.localPosition = petalPosition;
                PetalObject.transform.localRotation = petalRotation;


                PetalObject.transform.localScale *= (layer / (float)layerCount) * (MAX_SCALE - MIN_SCALE) + MIN_SCALE;

                layerPetals.Add(PetalObject);
            }

            GuidePetals.Add(layer, layerPetals);
            InitialPetalPositions.Add(layer, initialPositions);

        }



    }

    private float timer = 0f;
    private int currentStep = 0; // 0: Inspiration, 1: Hold, 2: Expiration

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentStep)
        {
            case 0: // Inspiration
                UpdatePetalAnimation(inspirationTimeMs);
                if (timer >= inspirationTimeMs / 1000f)
                {
                    timer = 0f; // Réinitialiser le timer au début de chaque étape
                    currentStep = 1; // Passer à la rétention
                }
                break;
            case 1: // Hold
                if (timer >= holdTimeMs / 1000f)
                {
                    timer = 0f; // Réinitialiser le timer au début de chaque étape
                    currentStep = 2; // Passer à l'expiration
                }
                break;
            case 2: // Expiration
                UpdatePetalAnimation(expirationTimeMs);
                if (timer >= expirationTimeMs / 1000f)
                {
                    timer = 0f; // Réinitialiser le timer au début de chaque étape
                    currentStep = 0; // Revenir à l'inspiration
                }
                break;
        }
    }



    void SpreadLayer(int layer, float t)
    {
        bool isEven = layer % 2 == 0;

        int i = 0;
        foreach (GameObject petal in GuidePetals[layer])
        {
            petal.transform.localPosition = Vector3.Lerp(InitialPetalPositions[layer][i], InitialPetalPositions[layer][i] * layer * spreadFactor, t);


            i++;
        }
        float rotationAngle = 360f / inspirationTimeMs * t;


        int rotationDirection = isEven ? 1 : -1;
        GuideLayers[layer - 1].transform.Rotate(0, 0, rotationAngle * rotationDirection);
        // decrease z position
        GuideLayers[layer - 1].transform.Translate(0, 0, -t * layer * 0.001f);




    }

    void GatherLayer(int layer, float t)
    {
        bool isEven = layer % 2 == 0;

        int i = 0;
        foreach (GameObject petal in GuidePetals[layer])
        {
            petal.transform.localPosition = Vector3.Lerp(InitialPetalPositions[layer][i] * layer * spreadFactor, InitialPetalPositions[layer][i], t);
            i++;
        }
        float rotationAngle = 360f / inspirationTimeMs * t;


        int rotationDirection = isEven ? -1 : 1;
        GuideLayers[layer - 1].transform.Rotate(0, 0, rotationAngle * rotationDirection);
        // increase z position
        GuideLayers[layer - 1].transform.Translate(0, 0, t * layer * 0.001f);

    }

    void UpdatePetalAnimation(float durationMs)
    {
        float t = timer / (durationMs / 1000f);
        t = EaseInOut(t);

        for (int layer = 1; layer < (layerCount + 1); layer++)
        {
            if (currentStep == 0)
            {
                SpreadLayer(layer, t);

            }
            else if (currentStep == 2)
            {
                GatherLayer(layer, t);
            }
        }
    }



    float EaseInOut(float t, float power = 2)
    {
        if (t < 0.5f)
        {
            return 0.5f * Mathf.Pow(2 * t, power);
        }
        else
        {
            return 1 - 0.5f * Mathf.Pow(2 * (1 - t), power);
        }
    }




    // Update is called once per frame
    // void Update()
    // {
    //     float sinX = Mathf.Sin(Time.time * freq);

    //     int i = 1;


    //     foreach (int layer in GuidePetals.Keys)
    //     {

    //         int coeff = 1;

    //         GuideLayers[layer - 1].transform.Rotate(0, 0, sinX * coeff * 0.0005f * 360);

    //         foreach (GameObject petal in GuidePetals[layer])
    //         {

    //             petal.transform.Translate(0, sinX * 0.00005f * i, sinX * 0.00005f * i, Space.Self);
    //             petal.transform.Rotate(sinX * 0.001f, sinX * 0.05f * layer, 0, Space.Self);
    //             i++;
    //         }


    //     }



    // }
}
