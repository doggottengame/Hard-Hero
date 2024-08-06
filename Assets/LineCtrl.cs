using UnityEngine;

public class LineCtrl : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetTrigger("Boom");
    }
}
