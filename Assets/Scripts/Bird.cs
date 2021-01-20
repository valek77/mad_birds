using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;
        startPosition = rb.position;
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
        rb.AddForce(direction * 500);

        sr.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
