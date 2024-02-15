using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


enum TimeType
{
    Inspiration,
    Expiration,
    Hold
}

public class TimeTextFeedback : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TimeType timeType;
    private GuideScript guideScript;

    public TextMeshProUGUI textTemps;


   void Start()
    {
        guideScript = GameObject.Find("Guide").GetComponent<GuideScript>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (timeType)
        {
            case TimeType.Inspiration:
                textTemps.text = guideScript.GetInspirationTimeS().ToString() + "s";
                break;
            case TimeType.Expiration:
                textTemps.text = guideScript.GetExpirationTimeS().ToString() + "s";
                break;
            case TimeType.Hold:
                textTemps.text = guideScript.GetHoldTimeS().ToString() + "s";
                break;
        }


    }
}
