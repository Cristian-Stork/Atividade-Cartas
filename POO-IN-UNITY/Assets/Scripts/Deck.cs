using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();  // Lista que contém as cartas do baralho

    // Método que carrega cartas de um arquivo JSON
    public void LoadCards(TextAsset jsonFile)
    {

        if (jsonFile == null)
        {
            Debug.LogError("JSON file is null. Check if the file is assigned in the Inspector.");
            return;
        }

        Debug.Log($"JSON Content: {jsonFile.text}"); // Mostra o conteúdo do JSON


        // Usa JsonUtility para converter o JSON em um objeto CartasData
        CardsData data = JsonUtility.FromJson<CardsData>(jsonFile.text);

        if (data.cards == null || data.cards.Count == 0)
        {
            Debug.LogError("No cards loaded from the JSON file. Please check the file format.");
            return;
        }

        cards.AddRange(data.cards);  // Adiciona as cartas carregadas à lista
        Shuffle();  // Embaralha as cartas após carregar
        Debug.Log($"Loaded {data.cards.Count} cards.");

    }

    // Método que embaralha as cartas
    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];  // Armazena a carta atual
            int randomIndex = Random.Range(i, cards.Count);  // Gera um índice aleatório
            cards[i] = cards[randomIndex];  // Troca a carta atual pela carta em randomIndex
            cards[randomIndex] = temp;  // Coloca a carta armazenada na posição aleatória
        }
    }

    // Método que distribui um número específico de cartas
    public List<Card> Distribute(int numCartas)
    {
        if (numCartas > cards.Count)
        {
            Debug.LogError("Not enough cards to distribute.");
            numCartas = cards.Count;  // Ajusta para o número disponível
        }

        List<Card> cartasDistribuidas = cards.GetRange(0, numCartas);  // Pega as cartas do início da lista
        cards.RemoveRange(0, numCartas);  // Remove as cartas distribuídas do baralho
        return cartasDistribuidas;  // Retorna as cartas distribuídas
    }
}

// Classe auxiliar para representar a estrutura do JSON
[System.Serializable]
public class CardsData
{
    public List<Card> cards;  // Lista de cartas carregadas do JSON
}