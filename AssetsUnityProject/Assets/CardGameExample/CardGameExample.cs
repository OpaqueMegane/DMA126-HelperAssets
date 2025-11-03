using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameExample : MonoBehaviour
{
    public CardPlayer player;
    public CardPlayer cpu;
    public TMPro.TMP_Text playerScoreText;
    public TMPro.TMP_Text cpuScoreText;
    public TMPro.TMP_Text resultText;
    public GameObject gameEndUI;

    void Start()
    {
        RunGame();
    }
    public void RunGame()
    {
        StartCoroutine(RunGameRoutine());
    }
    IEnumerator RunGameRoutine()
    {
        if (gameEndUI != null)
        {
            gameEndUI.gameObject.SetActive(false);
        }
        player.score = 12;
        cpu.score = 12;
        
        resultText.text = "";
        refreshScoreText();
        while (true)
        {
            
            while (!Input.GetKeyDown(KeyCode.D))
            {
                yield return null;
            }
            resultText.text = "";
            player.drawCards(1);
            cpu.drawCards(1);
            yield return new WaitForSeconds(1);

            var playerCard = player.getDrawnCard(0);
            var cpuCard = cpu.getDrawnCard(0);
            int result = decideWinner(playerCard, cpuCard);

            int winnerScoreChange = 1;

            while (result == 0)
            {
                resultText.text = "TIE! DRAW X3";
                winnerScoreChange += 3;
                yield return null;
                player.drawCards(3);
                cpu.drawCards(3);
                yield return new WaitForSeconds(1.2f);
                resultText.text = "";
                result = decideWinner(player.getDrawnCard(2), cpu.getDrawnCard(2));
                yield return new WaitForSeconds(0.5f);
            }

            if (result != 0)
            {
                string winner = "???";
                if (result == 1) //player wins
                {
                    winner = "PLAYER";
                }
                else
                {
                    winner = "OPPONENT";
                }
                
                string winResultText = winner + " WINS THE ROUND";
                resultText.text = winResultText;

  
            }
            

            (result == 1 ? player : cpu).score += winnerScoreChange;
            (result == 1 ? cpu : player).score -= winnerScoreChange;

            refreshScoreText();

            //check if someone has won the game, and end play
            bool gameOver = false;
            if (player.score <= 0)
            {
                resultText.text = "OPPONENT WINS!";
                gameOver = true;
        
            }
            else if (cpu.score <= 0)
            {
                resultText.text = "PLAYER WINS!";
                gameOver = true;
            }

            if (gameOver)
            {
                gameEndUI.SetActive(true);
                yield break;
            }

        }
    }

    void refreshScoreText()
    {
        playerScoreText.text = $"Player Score: {player.score}";
        cpuScoreText.text = $"CPU Score: {cpu.score}";
    }

    /// <summary>
    /// given 2 card values, decide who wins that play. 
    /// </summary>
    /// <param name="playerCard"></param>
    /// <param name="cpuCard"></param>
    /// <returns>1 is the player, -1 is the opponent, 0 is a tie.</returns>
    int decideWinner(int playerCard, int cpuCard)
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
