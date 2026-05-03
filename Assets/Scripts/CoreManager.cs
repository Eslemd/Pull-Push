using UnityEngine;

public class CoreManager : MonoBehaviour
{

    public static CoreManager Instance;

    private AhenkManager _ahenkManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _ahenkManager = FindFirstObjectByType<AhenkManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Harmonie_zone"))
        {
            _ahenkManager.OnZoneStateChanged(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Harmonie_zone"))
        {
            _ahenkManager.OnZoneStateChanged(false); 
        }
    }
}
