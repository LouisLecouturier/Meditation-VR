using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compteur : MonoBehaviour
{
    public float tempsInitial = 60.0f;
    private float tempsRestant;

    public TextMeshProUGUI textCompteur; // Utilisez TextMeshProUGUI pour les textes TextMeshPro

    private void Start()
    {
        tempsRestant = tempsInitial;
    }

    private void Update()
    {
        tempsRestant += Time.deltaTime;

        // Mettez à jour le texte du compteur
        textCompteur.text =  Mathf.Round(tempsRestant).ToString();

    }
}
