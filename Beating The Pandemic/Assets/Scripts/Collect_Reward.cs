using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Reward : MonoBehaviour
{
    public GameObject reward;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("CollectReward");
        }
    }

    IEnumerator CollectReward()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(reward);
    }
}
