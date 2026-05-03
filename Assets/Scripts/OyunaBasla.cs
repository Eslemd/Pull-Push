using UnityEngine;
using UnityEngine.SceneManagement; 

public class OyunaBasla : MonoBehaviour
{
    // Bu fonksiyonu butonumuza bağlayacağız
    public void OyunaBaslaa()
    {
        // Tırnak içine asıl oyun sahnende yazan ismi BİREBİR aynı şekilde yazmalısın.
        // Örn: "GameScene", "Level1" vb.
        SceneManager.LoadScene("SampleScene"); // Buraya kendi sahne ismini yazmalısın
        Debug.Log("Oyuna Başlandı!"); // Unity editöründe çalıştığını görmek için
    }

    // Ekstra: İleride Çıkış butonu da yapmak istersen diye kodunu buraya bırakıyorum
}