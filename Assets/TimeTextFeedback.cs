using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        guideScript = GameObject.Find("Guide").GetComponent<GuideScript>();

        switch (timeType)
        {
            case TimeType.Inspiration:
                GetComponent<Text>().text = guideScript.GetInspirationTimeS().ToString() + "s";
                break;
            case TimeType.Expiration:
                GetComponent<Text>().text = guideScript.GetExpirationTimeS().ToString() + "s";
                break;
            case TimeType.Hold:
                GetComponent<Text>().text = guideScript.GetHoldTimeS().ToString() + "s";
                break;
        }


    }
}
