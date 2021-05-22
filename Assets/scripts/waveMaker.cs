using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveMaker : MonoBehaviour
{
    public GameObject WaveOne;
    public GameObject WaveTwo;

    GameObject One;
    GameObject Two;

    bool one;

    public void createWave(Vector2 root)
    {
        One = Instantiate(WaveOne, root, Quaternion.identity);
        Two = Instantiate(WaveTwo, root, Quaternion.identity);
        StartCoroutine(grow());
        StartCoroutine(changing());
    }

    IEnumerator grow()
    {
        Vector3 newScale = Vector3.one;
        float step = .08f;
        Vector3 stepUp = new Vector3(0, .022f, 0);
        for (int i = 2; i < 62; i++)
        {
            newScale = new Vector3(newScale.x + step, newScale.y + step, 1);
            One.transform.localScale = newScale;
            Two.transform.localScale = newScale;
            One.transform.position += stepUp;
            Two.transform.position += stepUp;
            yield return null;
        }
    }

    IEnumerator changing()
    {
        for (int i = 2; i < 62; i++)
        {
            if(i%6 == 0)
            {
                One.SetActive(one);
                Two.SetActive(!one);
                one = !one;
            }
            yield return null;
        }
        Destroy(One);
        Destroy(Two);
    }
}
