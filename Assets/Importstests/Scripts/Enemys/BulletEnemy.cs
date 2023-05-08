using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    public LayerMask playerMask;

    private void Update()
    {
        bulletMove();
    }

    void bulletMove()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == playerMask)
        {
            Destroy(gameObject);
        }
    }

}
