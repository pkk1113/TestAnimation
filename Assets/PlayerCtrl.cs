using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] float takeOfTime = 0.2f;
    [SerializeField] float jumpTime = 2f;
    [SerializeField] float fallTime = 2f;

    Animator animator;
    bool _grounded = true;
    bool _jumping = false;
    bool grounded
    {
        get => _grounded;
        set
        {
            _grounded = value;
            animator.SetBool("grounded", _grounded);
        }
    }
    bool jumping
    {
        get => _jumping;
        set
        {
            _jumping = value;
            animator.SetBool("jumping", _jumping);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("grounded", grounded);
        animator.SetBool("jumping", jumping);
    }

    // Update is called once per frame
    void Update()
    {
        /// 점프
        if (Input.GetKey(KeyCode.Z) && grounded)
        {
            animator.SetTrigger("takeOf");
            StartCoroutine(StopTakeOfAfterSec());
        }

        /// 이동
        var xAxis = (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f) + (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        if (xAxis == 0f)
        {
            animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", true);
        }

        /// 공격
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger("attack");
        }

        /// 대쉬
        if (Input.GetKey(KeyCode.C))
        {
            animator.SetTrigger("dash");
        }
    }

    IEnumerator StopTakeOfAfterSec()
    {
        yield return new WaitForSeconds(takeOfTime);

        // 점핑 시작
        grounded = false;
        jumping = true;

        // 떨어지기 시작
        StartCoroutine(StopJumpAfterSec());
    }

    IEnumerator StopJumpAfterSec()
    {
        yield return new WaitForSeconds(jumpTime);

        // 점핑 종료
        jumping = false;

        // 떨어지기 시작
        StartCoroutine(StopFallAfterSec());
    }

    IEnumerator StopFallAfterSec()
    {
        yield return new WaitForSeconds(fallTime);

        // 착지
        grounded = true;
    }
}
