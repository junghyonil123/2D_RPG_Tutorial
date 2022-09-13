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
        while (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0) //�����ϳ��� �������ִٸ� ��������� �ڵ带���� �ִϸ��̼��� �ѹ��� ������� �ε巴����
        {
            //����Ű�� ��������� ����
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
                vector.y = 0; //x�� 0�� �ƴҶ� y�� �ʱ�ȭ���� �����ϳ��� �����ϵ�����
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            for (currentWalkCount = 0; currentWalkCount < walkCount; currentWalkCount++)
            {
                if (isRun)
                {
                    currentWalkCount++; //�޸����ִٸ� ī��Ʈ�� �ι�� �������� ��ĭ�� �����̵��� �����.
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
