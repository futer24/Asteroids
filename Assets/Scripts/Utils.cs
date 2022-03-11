using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 

public class Utils : MonoBehaviour
{

    public static Vector3 GetRandomTorque(float torqueValue)
    {
        Vector3 torque;
        torque.x = UnityEngine.Random.Range(-torqueValue, torqueValue);
        torque.y = UnityEngine.Random.Range(-torqueValue, torqueValue);
        torque.z = UnityEngine.Random.Range(-torqueValue, torqueValue);
        return torque;

    }

    public static string GetCloneName(GameObject prefab)
    {
        return prefab.name + "(Clone)";
    }

    public static IEnumerator DelayedAction(UnityAction callback, float seconds = 3f)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public static T GetChancePool<T>(List<T> list) where T : PoolingObjects
    {
        float totalWeight = 0;
        for (int i = 0; i < list.Count; i++)
        {
            T current = list[i];
            totalWeight += current.chance;
        }

        float randomWeight = UnityEngine.Random.Range(0, totalWeight);
        float currentWeight = 0;
        T result = default(T);

        for (int i = 0; i < list.Count; i++)
        {
            T current = list[i];
            currentWeight += current.chance;
            if (currentWeight > randomWeight)
            {
                return current;
               
            }
        }
        return result;
    }


}
