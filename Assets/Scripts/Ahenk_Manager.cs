using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AhenkManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public float[] tempLevels = { -100f, -75f, -50f, -25f, 0f};
    public int currentLevelIndex = 2; // Ba�lang�� 0 Temp
    public float thresholdTime = 2.0f; // 2 saniye kural�
    public float lerpSpeed = 2.0f; // Renk ge�i� h�z� (y�ksek de�er daha h�zl�d�r)

    [Header("Referanslar")]
    public Volume globalVolume;
    private WhiteBalance _whiteBalance;
    private Vignette _vignette;

    public Canvas canvasGroup; // Ekran efekti i�in CanvasGroup referans�

    private float _timer = 0f;
    private float _targetTemp = 0f; // Ula��lmak istenen s�cakl�k
    public bool isInside = true;

    public GameObject MC;

    void Start()
    {
        if (globalVolume.profile.TryGet<WhiteBalance>(out _whiteBalance))
        {
            _targetTemp = tempLevels[currentLevelIndex];
            _whiteBalance.temperature.value = _targetTemp; // Ba�lang��ta an�nda set et
        }

        globalVolume.profile.TryGet<Vignette>(out _vignette);
    }

    void Update()
    {
        HandleTimer();
        ApplySmoothColor();
    }

    void HandleTimer()
    {
        _timer += Time.deltaTime;

        if (_timer >= thresholdTime)
        {
            if (isInside) LevelUp();
            else LevelDown();

            if (!isInside && currentLevelIndex == 0)
            {
                if (MC != null) Destroy(MC);
                if (canvasGroup != null) canvasGroup.gameObject.SetActive(true);
                
            }

            _timer = 0f;
        }
    }

    // Renkleri her karede yumu�ak bir �ekilde hedefe yakla�t�r�r
    void ApplySmoothColor()
    {
        if (_whiteBalance != null)
        {
            // Mevcut de�eri, hedef de�ere do�ru yumu�ak�a kayd�r (Lerp)
            _whiteBalance.temperature.value = Mathf.Lerp(
                _whiteBalance.temperature.value,
                _targetTemp,
                Time.deltaTime * lerpSpeed
            );

            if (_vignette != null)
            {
                float t = Mathf.InverseLerp(tempLevels[0], tempLevels[tempLevels.Length - 1], _whiteBalance.temperature.value);

                _vignette.intensity.value = Mathf.Lerp(0.5f, 0f, t);
            }
        }
    }

    public void OnZoneStateChanged(bool inside)
    {
        isInside = inside;
        _timer = 0f;
    }

    void LevelUp()
    {
        if (currentLevelIndex < tempLevels.Length - 1)
        {
            currentLevelIndex++;
            _targetTemp = tempLevels[currentLevelIndex]; // Hedefi g�ncelle
        }
    }

    void LevelDown()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            _targetTemp = tempLevels[currentLevelIndex]; // Hedefi g�ncelle
        }
    }
}