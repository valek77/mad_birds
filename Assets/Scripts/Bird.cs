using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 startPosition;
    private Vector3 startPosition3;
    private Quaternion startRotation;


    [SerializeField]
    float forza=700;

    [SerializeField]
    float maxDragDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;
        startPosition = rb.position;
        startRotation = gameObject.transform.rotation;
        startPosition3 = new Vector3(startPosition.x, startPosition.y, transform.position.z);

    }


    private void OnMouseDown()
    {
       sr.color = Color.red;
    }

    private void OnMouseUp()
    {
        Vector2 direction = startPosition - rb.position  ;
        direction.Normalize();
        rb.isKinematic = false;
        rb.AddForce(direction * forza);
        //rb.freezeRotation = false;

        sr.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desideredPosition = mousePosition;

       


        if (Vector2.Distance(desideredPosition, startPosition) > maxDragDistance) 
        {
            Vector2 direction =   desideredPosition - startPosition;
            direction.Normalize();

            desideredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desideredPosition.x > startPosition.x)
            desideredPosition.x = startPosition.x;

        Vector3 desideredPosition3 = new Vector3(desideredPosition.x, desideredPosition.y, transform.position.z);
        if(Vector2.Distance(startPosition, desideredPosition)>1)
            Debug.DrawLine(startPosition3, desideredPosition3, Color.red, 2.5f, true);


        transform.position = desideredPosition3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay() {

        while(rb.velocity!= Vector2.zero)
            yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(2);

        rb.isKinematic = true;
        rb.position = startPosition;
        rb.velocity = Vector2.zero;
        gameObject.transform.rotation = startRotation;
    }
}
