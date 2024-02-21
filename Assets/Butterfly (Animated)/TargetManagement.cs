using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Camera cam;
    private float animationCooldown = 10.0f;
    private float nextAnimationTime = 0.0f;
    
    private bool isAnimating = false;


    // private bool IsVisible(Camera c)
    // {
    //     var planes = GeometryUtility.CalculateFrustumPlanes(c);
    //     var point = transform.position;

    //     foreach (var plane in planes)
    //     {
    //         if (plane.GetDistanceToPoint(point) < 0)
    //         {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
    private bool IsVisible(Camera c)
{
    Vector3 viewportPoint = c.WorldToViewportPoint(transform.position);

    // Vérifie si le point est dans le frustum de la caméra
    if (viewportPoint.z < 0 || viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
    {
        return false;
    }

    // Vérifie si le point est dans la moitié centrale de l'écran
    if (viewportPoint.x < 0.25f || viewportPoint.x > 0.75f || viewportPoint.y < 0.25f || viewportPoint.y > 0.75f)
    {
        return false;
    }

    return true;
}

    IEnumerator FadeOut()
{
    Renderer[] renderers = GetComponentsInChildren<Renderer>();
    float fadeDuration = 1f;
    float fadeSpeed = 1 / fadeDuration;

    for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
    {
        foreach (Renderer renderer in renderers)
        {
        Color originalColor = renderer.material.color;
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t));
        Debug.Log(newColor.a);

        renderer.material.color = newColor;
        }
        yield return null;
    }
}



    IEnumerator ResetOpacity()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Color originalColor = renderer.material.color;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

            renderer.material.color = newColor;
            yield return null;
        }
    }


    void Start(){
        StartCoroutine(ResetOpacity());
    }

    IEnumerator TriggerAnim() {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("trigger");
        nextAnimationTime = Time.time + animationCooldown;
        StartCoroutine(FadeOut());
    }


    void Update()
    {
        if (IsVisible(cam) && Time.time >= nextAnimationTime && !isAnimating)
        {
            Debug.Log("Launching animation");
            isAnimating = true;
            StartCoroutine(TriggerAnim());

        }

        if(!IsVisible(cam) && Time.time >= nextAnimationTime && isAnimating)
        {
            Debug.Log("Resetting animation");
            StartCoroutine(ResetOpacity());
            isAnimating = false;
        }
       
    }
}


