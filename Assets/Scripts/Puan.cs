using TMPro;
using UnityEngine;

public class Puan : MonoBehaviour
{
   public TextMeshProUGUI puanText;
   private float toplamPuan;

   public TextMeshProUGUI score;

   public TextMeshProUGUI highscore;

   private const string HighScoreKey = "HighScore";
   private int enYuksekPuan;

    private void Start()
    {
        toplamPuan = 0f;
        enYuksekPuan = PlayerPrefs.GetInt(HighScoreKey, 0);
        HighScoreGuncelle();
    }

   private void Update()
    {
        float mesafe = transform.position.magnitude;
        mesafe = Vector3.Distance(Vector3.zero, transform.position);
        if (mesafe < 3f)
        {
            toplamPuan += Time.deltaTime * 25f;
            ScoreGuncelle();
        } 
    }

    private void ScoreGuncelle()
    {
        int mevcutPuan = Mathf.FloorToInt(toplamPuan);

        if (puanText != null)
        {
            puanText.text = mevcutPuan.ToString();
        }

        if (score != null)
        {
            score.text = "Score: " + mevcutPuan;
        }

        if (mevcutPuan > enYuksekPuan)
        {
            enYuksekPuan = mevcutPuan;
            PlayerPrefs.SetInt(HighScoreKey, enYuksekPuan);
            PlayerPrefs.Save();
            HighScoreGuncelle();
        }
    }

    private void HighScoreGuncelle()
    {
        if (highscore != null)
        {
            highscore.text = "High Score: " + enYuksekPuan;
        }
    }

}

