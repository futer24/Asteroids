using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;
    private float destroySeconds = 2f;

    public void FractureObject()
    {
        GameObject fracturedInstance = Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        fracturedInstance.transform.localScale = transform.localScale;
        foreach (var item in fractured.GetComponentsInChildren<Rigidbody>())
        {
            item.AddExplosionForce(10, transform.position, 5f, 3.0F);
        }
        Destroy(fracturedInstance, destroySeconds);
        StartCoroutine(Utils.DelayedAction(() =>
        {
            gameObject.SetActive(false);
        }
        ,2f));
      
    }


}
