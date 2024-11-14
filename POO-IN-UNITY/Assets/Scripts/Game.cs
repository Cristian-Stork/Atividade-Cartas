using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Deck deck;  // Referência ao baralho
    public string player1Name = "Jogador 1";  // Nome do primeiro jogador
    public string Player2Name = "Jogador 2";  // Nome do segundo jogador
    private Player player1;  // Instância do primeiro jogador
    private Player player2;  // Instância do segundo jogador

    // Start is called before the first frame update
    void Start()
    {
        // Cria instâncias dos jogadores
        player1 = new Player(player1Name);
        player2 = new Player(Player2Name);
        StartGame();  // Chama o método para iniciar o jogo
    }

    // Método que inicializa o jogo
    void StartGame()
    {
        // Carrega cartas do arquivo JSON
        deck.LoadCards(Resources.Load<TextAsset>("deck"));

        // Distribui 3 cartas para o jogador 1
        List<Card> cardsPlayer1 = deck.Distribute(3);
        player1.GetCards(cardsPlayer1);
        Debug.Log($"{player1.name} recebeu: {string.Join(", ", cardsPlayer1.ConvertAll(c => c.DisplayCardInfo()))}");

        // Distribui 3 cartas para o jogador 2
        List<Card> cardsPlayer2 = deck.Distribute(3);
        player2.GetCards(cardsPlayer2);
        Debug.Log($"{player2.name} recebeu: {string.Join(", ", cardsPlayer2.ConvertAll(c => c.DisplayCardInfo()))}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
