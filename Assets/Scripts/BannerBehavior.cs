using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerBehavior : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float alpha;
    // Start is called before the first frame update
    void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Max(alpha - 0.005f, 0.0f) ;
        sprite.color = new Color(1f, 1f, 1f, alpha);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z);
        if (alpha <= 0.005f)
            sprite.enabled = false;
    }
}
