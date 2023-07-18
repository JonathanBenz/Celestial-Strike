using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadLevelTimer;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] AudioClip explosionSFX;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerControls.disableControls = true;
        explosionVFX.Play();
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().PlayOneShot(explosionSFX, 0.75f);
        DisableMeshRenderers();
        DisableBoxColliders();
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(reloadLevelTimer);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DisableMeshRenderers()
    {
        GameObject parts = GameObject.Find("Parts");
        MeshRenderer[] meshes = parts.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in meshes)
        {
            m.enabled = false;
        }
    }

    void DisableBoxColliders()
    {
        GameObject parts = GameObject.Find("Parts");
        BoxCollider[] colliders = parts.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider c in colliders)
        {
            c.enabled = false;
        }
    }
}
