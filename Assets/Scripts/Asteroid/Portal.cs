using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 0.5f) {
            ChangePosition();
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>() != null) {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            transform.Rotate(0, 0, 90.0f * Time.deltaTime * rotateSpeed);
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()   
    {
        newPosition = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    }
}