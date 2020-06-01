using UnityEngine;

public class CardData : MonoBehaviour
{
    public string cardName;
    public int cardGroup;

    // Start is called before the first frame update
    void Start()
    {
        Card card = new Card
        {
            name = cardName,
            cardGroup = cardGroup
        };

    }

}
