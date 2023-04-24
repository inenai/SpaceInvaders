using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour
{

    [SerializeField] GameObject lifeIcon;

    Image[] lifeIcons;

    public void Setup(int lives)
    {
        lifeIcons = new Image[lives];

        for (int i = 0; i < lives; i++)
        {
            GameObject go = Instantiate(lifeIcon, transform);
            lifeIcons[i] = go.GetComponent<Image>();
        }
    }

    public void RemoveLife()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }

}
