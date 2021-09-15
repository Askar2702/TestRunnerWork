using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private const string RUN = "IsRun";
    private const string PUNCH = "Punch";


    private void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerController.instance.Run += EnableRunningAnimation;
    }

    private void EnableRunningAnimation(bool activ)
    {
        _animator.SetBool(RUN, activ);
    }

    public void Attack()
    {
        _animator.SetTrigger(PUNCH);
    }
}
