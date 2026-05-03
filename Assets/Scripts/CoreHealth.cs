using UnityEngine;
using UnityEngine.UI; // Image kullanmak için gerekli

public class CoreHealth : MonoBehaviour
{
    
    [Header("UI Bağlantısı")]
    public Image canBariGorseli; // Yaptığın yeşil/mavi barı buraya bağlayacağız
    void Update()
    {
        float mesafe = transform.position.magnitude;
        mesafe = Vector3.Distance(Vector3.zero, transform.position);
        canBariGorseli.fillAmount = 1.2f-(mesafe/5f); // Mesafeyi 0-1 aralığına dönüştür

    }
}