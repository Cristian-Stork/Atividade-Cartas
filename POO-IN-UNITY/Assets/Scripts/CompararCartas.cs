using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompararCartas : MonoBehaviour
{
    public GameManager GameManager;

    private int player1carta;
    private int player2carta;

    private int player1Value;
    private int player2Value;

    private string player1suit;
    private string player2suit;

    private int player1SuitValor;
    private int player2SuitValor;

    [SerializeField] private TextMeshProUGUI cartaSelecionada1Text;
    [SerializeField] private TextMeshProUGUI cartaSelecionada2Text;
    [SerializeField] private TextMeshProUGUI vencedorDaRodadaText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jogar()
    {
        StartCoroutine(Jogando());
    }

    public void SelecionarCarta()
    {
        player1carta = Random.Range(0, GameManager.player1.playersHand.Count);
        player2carta = Random.Range(0, GameManager.player2.playersHand.Count);

        cartaSelecionada1Text.text = "A carta do Player 1 é " + GameManager.player1.playersHand[player1carta].DisplayCardInfo();
        cartaSelecionada2Text.text = "A carta do Player 2 é " + GameManager.player2.playersHand[player2carta].DisplayCardInfo();

        Debug.Log(cartaSelecionada1Text);
        Debug.Log(cartaSelecionada2Text);
    }

    public void ExtrairValores()
    {
        player1Value = ConverterValores(GameManager.player1.playersHand[player1carta].value);
        player2Value = ConverterValores(GameManager.player2.playersHand[player2carta].value);

        player1SuitValor = ConverterNaipe(GameManager.player1.playersHand[player1carta].suit);
        player2SuitValor = ConverterNaipe(GameManager.player2.playersHand[player2carta].suit);

        GameManager.player1.playersHand.Remove(GameManager.player1.playersHand[player1carta]);
        GameManager.player2.playersHand.Remove(GameManager.player2.playersHand[player2carta]);
    }

    int ConverterValores(string value)
    {
        int valueValor;

        switch (value)
        {
            case "As":
                valueValor = 1;
                break;

            case "Valete":
                valueValor = 11;
                break;

            case "Dama":
                valueValor = 12;
                break;

            case "Rei":
                valueValor = 13;
                break;

            case "Coringa":
                valueValor = 99;
                break;

            default:
                valueValor = int.Parse(value);
                break;

        }

        return valueValor;
    }

    int ConverterNaipe(string suit)
    {
        int suitValor;

        switch (suit)
        {
            case "Paus":
                suitValor = 4;
                break;

            case "Copas":
                suitValor = 3;
                break;

            case "Espadas":
                suitValor = 2;
                break;

            case "Ouros":
                suitValor = 1;
                break;

            case "Coringa":
                suitValor = 99;
                break;

            default:
                suitValor = int.Parse(suit);
                break;
        }

        return suitValor;
    }

    public void CompararValores()
    {
        Debug.Log(player1Value);
        Debug.Log(player2Value);
        Debug.Log(player1SuitValor);
        Debug.Log(player2SuitValor);

        if (player1Value > player2Value)
        {
            vencedorDaRodadaText.text = "Player 1 venceu";
            Debug.Log(vencedorDaRodadaText);
        }

        else
        {
            vencedorDaRodadaText.text = "Player 2 venceu";
            Debug.Log(vencedorDaRodadaText);
        }

        if (player1Value == player2Value)
        {
            if (player1SuitValor > player2SuitValor)
            {
                vencedorDaRodadaText.text = "Player 1 venceu";
                Debug.Log(vencedorDaRodadaText);
            }

            else
            {
                vencedorDaRodadaText.text = "Player 2 venceu";
                Debug.Log(vencedorDaRodadaText);
            }

            if (player1SuitValor == player2SuitValor)
            {
                vencedorDaRodadaText.text = "Empate";
                Debug.Log(vencedorDaRodadaText);
            }
        }
    }

    IEnumerator Jogando()
    {
        SelecionarCarta();
        ExtrairValores();
        CompararValores();

        yield return new WaitForSeconds(5);

        SelecionarCarta();
        ExtrairValores();
        CompararValores();

        yield return new WaitForSeconds(5);

        SelecionarCarta();
        ExtrairValores();
        CompararValores();
    }
}
