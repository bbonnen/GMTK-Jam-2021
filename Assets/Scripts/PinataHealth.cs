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
    public Rigidbody2D myRig;

    //havokk
    AudioSource audiosource;
    public ParticleSystem particle;
    public float particleTime = 2f;

    public ParticleSystem particleDeath;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        audiosource = GetComponent<AudioSource>();
        particle.Stop();
        if (myRig == null)
            myRig = GetComponent<Rigidbody2D>();
        myRig.AddForce(Vector2.right, ForceMode2D.Force);
        
    }

    public void GotHit(float damage = 0)
    {
        if (damage == 0)
            damage = damagePerHit;
        currentHealth -= damage;
        healthBar.localScale = new Vector3(Mathf.Lerp(0, maxHealthScale, currentHealth / maxHealth), healthBar.localScale.y, healthBar.localScale.z);

        particle.transform.position = transform.position;
        particle.Play();
        StartCoroutine(KillParticle());

        if (currentHealth <= 0)
        {
            GameManager.Instance.PinataDied();
            dead = true;
            audiosource.Play();
            particleDeath.Play();
        }

    }

    private void Update()
    {
        if (dead)
            particleDeath.transform.position = transform.position;
    }

    IEnumerator KillParticle()
    {
        yield return new WaitForSeconds(particleTime);
        particle.Stop();
    }
}
