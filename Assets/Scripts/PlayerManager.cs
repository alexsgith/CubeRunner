using UnityEngine;

public class PlayerManager : BasePlayer
{
    protected override void Update()
    {
        base.Update();

        if (playerState == PlayerState.Jump) return;

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }
}