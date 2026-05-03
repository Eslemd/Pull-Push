using System.Collections.Generic;
using UnityEngine;

public class random_Spawn : MonoBehaviour
{
    public GameObject pullPrefab;
    public GameObject pushPrefab;

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi; // Inspector'dan AudioSource'u sürükle
    public AudioClip solTikSesi;  // Sol tık sesi (Örn: Kick)
    public AudioClip sagTikSesi;  // Sağ tık sesi (Örn: Snare/Tiz)

    public float safeZoneSize = 2.5f;
    public int maxPlanetCount = 3;

    private Queue<GameObject> spawnedPlanetsQueue = new Queue<GameObject>();

    private void Start()
    {
        SpawnRandomObject();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnAtMousePosition(pullPrefab);
            CameraScript.Instance.Salla(0.1f, 0.2f);

            // SES ÇAL: Sol tık
            SesCal(solTikSesi);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SpawnAtMousePosition(pushPrefab);
            CameraScript.Instance.Salla(0.1f, 0.2f);

            // SES ÇAL: Sağ tık
            SesCal(sagTikSesi);
        }
    }

    // --- SES ÇALMA FONKSİYONU ---
    private void SesCal(AudioClip klip)
    {
        if (klip != null && sesKaynagi != null)
        {
            // Her tıklandığında sesin tonunu %5 yukarı/aşağı rastgele değiştirir
            // Bu, aynı sesin robotik gelmesini engeller.
            sesKaynagi.pitch = Random.Range(0.95f, 1.05f);

            // PlayOneShot, seslerin birbirini kesmeden üst üste binmesini sağlar
            sesKaynagi.PlayOneShot(klip);
        }
    }

    private void ManageObjectQueue(GameObject newPlanet)
    {
        spawnedPlanetsQueue.Enqueue(newPlanet);

        if (spawnedPlanetsQueue.Count > maxPlanetCount)
        {
            GameObject oldestPlanet = spawnedPlanetsQueue.Dequeue();
            if (oldestPlanet != null)
            {
                Destroy(oldestPlanet);
            }
        }
    }

    public void SpawnRandomObject()
    {
        Vector3 finalPosition = Vector3.zero;
        bool foundValidSpot = false;
        while (!foundValidSpot)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomY = Random.Range(-4.5f, 4.5f);
            if (Mathf.Abs(randomX) > safeZoneSize || Mathf.Abs(randomY) > safeZoneSize)
            {
                finalPosition = new Vector3(randomX, randomY, 0f);
                foundValidSpot = true;
            }
        }

        GameObject rastgeleGezegen = Instantiate(pullPrefab, finalPosition, Quaternion.identity);
        ManageObjectQueue(rastgeleGezegen);
    }

    public void SpawnAtMousePosition(GameObject prefab)
    {
        Vector3 mousePixelPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition);
        worldPosition.z = 0f;

        if (Mathf.Abs(worldPosition.x) < safeZoneSize && Mathf.Abs(worldPosition.y) < safeZoneSize)
        {
            Debug.Log("Hile Yapılamaz! Merkeze çok yakın tıkladın.");
            return;
        }

        GameObject tiklananGezegen = Instantiate(prefab, worldPosition, Quaternion.identity);
        ManageObjectQueue(tiklananGezegen);
    }
}