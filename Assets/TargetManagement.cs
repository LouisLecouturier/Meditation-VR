using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Camera cam;

    private bool isAnimating = false;

    private float animationCooldown = 10.0f;
    private float nextAnimationTime = 0.0f;

    	[SerializeField] private float fadePerSecond = 2.5f;


    public Renderer rend;



    // IEnumerator FadeOut3D (float targetAlpha, bool isVanish, float duration)
    //     {
    //         // Renderer sr = GetComponent<Renderer> ();
    //         float diffAlpha = targetAlpha - sr.material.color.a;

    //         float counter = 0;
    //         while (counter < duration) {
    //             float alphaAmount = sr.material.color.a + (Time.deltaTime * diffAlpha) / duration;
    //             sr.material.color = new Color (sr.material.color.r, sr.material.color.g, sr.material.color.b, alphaAmount);

    //             counter += Time.deltaTime;
    //             yield return null;
    //         }
    //         sr.material.color = new Color (sr.material.color.r, sr.material.color.g, sr.material.color.b, targetAlpha);
    //         if (isVanish) {
    //             sr.transform.gameObject.SetActive (false);
    //         }
    //     }


    private bool IsVisible(Camera c)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
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
        // Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t));
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.25f);

        renderer.material.color = newColor;
        yield return null;
        }
    }
}


    void Start(){
    //    Renderer[] renderers = GetComponentsInChildren<Renderer>();
    //     foreach (Renderer renderer in renderers)
    //     {
    //         Color originalColor = renderer.material.color;
    //         Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
    //         renderer.material.color = newColor;
    //     }
    }
    void Update()
    {
        if (IsVisible(cam) && Time.time >= nextAnimationTime)
        {
            if (!isAnimating)
            {
                // Debug.Log("Visible");
                isAnimating = true;
                // StartCoroutine(FadeOut3D(0, false, 2));
                animator.SetTrigger("trigger");
                nextAnimationTime = Time.time + animationCooldown;
                StartCoroutine(FadeOut());



            }
        }
        else
        {
            isAnimating = false;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1 && isAnimating)
        {
            Debug.Log("Animation finished");
            isAnimating = false;
        }
    }
}