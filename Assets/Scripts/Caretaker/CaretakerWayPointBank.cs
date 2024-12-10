using HorroHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerWayPointBank : Singleton<CaretakerWayPointBank>
{
    [SerializeField] private List<Transform> BasementWaypoint = new List<Transform>();
    [SerializeField] private List<Transform> GroundWaypoint = new List<Transform>();
    [SerializeField] private List<Transform> FlooroneWaypoint = new List<Transform>();
    [SerializeField] private List<Transform> TarreceWaypoint = new List<Transform>();

    public List<Transform> Waypoints = new List<Transform>();

    private void Start()
    {
        CombineWaypoints();
    }
    public void CombineWaypoints()
    {
        Waypoints.AddRange(BasementWaypoint);
        Waypoints.AddRange(GroundWaypoint);
        Waypoints.AddRange(FlooroneWaypoint);
        //Waypoints.AddRange(TarreceWaypoint);
    }
    int lastIndex;
    public Transform GetWaypoint()
    {
        int index = 0;
        Transform currentTransform;
        index = Random.Range(0, Waypoints.Count);
        if (index == lastIndex)
        {
            currentTransform = GetWaypoint();
        }
        else
        {
            currentTransform = Waypoints[index];
        }
        return currentTransform;
    }
}
