using UnityEngine;
using TMPro;

public class BooGlowEffect : MonoBehaviour
{
    [Header("Referensi")]
    public TextMeshProUGUI booText;       // teks “BOO”
    public RectTransform bloodImage;      // gambar darah

    [Header("Efek Glow BOO")]
    public Color darkColor = new Color(0.5f, 0.1f, 0.1f);
    public Color brightColor = new Color(1f, 0.9f, 0.9f);
    public float glowSpeed = 1.2f;        // lebih lambat, biar adem

    [Header("Efek Shake (lembut)")]
    public float booShakeAmount = 1.5f;   // kecil biar lembut
    public float bloodShakeAmount = 0.8f; // lebih kecil dari BOO
    public float shakeSpeed = 1.2f;       // goyangan lambat

    private Vector3 booOriginalPos;
    private Vector3 bloodOriginalPos;
    private float timer;

    void Start()
    {
        if (booText == null)
            Debug.LogWarning("⚠️ booText belum diisi!");
        if (bloodImage == null)
            Debug.LogWarning("⚠️ bloodImage belum diisi!");

        if (booText != null)
            booOriginalPos = booText.rectTransform.localPosition;
        if (bloodImage != null)
            bloodOriginalPos = bloodImage.localPosition;
    }

    void Update()
    {
        timer += Time.deltaTime * glowSpeed;

        // 🌈 Efek Glow BOO (lembut, warna nyala redup)
        if (booText != null)
        {
            float t = (Mathf.Sin(timer) + 1f) / 2f;
            booText.color = Color.Lerp(darkColor, brightColor, t);

            // Getaran lembut pelan
            float offsetX = Mathf.Sin(timer * shakeSpeed) * booShakeAmount;
            float offsetY = Mathf.Cos(timer * shakeSpeed * 1.3f) * booShakeAmount * 0.5f;
            booText.rectTransform.localPosition = booOriginalPos + new Vector3(offsetX, offsetY, 0);
        }

        // 💉 Getaran lembut darah
        if (bloodImage != null)
        {
            float offsetX = Mathf.Sin(timer * shakeSpeed * 0.7f) * bloodShakeAmount;
            float offsetY = Mathf.Cos(timer * shakeSpeed * 1.1f) * bloodShakeAmount * 0.5f;
            bloodImage.localPosition = bloodOriginalPos + new Vector3(offsetX, offsetY, 0);
        }
    }
}
