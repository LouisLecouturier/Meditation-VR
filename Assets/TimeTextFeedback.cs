
using UnityEngine;
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
        Debug.Log("NAME");
        Debug.Log(guideScript.name);
    }

    // Update is called once per frame
    void Update()
    {

        switch (timeType)
        {
            case TimeType.Inspiration:
                Debug.Log("Inspiration");
                Debug.Log( guideScript.GetInspirationTimeS());
                textTemps.text = guideScript.GetInspirationTimeS() + "s";
                break;
            case TimeType.Expiration:
                textTemps.text = guideScript.GetExpirationTimeS() + "s";
                break;
            case TimeType.Hold:
                textTemps.text = guideScript.GetHoldTimeS() + "s";
                break;
        }

    }
}
