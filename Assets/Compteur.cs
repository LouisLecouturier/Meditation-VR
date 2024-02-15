using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compteur : MonoBehaviour
{
    public float tempsInitial = 60.0f;
    private float tempsRestant;

    public TextMeshProUGUI textCompteur; 

    private void Start()
    {
        tempsRestant = tempsInitial;
    }

    private void Update()
    {
        tempsRestant -= Time.deltaTime;

        // Mettez à jour le texte du compteur
        textCompteur.text = "Temps : " + Mathf.Round(tempsRestant).ToString() + "s";

        // Vérifiez si le temps est écoulé
        if (tempsRestant <= 0)
        {
            // Mettez ici le code à exécuter lorsque le temps est écoulé
            Debug.Log("Fin de la session !");
            textCompteur.text = "Fin de la session !";
            // Vous pouvez désactiver le script ou déclencher une autre action
            enabled = false;
        }
    }
}
