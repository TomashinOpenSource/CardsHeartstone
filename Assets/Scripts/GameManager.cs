using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Карты")]
    [SerializeField] private Transform cardsHolder;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Card> cards;


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
        cards = new List<Card>();

        for (int i = 0; i < count; i++)
        {
            Card card = Instantiate(cardPrefab, cardsHolder).GetComponent<Card>();
            card.title.text = "Карта #" + (i + 1);
            cards.Add(card);
        }
        SetCardsPositions();
    }

    public void SetCardsPositions()
    {
        Vector2 width = cardPrefab.GetComponent<RectTransform>().sizeDelta;
        Vector2 step = width * 2 / 3;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<RectTransform>().anchoredPosition += Vector2.right * (step * i);
            int multipleAngle = cards.Count / 2 - i;
            cards[i].GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.forward * 10 * multipleAngle);
        }

        cardsHolder.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (width + step / 2);
    }

    public void ChangeValues()
    {
        StartCoroutine(ChangingValues());
    }
    private IEnumerator ChangingValues()
    {
        foreach (var card in cards)
        {
            yield return new WaitForSeconds(1);
            card.parameters[Random.Range(0, card.parameters.Length)].Value -= Random.Range(-2, 9);
        }
    }
}
