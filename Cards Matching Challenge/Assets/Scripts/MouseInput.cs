using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseInput : MonoBehaviour
{
    public FlipData flipData;

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    public Rigidbody cardRB;

    private void Start()
    {
        cardRB = this.GetComponent<Rigidbody>();
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
    }

    public void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0) ){
            cardRB.useGravity = false;
            cardRB.isKinematic = true;

            initialPosition = transform.localPosition;
            finalPosition = transform.localPosition + (Vector3.up * flipData.maxHeigth);

            StartCoroutine(RiseCard());
        }
    }
}
