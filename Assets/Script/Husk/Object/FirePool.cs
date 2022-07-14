using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePool : MonoBehaviour
{
    [SerializeField]
    private GameObject fireObjectPrefab;

    [SerializeField]
    private Queue<FiringObject> fireItemPool = new Queue<FiringObject>();


    private void Start()
    {
        AddFireItem(2);
    }

    public FiringObject GetFireItem()
    {
        if(fireItemPool.Count <= 0) 
        {
            AddFireItem(2);
        }

        var item = fireItemPool.Dequeue();
        item.transform.localPosition = Vector3.zero;
        item.transform.SetParent(null);
        item.gameObject.SetActive(true);

        return item;
    }

    public void ReturnItem(FiringObject returnObj)
    {
        returnObj.gameObject.SetActive(false);
        returnObj.transform.SetParent(this.transform);
        returnObj.transform.localPosition = Vector3.zero;

        fireItemPool.Enqueue(returnObj);
    }

    private void AddFireItem(int makecount)
    {
        for(int i = 0; i < makecount; i++)
        {
            var item = GameObject.Instantiate(fireObjectPrefab).GetComponent<FiringObject>();
            item.SetFirePool = this;

            ReturnItem(item);
        }
    }
}
