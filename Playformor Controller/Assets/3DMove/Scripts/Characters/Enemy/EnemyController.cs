
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    protected Animator animator;
    [SerializeField]
    protected StateMechine[] states;
    GameObject player;
    NavMeshAgent navMeshAgent;
    Rigidbody rigidBody;

    private bool isOver = false;
    public float maxSpeed { get; set; }
    private float curSpeed = 0;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //stateTable = new Dictionary<System.Type, IState>(states.Length);
        foreach (var item in states) {
            
        }
        maxSpeed = 0;
    }

    private void Update() {
        if (!isOver) {
            UpdateAnimator();
        }

    }


    private void UpdateAnimator() {
        curSpeed = Mathf.MoveTowards(curSpeed,maxSpeed,1f);
        //Vector3 speed = navMeshAgent.velocity;
        //Vector3 localVelocity = transform.InverseTransformDirection(speed);
        GetComponent<Animator>().SetFloat("forwardSpeed", curSpeed);

    }
    int lastIndex = -1;

    public void AttackAnim() {
        isOver = true;
        rigidBody.velocity = Vector3.zero;
        transform.LookAt(player.transform);
        animator.ResetTrigger("stopAttack");
        animator.SetTrigger("attack");
    }

    public void Cancel() {
        isOver = true;
        rigidBody.isKinematic = false;
        animator.SetTrigger("stopAttack");
        GetComponent<Animator>().SetFloat("forwardSpeed", 0);
    }

    public void MoveToPos(Vector3 pos,int wayPointIndex) {
        if(lastIndex != wayPointIndex && lastIndex != -1) {
            if (!isRotate) {
                targetRotation += 180f;
                StartCoroutine(RotateObj());
            }
        }
        lastIndex = wayPointIndex;
        //rigidBody.velocity = new Vector3(maxSpeed, rigidBody.velocity.y);
        rigidBody.MovePosition(pos);
        //SetVelocityX(maxSpeed);
    }
    bool isRotate = false;
    public float rotationSpeed = 30.0f;
    public float targetRotation = 90.0f;
    IEnumerator RotateObj() {
        isRotate = true;
        float startRotation = transform.rotation.eulerAngles.y;
        float elapsedTime = 0.0f;

        while (elapsedTime < 10.0f) {
            // 计算插值角度
            float currentRotation = Mathf.LerpAngle(startRotation, targetRotation, elapsedTime);

            // 应用旋转
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);

            elapsedTime += Time.deltaTime * rotationSpeed;

            yield return null;
        }
        rigidBody.transform.rotation = Quaternion.Euler(0, targetRotation, 0);

        isRotate = false;
    }

    public void SetVelocityX(float velocityX) {
        
    }



}
