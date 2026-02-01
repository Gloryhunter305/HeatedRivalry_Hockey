using UnityEngine;

public class kiss : MonoBehaviour
{
    public GameObject flashBang;
    public GameObject expand;
    public AudioSource boom;

    Vector3 bases = new Vector3(0.85f, 0.85f, 0.85f);
    int timer = 600;


    void FixedUpdate()
    {
        timer++;
        if (timer == 20)flashBang.SetActive(false);
        expand.transform.localScale = bases + new Vector3(timer/20f, timer/20f, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && timer >+ 600)
        {
            expand.transform.localScale = bases;
            timer = 0;
            flashBang.SetActive(true);
            boom.Play();
        }
    }
}
