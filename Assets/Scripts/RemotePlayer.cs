using System;
using UnityEngine;

public class RemotePlayer : BasePlayer
{
   
    private Vector3 syncedPosition;
    [SerializeField]private int score;
    
    protected override void Update()
    {
        if (playerState == PlayerState.GameOver) return;
        Vector3 target  = new Vector3(transform.localPosition.x, transform.localPosition.y, syncedPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, 50*Time.deltaTime);
    }

    public void SetSyncedPosition(SyncState state)
    {
        Debug.LogWarning("received state: " + state.playerState);
        //x position should be taken from the local player
        syncedPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,state.position.z);
        score = state.playerScore;
        if (Enum.TryParse(state.playerState, out PlayerState result))
        {
            if (result==PlayerState.Jump && playerState != PlayerState.Jump)
                Jump();
            else if (result==PlayerState.GameOver && playerState != PlayerState.GameOver)
            {
                ResetPlayer();
                playerState = PlayerState.GameOver;
                GameOver?.Invoke();
                ResetPlayer();
            }
            playerState = result;
        }
        else
            Debug.LogWarning("Invalid enum state: " + state.playerState);
    }
    
}
