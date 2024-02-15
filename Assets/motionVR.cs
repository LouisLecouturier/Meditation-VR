using UnityEngine;
using UnityEngine.XR;

public class MouvementCasqueVR : MonoBehaviour
{
    private void Update()
    {
        if (XRSettings.enabled)
        {
            // Récupérer les données de mouvement du casque VR
            Vector3 position = InputTracking.GetLocalPosition(XRNode.CenterEye);
            Quaternion rotation = InputTracking.GetLocalRotation(XRNode.CenterEye);

            // Utiliser les données de mouvement comme nécessaire
            // Par exemple, afficher la position et la rotation
            Debug.Log("Position : " + position + ", Rotation : " + rotation.eulerAngles);
        }
    }
}
