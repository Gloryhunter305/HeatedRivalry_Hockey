using UnityEngine;

public class funnyFace : MonoBehaviour
{
    public SpriteRenderer face;
    public SpriteRenderer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        face.transform.position = player.transform.position;
    }
}
