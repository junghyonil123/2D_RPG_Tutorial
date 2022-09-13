using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float speed;
    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool isRun;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0) //둘중하나라도 눌리고있다면 계속적으로 코드를실행 애니메이션을 한번만 실행시켜 부드럽게함
        {
            //방향키가 눌렷을경우 실행
            canMove = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                isRun = true;
            }
            else
            {
                applyRunSpeed = 0;
                isRun = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0; //x가 0이 아닐때 y를 초기화시켜 둘중하나만 동작하도록함
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            for (currentWalkCount = 0; currentWalkCount < walkCount; currentWalkCount++)
            {
                if (isRun)
                {
                    currentWalkCount++; //달리고있다면 카운트를 두배로 증가시켜 한칸만 움직이도록 만든다.
                }

                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                yield return new WaitForSeconds(0.02f);
            }

        }


        animator.SetBool("Walking", false);
        canMove = true;
    }

    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && canMove)
        {
            StartCoroutine(MoveCoroutine());
        }
    }
}
