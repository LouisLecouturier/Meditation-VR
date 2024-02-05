using UnityEngine;
using UnityEngine.UI;

public class Compteur : MonoBehaviour
{
    public float tempsInitial = 60.0f; // Temps initial du compteur en secondes
    private float tempsRestant; // Temps restant du compteur

    public Text textCompteur; // Correction : la variable doit être nommée textCompteur

    private void Start()
    {
        tempsRestant = tempsInitial;
    }

    private void Update()
    {
        tempsRestant -= Time.deltaTime;

        // Mettez à jour le texte du compteur
        textCompteur.text = "Temps : " + Mathf.Round(tempsRestant).ToString();

        // Vérifiez si le temps est écoulé
        if (tempsRestant <= 0)
        {
            // Mettez ici le code à exécuter lorsque le temps est écoulé
            Debug.Log("Temps écoulé !");
            // Vous pouvez désactiver le script ou déclencher une autre action
            enabled = false;
        }
    }
}
