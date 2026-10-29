using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float health = 100, maxHealth = 100;
    private float healDelay, healRate = 1f, maxHealDelay = 2f;
    public Volume damageBlur;
    private Vignette vignette;

    private float trashDamage = 32f;
    private float robaloDamage = 6.7f;

    private string currentSceneName;

    private const string TrashTag = "Trash", HookTag = "Hook";
    private const string MenuSceneName = "MainMenu", MangrooveSceneName = "Mangroove";

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if(damageBlur != null) {
            vignette = damageBlur.profile.components.Find(v => v is Vignette) as Vignette;
        }

        ProgressionControler._instance.IsInDangerZone = 0;
        
        health = maxHealth;
    }

    void Update()
    {
        if(string.Equals(currentSceneName, MangrooveSceneName)) // Só chama a função se estiver na cena na qual ela é aplicável
        {
            ManageDangerZone();
        }

        Heal();
    }

    private void Heal()
    {
        if (health >= maxHealth) return;

        if(healDelay > 0) {
            healDelay -= Time.deltaTime;
        } 
        else
        {
            health += healRate * Time.deltaTime;
            if(health > maxHealth) health = maxHealth;

            vignette.intensity.value = -((float)health/(float)maxHealth - 1f);
            // Debug.Log(health);
        }

    }

    private void ManageDangerZone()
    {
        UnityEngine.Debug.Log(ProgressionControler._instance.IsInPolution);
        if(ProgressionControler._instance.IsInPolution) //Not very monkey, but still
        {
            health -= robaloDamage * Time.deltaTime;
            if (health <= 0) Die();
            vignette.intensity.value = 1f - (float)health/(float)maxHealth;
            healDelay = maxHealDelay;
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(HookTag))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag(TrashTag))
        {
            health -= trashDamage;
            if (health <= 0) Die();
            vignette.intensity.value = 1f - (float)health/(float)maxHealth;
            healDelay = maxHealDelay;
        }
    }
}
