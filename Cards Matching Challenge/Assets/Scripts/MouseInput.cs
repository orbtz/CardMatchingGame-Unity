using System.Collections;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public FlipData flipData;

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    public Rigidbody cardRB;
    public bool canClick;

    public CardMatchCheck cardCheck;
    public CardData cardData;

    private void Start()
    {
        cardRB = GetComponent<Rigidbody>();
        cardData = GetComponent<CardData>();
        canClick = true;
    }

    IEnumerator RiseCard()
    {
        while (Vector3.Distance(transform.position, finalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, finalPosition, flipData.upforceSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(FlipCard());
    }

    IEnumerator FlipCard()
    {
        Quaternion flipValue = transform.localRotation * Quaternion.Euler(0, 0, 180);

        while (Quaternion.Angle(transform.rotation, flipValue) >= 0.01f) { 
            transform.localRotation = Quaternion.Slerp(transform.localRotation, flipValue, flipData.flipSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(SinkCard());
    }

    IEnumerator SinkCard()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition, flipData.upforceSpeed * Time.deltaTime);
            yield return null;
        }

        cardRB.useGravity = true;
        cardRB.isKinematic = false;

        canClick = true;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canClick && cardData.isActiveToFlip)
        {

            if (cardCheck.firstCard == null || cardCheck.secondCard == null || cardCheck.thirdCard == null)
            {

                if (cardCheck.firstCard == null)
                {
                    cardCheck.firstCard = this.gameObject;
                }
                else if (cardCheck.secondCard == null)
                {
                    cardCheck.secondCard = this.gameObject;
                } else
                {
                    cardCheck.thirdCard = this.gameObject;
                }

                canClick = false;
                cardData.isActiveToFlip = false;

                cardRB.useGravity = false;
                cardRB.isKinematic = true;

                initialPosition = transform.localPosition;
                finalPosition = transform.localPosition + (Vector3.up * flipData.maxHeigth);

                StartCoroutine(RiseCard());
            }
        }
    }
}
