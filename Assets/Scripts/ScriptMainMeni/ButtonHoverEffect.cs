using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    public float scaleUp = 1.1f;   // seberapa besar pas di-hover
    public float shakeAmount = 5f; // seberapa kuat goyangannya (pixel)
    public float speed = 10f;      // kecepatan animasi

    private bool isHovered = false;
    private float shakeTimer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Efek membesar halus
        Vector3 targetScale = isHovered ? originalScale * scaleUp : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        // Efek goyang kecil
        if (isHovered)
        {
            shakeTimer += Time.deltaTime * 20f;
            float shakeX = Mathf.Sin(shakeTimer * 20f) * shakeAmount * 0.1f;
            float shakeY = Mathf.Cos(shakeTimer * 15f) * shakeAmount * 0.1f;
            transform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0);
        }
        else
        {
            // Balikin posisi kalau gak dihover
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * speed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        shakeTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
