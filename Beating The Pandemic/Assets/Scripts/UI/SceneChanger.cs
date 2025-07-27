using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private int sceneNo;
	private GameObject lights;
	public bool FadeOnStart = true;
	private Animator FadeAnimator = null;


	void Start()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		sceneNo = SceneManager.GetActiveScene().buildIndex + 1;
		FadeAnimator = GetComponent<Animator>();
		lights = GameObject.FindGameObjectWithTag("Light");

		if (FadeOnStart == true)
        {
            FadeAnimator.SetTrigger("FadeOut");
        }
    }

    private void Update()
    {
		if (GameObject.FindGameObjectsWithTag("Spawner").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
		{
			SoundManager.PlaySound("lock");
			GetComponent<BoxCollider2D>().enabled = true;
		}
    }

	public void SceneChange()
	{
		SceneManager.LoadScene(sceneNo);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag != "Player") return;

		FadeAnimator.SetTrigger("FadeIn");

		lights.SetActive(false);
	}
}