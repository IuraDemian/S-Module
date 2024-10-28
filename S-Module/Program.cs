using System;
using System.Collections.Generic;

// Викоростовував Патерн Прототип, він дозволив створити нові об'єкти карт шляхом клонування вже існуючих,
// це спростило створення нових карт та уникнув проблему з копіюванням обєктів вручну.

public class Card : ICloneable
{
    public string Suit { get; set; }
    public string Value { get; set; }

    public Card(string suit, string value)
    {
        Suit = suit;
        Value = value;
    }

    public object Clone()
    {
        return new Card(Suit, Value);
    }
    public override bool Equals(object obj)
    {
        if (obj is Card otherCard)
        {
            return this.Suit == otherCard.Suit && this.Value == otherCard.Value;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Value);
    }
}

public class Deck
{
    private List<Card> cards = new List<Card>();

    public Deck()
    {
        string[] suits = { "Пiка", "Бубна", "Трефа", "Чирва" };
        string[] values = { "Туз", "Король", "Дама", "Валет" };

        foreach (var suit in suits)
        {
            foreach (var value in values)
            {
                cards.Add(new Card(suit, value));
            }
        }
    }

    public List<Card> GenerateRandomCards(int count)
    {
        const int maxUniqueCards = 16;
        count = Math.Min(count, maxUniqueCards);

        Random random = new Random();
        HashSet<Card> uniqueCards = new HashSet<Card>();

        while (uniqueCards.Count < count)
        {
            int index = random.Next(cards.Count);
            Card card = (Card)cards[index].Clone();
            uniqueCards.Add(card);
        }

        return new List<Card>(uniqueCards);
    }
}

class Program
{
    static void Main()
    {
        Deck deck = new Deck();
        List<Card> randomCards = deck.GenerateRandomCards(4);

        foreach (Card card in randomCards)
        {
            Console.WriteLine($"Карта - {card.Value}, {card.Suit}");
        }
    }
}