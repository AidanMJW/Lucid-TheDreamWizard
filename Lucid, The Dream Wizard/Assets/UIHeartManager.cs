using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartManager : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        updateHearts();
    }

    void updateHearts()
    {
        foreach (GameObject heart in hearts)
            heart.SetActive(false);

        for(int i = 0; i < player.getHealth(); i++)
        {
            if (hearts[i] != null)
                hearts[i].SetActive(true);
        }
    }
}
