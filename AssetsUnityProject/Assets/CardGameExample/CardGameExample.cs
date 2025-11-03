using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameExample : MonoBehaviour
{
    public CardPlayer player;
    public CardPlayer cpu;
    public TMPro.TMP_Text playerScoreText;
    public TMPro.TMP_Text cpuScoreText;

    IEnumerator Start()
    {
        refreshScoreText();
        while (true)
        {
            while (!Input.GetKeyDown(KeyCode.D))
            {
                yield return null;
            }
            player.drawCards(1);
            cpu.drawCards(1);
            yield return new WaitForSeconds(1);

            var playerCard = player.getDrawnCard(0);
            var cpuCard = cpu.getDrawnCard(0);
            int result = compareCards(playerCard, cpuCard);

            int winnerScoreChange = 1;

            if (result != 0)
            {
                Debug.Log($"{(result == 1 ? "PLAYER" : "OPPONENT")} WINS HAND");
                
            }
            else
            {
                while (result == 0)
                {
                    winnerScoreChange += 3;
                    yield return null;
                    player.drawCards(3);
                    cpu.drawCards(3);
                    yield return new WaitForSeconds(1.2f);
                    result = compareCards(player.getDrawnCard(2), cpu.getDrawnCard(2));
                    Debug.Log($"{(result == 1 ? "PLAYER" : "OPPONENT")} WINS HAND");
                    yield return new WaitForSeconds(0.5f);
                }
            }

            (result == 1 ? player : cpu).score += winnerScoreChange;
            (result == 1 ? cpu : player).score -= winnerScoreChange;


            refreshScoreText();
            yield return null;
        }
    }

    void refreshScoreText()
    {
        playerScoreText.text = $"Player Score: {player.score}";
        cpuScoreText.text = $"CPU Score: {cpu.score}";
    }

    int compareCards(int playerCard, int cpuCard)
    {
        bool tied = playerCard == cpuCard;
        bool jokered = playerCard == -1 || cpuCard == -1;
        bool draw3 = tied || jokered;

        return draw3 ? 0 : playerCard > cpuCard ?  1 : -1;
    }


    void Update()
    {

    }
}
