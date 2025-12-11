using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance;

    [Header("Sound Clips")]
    public AudioClip playerFootstep;
    public AudioClip enemyFootstep;
    public AudioClip collisionSound;

    [Header("Volume Settings")]
    public float playerFootstepVolume = 0.5f;
    public float enemyFootstepVolume = 0.4f;
    public float collisionVolume = 0.7f;

    private AudioSource playerAudioSource;
    private AudioSource enemyAudioSource;
    private AudioSource collisionAudioSource;
    private Transform player;
    private Vector2 playerLastPosition;
    private Coroutine playerCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        playerAudioSource = gameObject.AddComponent<AudioSource>();
        playerAudioSource.playOnAwake = false;

        enemyAudioSource = gameObject.AddComponent<AudioSource>();
        enemyAudioSource.playOnAwake = false;

        collisionAudioSource = gameObject.AddComponent<AudioSource>();
        collisionAudioSource.playOnAwake = false;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerLastPosition = player.position;
        }
    }

    private void Update()
    {
        HandlePlayerFootsteps();
        HandleEnemyFootsteps();
    }

    private void HandlePlayerFootsteps()
    {
        if (player == null || playerFootstep == null)
            return;

        Vector2 currentPosition = player.position;
        bool isMoving = Vector2.Distance(currentPosition, playerLastPosition) > 0.001f;

        if (isMoving && !playerAudioSource.isPlaying)
        {
            playerCoroutine = StartCoroutine(PlayPlayerFootstep());
        }
        else if (!isMoving && playerAudioSource.isPlaying)
        {
            StopPlayerFootstep();
        }

        playerLastPosition = currentPosition;
    }

    private IEnumerator PlayPlayerFootstep()
    {
        playerAudioSource.clip = playerFootstep;
        playerAudioSource.volume = playerFootstepVolume;
        playerAudioSource.Play();
        yield return new WaitForSeconds(playerFootstep.length);
    }

    private void StopPlayerFootstep()
    {
        if (playerCoroutine != null)
            StopCoroutine(playerCoroutine);
        
        playerAudioSource.Stop();
        playerAudioSource.clip = null;
    }

    private void HandleEnemyFootsteps()
    {
        if (enemyFootstep == null)
            return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0 && !enemyAudioSource.isPlaying)
        {
            StartCoroutine(PlayEnemyFootstep());
        }
    }

    private IEnumerator PlayEnemyFootstep()
    {
        enemyAudioSource.clip = enemyFootstep;
        enemyAudioSource.volume = enemyFootstepVolume;
        enemyAudioSource.Play();
        yield return new WaitForSeconds(enemyFootstep.length);
    }

    public void PlayCollision()
    {
        if (collisionSound == null)
            return;

        collisionAudioSource.PlayOneShot(collisionSound, collisionVolume);
    }
}