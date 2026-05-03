using UnityEngine;

public class SpaceShip_Spawn : MonoBehaviour
{
   [SerializeField] private GameObject[] spaceShipPrefab;
   [SerializeField] private float firstWaveDelay = 15f;
   [SerializeField] private float waveInterval = 15f;
   [SerializeField] private int minShipsPerWave = 3;
   [SerializeField] private int maxShipsPerWave = 6;
   [SerializeField] private float addForceSpeed = 8f;
   [SerializeField] private AudioSource startAudioSource;
   [SerializeField] private AudioClip startSound;
   [SerializeField] private GameObject spawnpoint;

   private void Start()
   {
      InvokeRepeating(nameof(SpawnWave), Mathf.Max(0f, firstWaveDelay), waveInterval);
   }

   private void SpawnWave()
   {
      if (spaceShipPrefab == null || spaceShipPrefab.Length == 0)
      {
         return;
      }

      int clampedMin = Mathf.Max(1, minShipsPerWave);
      int clampedMax = Mathf.Max(clampedMin, maxShipsPerWave);
      int shipsToSpawn = Random.Range(clampedMin, clampedMax + 1);

      if (startAudioSource != null && startSound != null)
      {
         startAudioSource.PlayOneShot(startSound);
      }

      for (int i = 0; i < shipsToSpawn; i++)
      {
         SpawnSpaceShip();
      }
   }

   private void SpawnSpaceShip()
   {
      if (spaceShipPrefab == null || spaceShipPrefab.Length == 0 || spawnpoint == null)
      {
         return;
      }

      int randomIndex = Random.Range(0, spaceShipPrefab.Length);
      Vector3 spawnPosition = GetRandomSpawnPosition();
      Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 180f);
      GameObject spawnedShip = Instantiate(spaceShipPrefab[randomIndex], spawnPosition, spawnRotation);

      Rigidbody rb3D = spawnedShip.GetComponent<Rigidbody>();
      if (rb3D != null)
      {
         rb3D.AddForce(Vector3.down * addForceSpeed, ForceMode.Impulse);
      }

      Rigidbody2D rb2D = spawnedShip.GetComponent<Rigidbody2D>();
      if (rb2D != null)
      {
         rb2D.AddForce(Vector2.down * addForceSpeed, ForceMode2D.Impulse);
      }
   }

   private Vector3 GetRandomSpawnPosition()
   {
      Collider2D collider2D = spawnpoint.GetComponent<Collider2D>();
      if (collider2D != null)
      {
         Bounds bounds = collider2D.bounds;
         return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            spawnpoint.transform.position.z
         );
      }

      Collider collider3D = spawnpoint.GetComponent<Collider>();
      if (collider3D != null)
      {
         Bounds bounds = collider3D.bounds;
         return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
         );
      }

      Renderer rendererComponent = spawnpoint.GetComponent<Renderer>();
      if (rendererComponent != null)
      {
         Bounds bounds = rendererComponent.bounds;
         return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            spawnpoint.transform.position.z
         );
      }

      return spawnpoint.transform.position;
   }
}
