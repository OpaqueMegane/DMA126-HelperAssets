using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public int fullDeckSize = 12;
    public GameObject deck;
    public int score = 0;
    public SpriteRenderer card0;
    public SpriteRenderer card1;
    public SpriteRenderer card2;
    SpriteRenderer[] animCards;

    public KeyCode drawKey = KeyCode.D;
    public KeyCode drawKey3 = KeyCode.F;
    Animator animator;
    public List<Sprite> sprites = new List<Sprite>();
    void Start()
    {
        score = fullDeckSize;
        animator = GetComponentInChildren<Animator>();
        animCards = new SpriteRenderer[] { card0, card1, card2 };
    }

    public void drawCards(int nToDraw)
    {
        for (int i = 0; i < animCards.Length; i++)
        {
            animCards[i].sprite = sprites[Random.Range(0, sprites.Count)];
            animCards[i].transform.parent.localEulerAngles = Vector3.up * ((Random.Range(0, 2) * 180 ) + Random.Range(-6f, 6f));
        }
        animator.SetTrigger("draw-card-" + nToDraw);

        UpdateDeckScale();
    }


    void UpdateDeckScale()
    {
        deck.transform.localScale = new Vector3(1, Mathf.InverseLerp(0, 12, score),1);
        deck.gameObject.SetActive(score > 0);
    }
    public string getDrawnCardName(int index)
    {
        if (index == 0) return card0.sprite.name;
        else if (index == 1) return card1.sprite.name;
        else if (index == 2) return card2.sprite.name;
        return null;
    }
    public int getDrawnCard(int index)
    {
        var playerCard = getDrawnCardName(index);
        int playerCardVal = playerCard == "skull" ? 0 : playerCard == "joker" ? -1 : int.Parse(playerCard);
        return playerCardVal;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(drawKey))
    //    {
    //        drawCards(1);
    //    }

    //    if (Input.GetKeyDown(drawKey3))
    //    {
    //        drawCards(3);
    //    }
    //}
}
