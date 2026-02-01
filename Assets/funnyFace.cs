using UnityEngine;
using System.Collections;

public class funnyFace : MonoBehaviour
{
    public SpriteRenderer face;
    public SpriteRenderer player;
    bool emoting;

    public Sprite neutral;
    public Sprite happy;
    public Sprite angry;
    public Sprite kissy;
    public Sprite hurt;

    Vector3 laugh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        face.transform.position = player.transform.position + laugh;
    }
    public IEnumerator Angry()
    {
        if (emoting == false)
        {
            emoting = true;
            while (emoting == true)
            {

                face.sprite = angry;
                for (int i = 0; i < 3; i++)
                {
                    face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 20);
                    yield return new WaitForSeconds(0.35f);
                    face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -20);
                    yield return new WaitForSeconds(0.35f);
                }

                face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                face.sprite = neutral;
                emoting = false;
                yield return null;

            }
        }
        else
            yield return null;
    }
    public IEnumerator Happy()
    {
        if (emoting == false)
        {
            emoting = true;
            while (emoting == true)
            {
                int r = Random.Range(1, 6);
                if (r == 5)
                    face.sprite = kissy;
                else
                    face.sprite = happy;
                for (int i = 0; i < 3; i++)
                {
                    laugh = new Vector3(0,0.3f,0);
                    yield return new WaitForSeconds(0.15f);
                    laugh = new Vector3(0, 0, 0);
                    yield return new WaitForSeconds(0.15f);
                }

                face.sprite = neutral;
                emoting = false;
                yield return null;

            }
        }
        else
            yield return null;
    }
    public IEnumerator Kissy()
    {
        if (emoting == false)
        {
            emoting = true;
            face.sprite = kissy;
            for (int i = 0; i < 2; i++)
            {
                face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 20);
                yield return new WaitForSeconds(0.35f);
                face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -20);
                yield return new WaitForSeconds(0.35f);
            }
            face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            face.sprite = neutral;
            emoting = false;
            yield return null;
        }
        else
            yield return null;
    }

    public IEnumerator Hurt()
    {
        if (emoting == false)
        {
            emoting = true;
            face.sprite = hurt;
            for (int i = 0; i < 100; i++)
            {
                face.gameObject.transform.localRotation *= Quaternion.Euler(0, 0, 3);
                yield return new WaitForSeconds(0.01f);
            }
            face.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            face.sprite = neutral;
            emoting = false;
            yield return null;
        }
        else
            yield return null;
    }
}
