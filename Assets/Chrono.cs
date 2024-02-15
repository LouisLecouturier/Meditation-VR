using UnityEngine;
using System;
using TMPro;

public class Chrono : MonoBehaviour
{
    private float _tempsEcoule;

    public TextMeshProUGUI textCompteur;

    private void Start()
    {
        _tempsEcoule = 0f;
    }

    private void Update()
    {
        _tempsEcoule += Time.deltaTime;

        textCompteur.text = _tempsEcoule.ToString("F2");
    }
}
