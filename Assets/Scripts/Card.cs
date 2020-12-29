using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Арт")]
    [SerializeField] private Image artZone;
    [SerializeField] private int imageWidth, imageHeight;
    private Sprite art;

    #region Свойства
    public Sprite Art
    { 
        get => art; 
        set
        {
            art = value;
            artZone.sprite = value;
        }
    }
    #endregion

    void Start()
    {
        LoadImage();
    }

    void Update()
    {
        
    }

    public void LoadImage()
    {
        string url = "https://picsum.photos/" + imageWidth + "/" + imageHeight;
        StartCoroutine(LoadingImage(url));
    }
    private IEnumerator LoadingImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) Debug.Log(request.error);
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Art = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
