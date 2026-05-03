using UnityEngine;

public class MesafePitchKontrol : MonoBehaviour
{
    public AudioSource muzikKaynagi;
    public Transform karakter;      // Karakterin Transform'u
    public Transform merkezNokta;   // Güvenli bölgenin merkezi (Harmonie_zone)

    [Header("Mesafe Ayarlarý")]
    public float maksimumMesafe = 20f; // Ne kadar uzaklaþýnca pitch en dibe vursun?
    public float minPitch = 0.6f;      // En düþük pitch deðeri
    public float maxPitch = 1.0f;      // En yüksek pitch deðeri

    void Update()
    {
        if (karakter != null && merkezNokta != null)
        {
            // 1. Karakter ile merkez arasýndaki mesafeyi hesapla
            float mesafe = Vector3.Distance(karakter.position, merkezNokta.position);

            // 2. Mesafeyi 0-1 arasý bir orana çevir (Ters oran)
            // Yakýndayken 1, Uzaktayken 0 olacak þekilde
            float oran = 1f - Mathf.Clamp01(mesafe / maksimumMesafe);

            // 3. Pitch deðerini uygula
            muzikKaynagi.pitch = Mathf.Lerp(minPitch, maxPitch, oran);

            // Test etmek için konsola yazdýrabilirsin:
            // Debug.Log("Uzaklýk: " + mesafe + " | Pitch: " + muzikKaynagi.pitch);
        }
    }
}