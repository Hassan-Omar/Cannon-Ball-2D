using UnityEngine;
using System.Collections;

public class SmartTool : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Hit", true);
        StartCoroutine("InverStatus");
    }

    IEnumerator InverStatus()
    {
        yield return new WaitForSeconds(clip.length);
        animator.SetBool("Hit", false);
        animator.SetBool("Hit2", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Hit2", false);
    }
}
