using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public GameObject deck;
    public int score = 0;
    public SpriteRenderer card0;
    public SpriteRenderer card1;
    public SpriteRenderer card2;
    SpriteRenderer[] animCards;

    Animator animator;
    public List<Sprite> sprites = new List<Sprite>();
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animCards = new SpriteRenderer[] { card0, card1, card2 };
    }

    public void drawCards(int nToDraw)
    {
        for (int i = 0; i < animCards.Length; i++)
        {
            animCards[i].sprite = sprites[Random.Range(0, sprites.Count)];
            
            //randomize card rotation for a more naturalistic look
            //animCards[i].transform.parent.localEulerAngles = Vector3.up * ((Random.Range(0, 2) * 180 ) + Random.Range(-6f, 6f));
        }
        animator.SetTrigger("draw-card-" + nToDraw);
    }

    public int getDrawnCard(int index)
    {
        var playerCard = animCards[index].sprite.name;
        int playerCardVal = playerCard == "skull" ? 0 : playerCard == "joker" ? -1 : int.Parse(playerCard);
        return playerCardVal;
    }
}
