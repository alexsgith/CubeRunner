using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]PlayerManager playerScript;
    private LinkedList<Transform> floorsPool = new();
    private List<Vector3> initialFloor = new();
    private float floorDistance;

    private void OnEnable()
    {
        playerScript.NewFloorTriggered += NewFloorTriggered;
        playerScript.GameOver += ResetFloor;
    }

    private void OnDisable()
    {
        playerScript.NewFloorTriggered -= NewFloorTriggered;
        playerScript.GameOver -= ResetFloor;
    }

    private void Start()
    {
        foreach (Transform floor in transform)
        {
            floorsPool.AddLast(floor);
            initialFloor.Add(floor.localPosition);
        }
        floorDistance = floorsPool.First.Value.localScale.z;
    }

    public void NewFloorTriggered()
    {
        Transform prevFloor = floorsPool.First.Value;
        floorsPool.RemoveFirst();
        foreach (Transform orb in prevFloor.GetComponentsInChildren<Transform>(true)) 
        {
            if (orb.CompareTag("Orb")) orb.gameObject.SetActive(true);
        }
        
        Vector3 newPos = prevFloor.localPosition;
        newPos.z = floorsPool.Last.Value.localPosition.z + floorDistance;
        prevFloor.localPosition = newPos;
        floorsPool.AddLast(prevFloor);
    }

    void ResetFloor()
    {
        for (int i = 0; i < floorsPool.Count; i++)
        {
            Transform prevFloor = floorsPool.First.Value;
            floorsPool.RemoveFirst();
            foreach (Transform orb in prevFloor.GetComponentsInChildren<Transform>(true)) 
            {
                if (orb.CompareTag("Orb")) orb.gameObject.SetActive(true);
            }
            Vector3 newPos = initialFloor[i];
            newPos.z = i * floorDistance;
            prevFloor.localPosition = newPos;
            floorsPool.AddLast(prevFloor);
        }
    }
}
