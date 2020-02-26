using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private static ProjectilePool _instance;

    private Dictionary<string, GameObject[]> shotCollection;
    private Dictionary<string, int> indexCollection;


    public static ProjectilePool instance
    {
        get
        {
            if (_instance == null) _instance = new ProjectilePool();
            return _instance;
        }
    }

    private ProjectilePool()
    {
        shotCollection = new Dictionary<string, GameObject[]>();
        indexCollection = new Dictionary<string, int>();
    }

    public void CreateShotPool(string name, int size, GameObject shot)
    {
        if (!shotCollection.ContainsKey(name))
        {
            GameObject poolParent = new GameObject("_" + name + "Pool");
            GameObject[] thePool = new GameObject[size];
            GameObject temp = null;
            for (int i = 0; i < size; i++)
            {
                temp = GameObject.Instantiate(shot);
                temp.transform.parent = poolParent.transform;
                temp.SetActive(false);
                thePool[i] = temp;
            }
            shotCollection.Add(name, thePool);
            indexCollection.Add(name, 0);
        }
    }


    public GameObject GetNextShot(string poolName)
    {
        GameObject result = null;
        if(shotCollection.ContainsKey(poolName))
        {
            GameObject[] pool = shotCollection[poolName];
            int curIndex = indexCollection[poolName];
            curIndex = curIndex % shotCollection[poolName].Length;
            int lookupMax = shotCollection[poolName].Length;
            int lookupCount = 0;
            while (lookupCount < lookupMax)
            {
                curIndex = curIndex % shotCollection[poolName].Length;
                if(!pool[curIndex].activeInHierarchy)
                {
                    result = pool[curIndex];
                    curIndex += 1;
                    break;
                }
                else
                {
                    lookupCount++;
                    curIndex++;
                }
            }
            indexCollection[poolName] = curIndex;
        }


        return result;
    }
}
