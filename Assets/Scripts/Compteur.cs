using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compteur : MonoBehaviour
{
    public float tempsInitial = 0.0f;
    private float tempsRestant;

    public TextMeshProUGUI textCompteur;

    private void Start()
    {
        tempsRestant = tempsInitial;
    }

    private void Update()
    {
        tempsRestant += Time.deltaTime;

        int minutes = Mathf.FloorToInt(tempsRestant / 60F - 1);
        int secondes = Mathf.FloorToInt(tempsRestant - minutes * 60 - 60);

        // Mettez à jour le texte du compteur
        textCompteur.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }
}
