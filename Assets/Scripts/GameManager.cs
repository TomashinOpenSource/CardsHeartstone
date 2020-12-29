using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform cardsHolder;
    [SerializeField] private GameObject cardPrefab;
    private List<Card> cards;

    void Start()
    {
        SpawnCards();
    }

    void Update()
    {
        
    }

    public void SpawnCards()
    {
        int count = Random.Range(4, 7);
        count = 5;
        cards = new List<Card>();

        Vector2 size = cardPrefab.GetComponent<RectTransform>().sizeDelta;
        Vector2 step = size * 2 / 3;

        for (int i = 0; i < count; i++)
        {
            Card card = Instantiate(cardPrefab, cardsHolder).GetComponent<Card>();
            card.title.text = "Карта #" + (i + 1);
            card.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (step * i);
            int multipleAngle = count / 2 - i;
            card.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.forward * 10 * multipleAngle);
            cards.Add(card);
        }

        cardsHolder.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (size + step / 2);
    }
}
