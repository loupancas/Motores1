using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : Weapon
{
    [Header("Referencias")]
    Transform objectTrans;
    Rigidbody objectRB;

    [Header("Caracteristicas telekineticas")]
    [SerializeField] float smooth;
    [SerializeField] float sensitivescrollY = 1;
    [SerializeField] float minDist = 1;
    [SerializeField] float maxDist = 100;

    [Header("Vars")]
    [SerializeField] bool hooked;
    [SerializeField] bool distObj;
    [SerializeField] float _distObj;

    protected override void Start()
    {
        base.Start();
        ID = 1;
    }
    // Update is called once per frame
    

    public override void Attack()
    {
        base.Attack();
        if (!onground)
        {
            if (Input.GetButtonDown("Fire1") && !hooked)
            {
                DetectionObject();
            }

            if (hooked)
            {
                MoveObject();
            }
            if (Input.GetButtonDown("Fire2") && hooked)
            {
                ThrowObject();
            }
        }
    }

    void DetectionObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //tiro un raycast
        if (Physics.Raycast(ray, out hit, scope, layermask))
        {
            objectTrans = hit.transform;
            objectRB = hit.rigidbody;
            objectRB.useGravity = false;
            hooked = true;
        }
    }
    void MoveObject()
    {
        Vector3 distvector = objectTrans.position - spawnpoint.position;
        if (!distObj)
        {
            _distObj = distvector.magnitude;
            distObj = true;
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            _distObj += Input.mouseScrollDelta.y * sensitivescrollY;
        }
        if (_distObj < minDist || _distObj > maxDist)
        {
            distObj = false;
            hooked = false;
            objectRB.useGravity = true;
        }
        Vector3 newposition = distvector - spawnpoint.forward * _distObj;
        objectTrans.position = Vector3.Lerp(objectTrans.position, objectTrans.position - newposition, smooth * Time.deltaTime);
    }
    void ThrowObject()
    {
        _distObj = 0;
        distObj = false;
        hooked = false;
        objectRB.useGravity = true;
        objectRB.AddForce(transform.forward * strenght, ForceMode.Impulse);
    }
}
