using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] float jumpTime = 2f;
    [SerializeField] float fallTime = 2f;

    Animator animator;
    bool grounded = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("grounded", grounded);
    }

    // Update is called once per frame
    void Update()
    {
        /// 점프
        if (Input.GetKey(KeyCode.Z) && grounded)
        {
            grounded = false;
            animator.SetBool("jumping", true);
            animator.SetBool("grounded", false);
            animator.SetTrigger("takeOf");
            StartCoroutine(StopJumpAfterSec());
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

    IEnumerator StopJumpAfterSec()
    {
        yield return new WaitForSeconds(jumpTime);

        // 점핑 종료
        animator.SetBool("jumping", false);

        // 떨어지기 시작
        StartCoroutine(StopFallAfterSec());
    }

    IEnumerator StopFallAfterSec()
    {
        yield return new WaitForSeconds(fallTime);

        // 착지
        grounded = true;
        animator.SetBool("grounded", true);
    }
}
