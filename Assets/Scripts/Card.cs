using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [Header("Арт")]
    [SerializeField] private Image artZone;
    [SerializeField] private Vector2 artSize;
    [SerializeField] private bool isSaveOnDevice;
    private Sprite art;

    [Header("Параметры")]
    public CardParameter attack;
    public CardParameter hp;
    public CardParameter mana;

    [Header("Текстовые поля")]
    public TMP_Text title;
    public TMP_Text description;

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
        attack.Value = Random.Range(1, 5);
        mana.Value = Random.Range(1, 5);
        hp.Value = Random.Range(5, 10);
    }

    void Update()
    {
        
    }

    public void LoadImage()
    {
        string url = "https://picsum.photos/" + artSize.x + "/" + artSize.y;
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

            if (isSaveOnDevice)
            {
                byte[] bytes = texture.EncodeToPNG();
                string name = texture.name + ".png";
                File.WriteAllBytes(Application.persistentDataPath + name, bytes);
            }
        }
        if (title.text == "") title.text = url;
        description.text = "Рандомная картинка с " + url;
    }
}

[System.Serializable]
public class CardParameter
{
    [SerializeField] private TMP_Text field;
    [SerializeField] private int value;

    public TMP_Text Field { get => field; set => field = value; }
    public int Value
    { 
        get => value;
        set
        {
            this.value = value;
            Field.text = value.ToString();
            Field.transform.DOShakeScale(1);
        }
    }
}