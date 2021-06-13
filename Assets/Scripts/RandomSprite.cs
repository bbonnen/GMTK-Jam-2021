using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public SpriteRenderer mySprite;

    [System.Serializable]
    public struct PresentSprite
    {
        public Sprite image;
        public float scale;
    }
    [SerializeField]
    public PresentSprite[] possibleSprites;

    // Start is called before the first frame update
    void Start()
    {
        if (mySprite == null)
            mySprite = GetComponent<SpriteRenderer>();
        if(possibleSprites.Length > 0)
        {
            int i = Random.Range(0, possibleSprites.Length - 1);
            mySprite.sprite = possibleSprites[i].image;
            transform.localScale *= possibleSprites[i].scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
