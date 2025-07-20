using System;
using System.Collections;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public Action NewFloorTriggered;
    public Action GameOver;
    public Action<int> ScoreUpdated;

    protected ParticleSystem hitParticle;

    [Header("Player Properties")]
    [SerializeField] protected float initialSpeed = 3;
    [SerializeField] protected float speedMultiplier = .5f;
    [SerializeField] protected int collectablePoint = 5;
    [SerializeField] protected int distancePoint = 1;

    protected Vector3 initialPosition;
    protected float jumpForce = 5f;
    protected Rigidbody rb;
    protected PlayerState playerState;

    protected int orbScore = 0;
    protected int distanceScore = 0;

    protected virtual void Awake()
    {
        initialPosition = transform.localPosition;
        playerState = PlayerState.GameOver;
        rb = GetComponent<Rigidbody>();
        hitParticle = transform.GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Update()
    {
        if (playerState == PlayerState.GameOver) return;

        MoveForward();
        UpdateScore();
    }

    protected void MoveForward()
    {
        transform.position += new Vector3(0, 0, initialSpeed) * Time.deltaTime;
    }

    protected void UpdateScore()
    {
        distanceScore = (int)transform.position.z * distancePoint;
        ScoreUpdated?.Invoke(GetScore());
    }

    protected void Jump()
    {
        playerState = PlayerState.Jump;
        Vector3 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FloorTrigger"))
        {
            NewFloorTriggered?.Invoke();
            initialSpeed += speedMultiplier;
        }
        else if (other.CompareTag("Orb"))
        {
            hitParticle.Stop();
            hitParticle.Play();
            orbScore += collectablePoint;
            other.gameObject.SetActive(false);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && playerState != PlayerState.GameOver)
            playerState = PlayerState.Run;
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            playerState = PlayerState.GameOver;
            StartCoroutine(CallGameOver());
        }
    }

    public void StartPlay()
    {
        playerState = PlayerState.Run;
        orbScore = distanceScore = 0;
        ScoreUpdated?.Invoke(GetScore());
    }

    public int GetScore() => distanceScore + orbScore;

    protected void ResetPlayer()
    {
        initialSpeed = 3;
        transform.localPosition = initialPosition;
    }

    protected IEnumerator CallGameOver()
    {
        yield return new WaitForSeconds(1);
        GameOver?.Invoke();
        yield return null;
        ResetPlayer();
    }
}
public enum PlayerState
{
    Run,
    Jump,
    GameOver
}