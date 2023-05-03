using System.Collections;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{

    [SerializeField] private GameObject lifeIcon;

    public void Setup(int lives)
    {
        StartCoroutine(Initialize(lives));       
    }

    private IEnumerator Initialize(int lives)
    {
        bool hasLives = true;
        while (hasLives) {
            hasLives = RemoveLife();
            yield return null;
        }

        for (int i = 0; i < lives; i++)
        {
            Instantiate(lifeIcon, transform);
        }
    }

    public bool RemoveLife()
    {
        if (transform.childCount > 0)
        { 
            Destroy(transform.GetChild(0).gameObject);
            return true;
        }
        return false;
    }

}
