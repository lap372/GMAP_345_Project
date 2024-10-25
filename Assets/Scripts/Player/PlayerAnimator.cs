using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

  

    private const string IS_WALKING = "IsWalking";
    //strings are bad, this gives a compiler error incase we make a mistake and we now dont haveto spell IsWalking perfectly and spray&pray,
    //errors will appear
    //"XxxXxxx" needs to be same ass in animator. XX_XXXXXX needs to be same as in void Update

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
