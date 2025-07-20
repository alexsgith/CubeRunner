using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDataReceive : MonoBehaviour
{
    
    public static RemoteDataReceive Instance { get; private set; }
    [SerializeField]RemotePlayer remotePlayer;
    private Queue<SyncState> syncBuffer = new Queue<SyncState>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PassStateData(SyncState state)
    {
        Debug.Log("Send state: " + state.playerState);
        syncBuffer.Enqueue(state);
        remotePlayer.SetSyncedPosition(state);
        syncBuffer.Dequeue();
    }
}
public struct SyncState
{
    public Vector3 position;
    public int playerScore;
    public String playerState;
    public SyncState(Vector3 pos, int score, string state)
    {
        position = pos;
        playerScore = score;
        playerState = state;
    }
}
