using UnityEngine;
using UnityEngine.UI;

public class ControleVolume : MonoBehaviour
{
    public Slider barreDeVolume; // Référence à la Scrollbar (assurez-vous d'utiliser le composant Slider)

    // Fonction appelée lorsqu'il y a un changement sur la barre de volume
    public void ChangerVolume()
    {
        // Utilisez la valeur normalisée de la barre de volume (entre 0 et 1) pour définir le volume
        AudioListener.volume = barreDeVolume.value;
    }
}
