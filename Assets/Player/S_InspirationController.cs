using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_InspirationController : MonoBehaviour

{


    private GuideScript guideScript;
    public Scrollbar scrollBar;

    [SerializeField] private float maxTimeMs = 10000;
    [SerializeField] private float minTimeMs = 1000;



    // Start is called before the first frame update
    void Start()
    {
        guideScript = GameObject.Find("Guide").GetComponent<GuideScript>();
        scrollBar.onValueChanged.AddListener(OnScrollBarValueChanged); // Ajoutez cette ligne pour vous abonner à l'événement onValueChanged
    }


    private void OnScrollBarValueChanged(float value)
    {
        float mappedValue = minTimeMs + value * (maxTimeMs - minTimeMs);
        guideScript.SetInspirationTimeMs(mappedValue);
    }
}
