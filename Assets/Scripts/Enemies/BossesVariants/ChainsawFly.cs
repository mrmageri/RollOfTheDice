using Enemies;
using UnityEngine;

public class ChainsawFly : Enemy
{
    private Vector3 _target;
    [SerializeField] private float _distance = 4f;
    private int _maxActions = 5;
    private int actionNum = 0;
    private void Start()
    {
        SetUnclickable();
        var position = targetTransform.position;
        position = new Vector3(position.x + _distance,position.y, position.z);
        _target = position;
    }

    protected override void Moving()
    {
        if (transform.position != _target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
            if(animator.GetBool("isWalking") == false) animator.SetBool("isWalking", true);
            return;
        }
        if (isMovingBack)
        {
            MovingBack();
            return;
        }
        
        if (actionNum == 0)
        {
            animator.SetBool("isWalking", false);
            actionNum = Random.Range(1, _maxActions);
            animator.SetInteger("attack", actionNum);
        }
    }

    protected override void EventOnDeathExtra()
    {
            
    }

    protected override void onHitEffect()
    {
        animator.SetInteger("attack", 0);
    }

    public void SetClickable()
    {
        isClickable = true;
    }
    
    public void SetUnclickable()
    {
        isClickable = false;
    }

    public void SetAnimatorZero()
    {
        animator.SetInteger("attack", 0);
    }

    public void SetAnimatorRandom()
    {
        animator.SetBool("isWalking", false);
        actionNum = Random.Range(1, _maxActions);
        animator.SetInteger("attack", actionNum);
    }
}
