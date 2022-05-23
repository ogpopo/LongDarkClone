using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;
    private float _animationBlend;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void EnablingWalkingAnimation()
    {
        _animator.SetFloat("Speed_f", 0.6f);
    }
    
    public void DeEnablingWalkingAnimation()
    {
        _animator.SetFloat("Speed_f", 0.4f);
    }

    private void ManagingBasicAnimations()
    {
 
    }
}
