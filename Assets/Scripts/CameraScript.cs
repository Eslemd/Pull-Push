using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;

    private Vector3 orjinalPozisyon;
    private Coroutine aktifSallama;

    [Header("Hareket Ayarları")]
    public float hareketHizi = 1.5f;
    public float yumusaklik = 1.0f;
    public float beklemeSuresi = 5.0f; // 5 saniyede bir hareket eder

    private Vector3 hedefPozisyon;
    private Vector3 mevcutHiz = Vector3.zero;

    private void Awake()
    {
        Instance = this;
        orjinalPozisyon = transform.position;
        hedefPozisyon = orjinalPozisyon; // Başlangıçta olduğu yerde kalsın

        // Hareket döngüsünü başlat
        StartCoroutine(GezintiDongusu());
    }

    private void Update()
    {
        // Kamerayı sürekli hedef pozisyona doğru yumuşatır
        orjinalPozisyon = Vector3.SmoothDamp(orjinalPozisyon, hedefPozisyon, ref mevcutHiz, yumusaklik, hareketHizi);

        // Eğer sarsıntı yoksa pozisyonu güncelle
        if (aktifSallama == null)
        {
            transform.position = orjinalPozisyon;
        }
    }

    // --- YENİ EKLENEN ZAMANLAYICI DÖNGÜSÜ ---
    private IEnumerator GezintiDongusu()
    {
        while (true) // Oyun açık olduğu sürece döner
        {
            yield return new WaitForSeconds(beklemeSuresi);
            SetYeniHedef();
        }
    }

    private void SetYeniHedef()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-4f, 4f);
        hedefPozisyon = new Vector3(x, y, transform.position.z);
    }

    public void Salla(float sure, float siddet)
    {
        if (aktifSallama != null)
        {
            StopCoroutine(aktifSallama);
        }
        aktifSallama = StartCoroutine(SallamaEfekti(sure, siddet));
    }

    private IEnumerator SallamaEfekti(float sure, float siddet)
    {
        float gecenZaman = 0f;

        while (gecenZaman < sure)
        {
            float x = Random.Range(-0.4f, 0.4f) * siddet;
            float y = Random.Range(-0.4f, 0.4f) * siddet;

            transform.position = new Vector3(orjinalPozisyon.x + x, orjinalPozisyon.y + y, orjinalPozisyon.z);

            gecenZaman += Time.deltaTime;
            yield return null;
        }

        transform.position = orjinalPozisyon;
        aktifSallama = null;
    }
}