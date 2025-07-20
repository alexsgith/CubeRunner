using UnityEngine;

public class PlayerManager : BasePlayer
{
    private int prevPosZ;
    private PlayerState lastSentState = PlayerState.GameOver;

    protected override void Update()
    {
        if (playerState == PlayerState.GameOver) return;

        base.Update();
        
        if (playerState != PlayerState.Jump)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                || Input.GetMouseButtonDown(0))
            {
                Jump();              
                SendSyncState();     
            }
        }
        
        int curZtick = (int)(transform.localPosition.z * 10);
        if (curZtick > prevPosZ)
        {
            SendSyncState();
            prevPosZ = curZtick;
        }
        
        if (playerState != lastSentState)
        {
            SendSyncState();
        }
    }

    public override void StartPlay()
    {
        base.StartPlay();
        SendSyncState();
    }

    protected override void ResetPlayer()
    {
        base.ResetPlayer();
        prevPosZ = (int)(transform.localPosition.z * 10);
        lastSentState = playerState;
        SendSyncState();
    }

    private void SendSyncState()
    {
        SyncState state = new(transform.localPosition, GetScore(), playerState.ToString());
        RemoteDataReceive.Instance.PassStateData(state);
        lastSentState = playerState;
    }
}