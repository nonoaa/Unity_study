using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class SyncObjectPool<T> : ISyncObjectPool<T> where T : PhotonView 
{
    public async Task<T> RequestBy(int id = 0)
    {
        _delay = 0;
        
        while (_prefab == null)
        {
            await Task.Delay(30);
            _delay += 30;

            if (DelayLimit < _delay)
            {
                throw new System.Exception("request time over");
            }
        }

        T item;

        if (_queue.Count.Equals(0))
        {
            item = Object.Instantiate(_prefab);
            
            if (item.TryGetComponent(out IPooledItem pooledItem))
            // if (item is IPooledItem pooledItem)
            {
                pooledItem.Release = () => {
                    Release(item.ViewID);
                    // _account.Remove(item.ViewID);
                    // _queue.Enqueue(item);
                    //
                    // item.ViewID = PhotonNetwork.SyncViewId;
                    // item.gameObject.SetActive(false);
                };
            }
            else
            {
                Object.Destroy(item.gameObject);
                throw new System.Exception("this item type is not [IPooledItem]");
            }
        }
        else
        {
            item = _queue.Dequeue();
        }

        // if (PhotonNetwork.IsMasterClient)
        // {
        //     PhotonNetwork.AllocateViewID(item);
        // }
        if (id.Equals(0))
        {
            PhotonNetwork.AllocateViewID(item);
        }
        else
        {
            item.ViewID = id;
        }
        
        _account.Add(item.ViewID, item);
        item.gameObject.SetActive(true);
        return item;
    }
    
    public void Release(int key)
    {
        var item = _account[key];
        
        _account.Remove(key);
        _queue.Enqueue(item);
    
        item.ViewID = PhotonNetwork.SyncViewId;
        item.gameObject.SetActive(false);
    }

    public bool IsAccounted(int key)
    {
        return _account.ContainsKey(key);
    }
}

public partial class SyncObjectPool<T> where T : PhotonView
{
    private T _prefab;
    private readonly Queue<T> _queue;
    private readonly Dictionary<int, T> _account;
    private const int DelayLimit = 1000;
    private int _delay = 0;

    public SyncObjectPool(string addressName)
    {
        _queue = new Queue<T>();
        _account = new Dictionary<int, T>();
        SetPrefab(addressName);
    }

    private async Task SetPrefab(string addressName)
    {
        var handler = Addressables.LoadAssetAsync<PhotonView>(addressName);

        while (!handler.IsDone)
        {
            Debug.Log($"Loading {addressName} asset...");
            await Task.Delay(30);
        }

        _prefab = handler.Result as T;

        if (_prefab == null)
        {
            throw new System.Exception($"wrong address name = [{addressName}]");
        }
        
        // AtLeastOneInPool();
    }

    // private void AtLeastOneInPool()
    // {
    //     var item = Object.Instantiate(_prefab);
    //
    //     item.gameObject.SetActive(false);
    //     _queue.Enqueue(item);
    // }
}


