using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour , IEnemy
{
    [SerializeField] private Animator _animator;
    public void TakeDamage()
    {
        _animator.SetTrigger("Death");
        StartCoroutine(SelfDestroy());
    }
     
    private IEnumerator SelfDestroy()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        SelfDisable();
    }

    public void SelfDisable()
    {
        gameObject.SetActive(false);
    }
   
}
