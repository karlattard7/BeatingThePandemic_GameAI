using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
