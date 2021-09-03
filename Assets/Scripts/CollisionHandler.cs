using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float loadDelay = 1f;
   void OnTriggerEnter(Collider other) 
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        explosionParticles.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke ("ReloadLevel", loadDelay);
        foreach (MeshRenderer meshInChild in GetComponentsInChildren<MeshRenderer>())
        meshInChild.enabled = false;
        foreach (Collider colliderInChild in GetComponentsInChildren<Collider>())
        colliderInChild.enabled = false;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

