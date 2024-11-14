[System.Serializable]
public class Card  // Remover MonoBehaviour
{
    public string suit;  // Naipe da carta (ex: Copas, Ouros)
    public string value;  // Valor da carta (ex: Ás, 2, 3)

    // Método que retorna uma representação em string da carta
    public string DisplayCardInfo()
    {
        return $"{suit} de {value}";
    }
}
