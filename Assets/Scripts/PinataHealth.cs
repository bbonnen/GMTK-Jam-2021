using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }
    public float damagePerHit = 10f;

    public float maxHealthScale = 1f;
    public Transform healthBar;

    //havokk
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        audiosource = GetComponent<AudioSource>();
    }

    public void GotHit(float damage = 0)
    {
        if (damage == 0)
            damage = damagePerHit;
        currentHealth -= damage;
        healthBar.localScale = new Vector3(Mathf.Lerp(0, maxHealthScale, currentHealth / maxHealth), healthBar.localScale.y, healthBar.localScale.z);
        if (currentHealth <= 0)
        {
            GameManager.Instance.PinataDied();
            audiosource.Play();
        }

    }
}
