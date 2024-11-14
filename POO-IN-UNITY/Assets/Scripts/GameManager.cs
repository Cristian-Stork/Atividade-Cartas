using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor; // Importante para usar a UI

public class GameManager : MonoBehaviour
{
    public Deck deck;  // Referência ao baralho
    public string player1Name = "Jogador 1";  // Nome do primeiro jogador
    public string player2Name = "Jogador 2";  // Nome do segundo jogador
    [HideInInspector] public Player player1;  // Instância do primeiro jogador
    [HideInInspector] public Player player2;  // Instância do segundo jogador

    public GameObject player1Object;  // Referência ao Jogador 1
    public GameObject player2Object;  // Referência ao Jogador 2

    public GameObject cardPrefab;  // Prefab para as cartas
    public GameObject cardTextPrefab;  // Prefab for the text

    public CompararCartas compararCartas;

    bool coringaVermelho = false;
    bool coringaPreto = false;
    bool segundoCoringa = false;


    // Método chamado ao iniciar o jogo
    void Start()
    {

        // Verifique se o deck foi atribuído
        if (deck == null)
        {
            Debug.LogError("Deck is not assigned in the inspector!");
            return; // Saia do método se o deck não estiver atribuído
        }


        // Cria instâncias dos jogadores
        player1 = new Player(player1Name);
        player2 = new Player(player2Name);
        StartGame();  // Chama o método para iniciar o jogo
    }

    // Método que inicializa o jogo
    void StartGame()
    {
        //TestLoadJson();
       // Carrega cartas do arquivo JSON
        deck.LoadCards(Resources.Load<TextAsset>("deck"));// Verifique se o nome do arquivo está correto
        Debug.Log(deck.cards.Count); // Verifica quantas cartas foram carregadas

        // Certifique - se de que cartas foram carregadas
        if (deck.cards.Count == 0)
        {
            Debug.LogError("No cards available in the deck. Check the JSON file.");
            return; // Saia do método se não houver cartas
        }

        // Distribui 3 cartas para o jogador 1
        List<Card> cardsPlayer1 = deck.Distribute(3);
        player1.GetCards(cardsPlayer1);
        Debug.Log($"{player1.name} recebeu: {string.Join(", ", cardsPlayer1.ConvertAll(c => c.DisplayCardInfo()))}");

        // Distribui 3 cartas para o jogador 2
        List<Card> cardsPlayer2 = deck.Distribute(3);
        player2.GetCards(cardsPlayer2);
        Debug.Log($"{player2.name} recebeu: {string.Join(", ", cardsPlayer2.ConvertAll(c => c.DisplayCardInfo()))}");

     
        // Exibir as cartas na cena
        ShowCards(cardsPlayer1, player1Object.transform.position);
        ShowCards(cardsPlayer2, player2Object.transform.position);

        compararCartas.Jogar();
    }

    void ShowCards(List<Card> cards, Vector3 playerPosition)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            // Instanciar a carta na posição do jogador
            GameObject cardObj = Instantiate(cardPrefab, playerPosition + new Vector3(-(0 + i * 0.3f), 2.7f + i * 0.5f, 0), Quaternion.Euler(0, -90, 0));
            cardObj.name = cards[i].DisplayCardInfo();  // Nome da carta

            SpriteRenderer cardObjSprite = cardObj.GetComponent<SpriteRenderer>();

            if (cardObj.name == "Coringa de Coringa" && segundoCoringa == false)
            {
                Debug.Log("Identificou a carta");
                switch (Random.Range(1, 3))
                {
                    case 1:
                        cardObjSprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Cartas/Preto Coringa.png");
                        coringaPreto = true;
                        Debug.Log("Escolheu a preta");
                        break;

                    case 2:
                        cardObjSprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Cartas/Vermelho Coringa.png");
                        coringaVermelho = true;
                        Debug.Log("Escolheu a vermelha");
                        break;
                }

                segundoCoringa = true;
            }

            else if (cardObj.name == "Coringa de Coringa" && segundoCoringa == true)
            {
                Debug.Log("Identificou a segunda carta");
                if (coringaPreto == false)
                {
                    cardObjSprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Cartas/Preto Coringa.png");
                }

                if (coringaVermelho == false)
                {
                    cardObjSprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Cartas/Vermelho Coringa.png");
                }
            }

            else
            {
                cardObjSprite.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Cartas/" + cards[i].DisplayCardInfo() + ".png");
            }
        }
    }

    void TestLoadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("deck");
        if (jsonFile != null)
        {
            Debug.Log(jsonFile.text);  // Mostra o conteúdo do JSON
            CardsData data = JsonUtility.FromJson<CardsData>(jsonFile.text);
            if (data.cards != null)
            {
                Debug.Log($"Cards loaded: {data.cards.Count}");
                foreach (var card in data.cards)
                {
                    Debug.Log($"{card.value} de {card.suit}");
                }
            }
            else
            {
                Debug.LogError("Cards are null after deserialization.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
}
