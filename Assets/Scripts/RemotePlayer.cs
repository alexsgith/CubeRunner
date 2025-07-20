using UnityEngine;

public class RemotePlayer : BasePlayer
{
   
    public Vector3 syncedPosition;
    protected override void Update()
    {
        if (playerState == PlayerState.GameOver) return;
        transform.position = Vector3.Lerp(transform.position, syncedPosition, 10f * Time.deltaTime);
    }

    public void SetSyncedPosition(Vector3 pos)
    {
        syncedPosition = pos;
    }
    
}
