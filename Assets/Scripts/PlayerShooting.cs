using UnityEngine;
using System.Collections;
using TMPro; 

public class PlayerShooter : MonoBehaviour
{
    [Header("UI & Managers")]
    public ScoreManager scoreManager; 
    public TextMeshProUGUI ammoText;  

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip emptyClickSound;

    [Header("Settings")]
    public int maxAmmo = 12;
    public float reloadDuration = 2.6f;
    
    private int currentAmmo;
    private bool isReloading = false; 

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI(); 
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetButtonDown("Fire1") && !isReloading)
            {
                if (currentAmmo > 0) 
                {
                    Shoot();
                }
                else 
                {
                    audioSource.PlayOneShot(emptyClickSound);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
            {
                StartCoroutine(ReloadRoutine());
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI(); 
        audioSource.PlayOneShot(shootSound);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.CompareTag("Bullseye"))
            {
                scoreManager.AddScore(25);
            }
            else if (hit.collider.CompareTag("Target"))
            {
                scoreManager.AddScore(10);
            }
        }
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true; 
        audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadDuration);

        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        isReloading = false; 
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null) 
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }
}