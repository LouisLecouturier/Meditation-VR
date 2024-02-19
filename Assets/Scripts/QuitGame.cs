using UnityEngine;

public class GestionQuitter : MonoBehaviour
{
    // Fonction appelée lorsque le bouton Quit est pressé
    public void QuitterJeu()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
