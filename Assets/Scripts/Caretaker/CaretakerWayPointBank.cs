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
    [SerializeField] private List<Transform> CustomPositions = new List<Transform>();

    public List<Transform> Waypoints = new List<Transform>();

    private void Start()
    {
        CombineWaypoints();
    }
    public void CombineWaypoints()
    {
        Waypoints.AddRange(GroundWaypoint);
        Waypoints.AddRange(FlooroneWaypoint);
    }

    public void AddWaypoints(int count)
    {
        if (count == 0) Waypoints.AddRange(BasementWaypoint);
        if (count == 1) Waypoints.AddRange(TarreceWaypoint);
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
        Debug.Log($"<color=red> Waitpoint Loaded </color>");
        return currentTransform;
    }

    public Transform GetCustomWaypoint(int id)
    {
        return CustomPositions[id];
    }
}
