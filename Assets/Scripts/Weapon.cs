using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : InfiniteWorldBehaviour
{
    public float speed = 40f;

    protected float deadTime = 1f;

    public abstract void Shoot(Vector3 direction);

    private void OnEnable()
    {

        StartCoroutine(Utils.DelayedAction(() =>
        {
            gameObject.SetActive(false);
        }, deadTime
        ));
    }


    private void OnTriggerEnter(Collider other) => gameObject.SetActive(false);


}
