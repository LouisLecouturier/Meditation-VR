
using System;
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
    private Dictionary<int, Vector3[]> InitialPetalScales = new Dictionary<int, Vector3[]>();



    public double GetInspirationTimeS() {
        float inspirationTimeS = inspirationTimeMs / 1000f;
        return Math.Round(inspirationTimeS, 1);
    }

    public void SetInspirationTimeMs(float timeMs)
    {
        inspirationTimeMs = timeMs;
    }
    public double GetExpirationTimeS() {

        float expTimeS = expirationTimeMs / 1000f;
        return Math.Round(expTimeS, 1);
    }

    public void SetExpirationTimeMs(float timeMs)
    {
    
        expirationTimeMs = timeMs;
    }

    public double GetHoldTimeS() {
        float holdTimeS = holdTimeMs / 1000f;
        return Math.Round(holdTimeS, 1);
    }
    public void SetHoldTimeMs(float timeMs)
    {
        holdTimeMs = timeMs;
    }


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
            Vector3[] initialScales = new Vector3[layerPetalCount];

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


                float scale = layer / (float)layerCount * (MAX_SCALE - MIN_SCALE) + MIN_SCALE;
                initialScales[j] = PetalObject.transform.localScale;
                PetalObject.transform.localScale *= scale;


                layerPetals.Add(PetalObject);
            }

            GuidePetals.Add(layer, layerPetals);
            InitialPetalPositions.Add(layer, initialPositions);
            InitialPetalScales.Add(layer, initialScales);

        }


    }





    float EasedProgression(float timeProgression)
    {
        float SinX = Math.Abs(Mathf.Sin(Mathf.PI * timeProgression * 0.5f));
        return SinX;
    }




    float TimeProgression(float timer, float periodDurationMs)
    {
        return timer / periodDurationMs;
    }


    private float timer = 0f;
    private int currentStep = 0; // 0: Inspiration, 1: Hold, 2: Expiration
    float stepProgression = 0;
    void Update()
    {

        timer += Time.deltaTime * 1000;


        switch (currentStep)
        {
            case 0:
                stepProgression = TimeProgression(timer, inspirationTimeMs);

                break;

            case 1:
                stepProgression = TimeProgression(timer, holdTimeMs);
                break;

            case 2:
                stepProgression = TimeProgression(timer, expirationTimeMs);
                break;

        }



        HandleStepAnimations(currentStep, stepProgression);


        if (stepProgression >= 1)
        {
            timer = 0;
            currentStep++;
            if (currentStep > 2) currentStep = 0;
        }

    }


    void RotateLayer(int layer, float angle, bool revert, float progression)
    {
        if (revert) angle = -angle;
        GuideLayers[layer - 1].transform.localRotation = Quaternion.Euler(0, 0, angle * progression);
    }



    void AnimateInspiration(float progression)
    {

        for (int layer = 1; layer < (layerCount + 1); layer++)
        {
            int layerPetalCount = layer * initialPetalCount;
            Vector3[] initialPositions = InitialPetalPositions[layer];
            List<GameObject> layerPetals = GuidePetals[layer];

            bool isEven = layer % 2 == 0;

            RotateLayer(layer, 360, isEven, progression);

            GuideLayers[layer - 1].transform.position = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -layer * layerGap), progression);

            for (int j = 0; j < layerPetalCount; j++)
            {
                Vector3 initialPosition = initialPositions[j];
                GameObject petal = layerPetals[j];

                petal.transform.localPosition = Vector3.Lerp(initialPosition, layer * spreadFactor * initialPosition, progression);
            }
        }
    }


    void AnimateHold(float progression)
    {

        for (int layer = 1; layer < (layerCount + 1); layer++)
        {
            //
        }

    }


    void AnimateExpiration(float progression)
    {

        for (int layer = 1; layer < (layerCount + 1); layer++)
        {
            int layerPetalCount = layer * initialPetalCount;
            Vector3[] initialPositions = InitialPetalPositions[layer];
            List<GameObject> layerPetals = GuidePetals[layer];

            bool isEven = layer % 2 == 0;


            RotateLayer(layer, 360, isEven, progression);

            GuideLayers[layer - 1].transform.position = Vector3.Lerp(new Vector3(0, 0, -layer * layerGap), Vector3.zero, progression);

            for (int j = 0; j < layerPetalCount; j++)
            {
                Vector3 initialPosition = initialPositions[j];
                GameObject petal = layerPetals[j];

                petal.transform.localPosition = Vector3.Lerp(layer * spreadFactor * initialPosition, initialPosition, progression);
            }
        }

    }


    void HandleStepAnimations(int step, float progression)
    {
        switch (step)
        {
            case 0:
                float inspirationProgression = EasedProgression(progression);
                AnimateInspiration(inspirationProgression);
                break;
            case 1:
                AnimateHold(progression);
                break;
            case 2:
                float expirationProgression = EasedProgression(progression);
                AnimateExpiration(expirationProgression);
                break;
        }
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

