using UnityEngine;

public class CardData : MonoBehaviour
{
    public string cardName;
    public int cardGroup;

    public bool isActiveToFlip = true;
    public bool isActiveToPlay = true;

    public void SetCardInactiveToPlay()
    {
        isActiveToPlay = false;
    }

}
