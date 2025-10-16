using UnityEngine;
using TMPro;

public class CatchTextFade : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RectTransform rect;
    private float timer = 0f;

    [Header("Fade Settings")]
    public float fadeSpeed = 1f; // kecepatan efek muncul dan hilang
    public float scaleAmount = 1.05f; // seberapa besar teks membesar

    private Vector3 originalScale;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
        text.alpha = 0f; // awalnya tidak terlihat
    }

    void Update()
    {
        // Timer untuk looping efek
        timer += Time.deltaTime * fadeSpeed;

        // Gunakan sin untuk transisi halus
        float fade = Mathf.Abs(Mathf.Sin(timer));

        // Fade in & out
        text.alpha = fade;

        // Skala sedikit membesar ketika sedang terang
        float scale = Mathf.Lerp(1f, scaleAmount, fade);
        rect.localScale = originalScale * scale;
    }
}
