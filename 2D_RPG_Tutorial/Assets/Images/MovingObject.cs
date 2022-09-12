using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
