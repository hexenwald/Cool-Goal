using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float NextLevelDelay = 2f;
    [SerializeField] AudioClip VictorySound;
    [SerializeField] AudioClip DefeatSound;
    [SerializeField] AudioClip SoccerKick;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys();
        if (Input.GetMouseButtonUp(0))
        {
            audioSource.PlayOneShot(SoccerKick);
        }
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Respawn":
                StartCrashSequence(); 
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("DENEME");
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(VictorySound);
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(GetComponent<BezierFollow>());
        Invoke("LoadNextLevel", NextLevelDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(DefeatSound);
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(GetComponent<BezierFollow>());
        Invoke("ReloadLevel", LevelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
