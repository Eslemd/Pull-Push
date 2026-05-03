using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuKontrol : MonoBehaviour
{
    // Bu fonksiyonu butonumuza bağlayacağız
    public void OyunaBasla()
    {
        // Tırnak içine asıl oyun sahnende yazan ismi BİREBİR aynı şekilde yazmalısın.
        // Örn: "GameScene", "Level1" vb.
        SceneManager.LoadScene("Tutorial"); // Buraya kendi sahne ismini yazmalısın
    }

    // Ekstra: İleride Çıkış butonu da yapmak istersen diye kodunu buraya bırakıyorum
    public void OyundanCik()
    {
        Debug.Log("Oyundan çıkıldı!"); // Unity editöründe çalıştığını görmek için
        Application.Quit(); // Gerçek oyunda oyunu kapatır
    }
}