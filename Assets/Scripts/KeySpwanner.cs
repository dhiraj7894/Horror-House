using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeySpwanner : MonoBehaviour
{
    public Transform[] _SpwanPositions;

    [Space(10)]
    public GameObject _GarageKey;
    public GameObject _BasementKey;
    public GameObject _TarraceKey;
    public GameObject _HanumanChalisaBook;
    public GameObject _Fuse1;
    public GameObject _Fuse2;
    public GameObject _CarKey;

    void Start()
    {
        SpwanKey(_CarKey);
        SpwanKey(_TarraceKey);

    }


    public Transform SpwanKey(GameObject obj)
    {
        int keyPosition = Random.Range(0, _SpwanPositions.Length - 1);
        obj.transform.position = _SpwanPositions[keyPosition].position;
        return obj.transform;
    }


}
