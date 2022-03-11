using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorldBehaviour : MonoBehaviour
{
    protected Rigidbody rBody;

    public virtual void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }


    void OnBecameInvisible()
    {
        transform.position = GameManager.instance.spawnManager.GetNewPosition(transform.position);

    }
}
