using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

 
[System.Serializable]
public class PoolingObjects
{
    public GameObject pooledPrefab;
    public int count;
    public int chance;
}

public enum ObjectPoolType
{
    PROYECTILE = 0,
    ASTEROID_SMALL,
    ASTEROID_MEDIUM,
    ASTEROID_BIG

}

public class PoolingController : MonoBehaviour
{
    public List<PoolingObjects> poolingObjectsClass;
  
    List<GameObject> pooledObjectsList = new List<GameObject>();

    public static PoolingController instance;  

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        CreateNewList();      
    }

    void CreateNewList()
    {
        for (int i = 0; i < poolingObjectsClass.Count; i++)  
        {
            for (int k = 0; k < poolingObjectsClass[i].count; k++)
            {
                GameObject newObj = Instantiate(poolingObjectsClass[i].pooledPrefab, transform);
                pooledObjectsList.Add(newObj);
                newObj.SetActive(false);
            }
        }
    }


    public GameObject GetPoolingObject(GameObject prefab)    
    {
        string cloneName = Utils.GetCloneName(prefab);
        for (int i = 0; i < pooledObjectsList.Count; i++)
        {
            if (!pooledObjectsList[i].activeSelf && pooledObjectsList[i].name == cloneName)
            {
                return pooledObjectsList[i];
            }
        }
        return AddNewObject(prefab);                       
    }

    public GameObject GetPoolingObject(ObjectPoolType typePoolObject)
    {
        return GetPoolingObject(poolingObjectsClass[(int)typePoolObject].pooledPrefab);
    }

    public GameObject GetWeightedPoolObject()
    {
        return GetPoolingObject(Utils.GetChancePool<PoolingObjects>(poolingObjectsClass).pooledPrefab);
    }

    public void DisablePoolingObjects()
    {
        for (int i = 0; i < pooledObjectsList.Count; i++)
        {
            pooledObjectsList[i].SetActive(false);
        }

    }

    GameObject AddNewObject(GameObject prefab)          
    {
        GameObject newObj = Instantiate(prefab, transform);
        pooledObjectsList.Add(newObj);
        newObj.SetActive(false);
        return newObj;
    }

  
}
