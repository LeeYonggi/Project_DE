using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    private GameObject model = null;

    [SerializeField]
    protected Vector3 moveVector = new Vector3(0, 0, 1);

    public enum Behaviour_State
    {
        START_STATE,
        IDLE_STATE,
        MOVE_STATE,
        ATTACK_STATE,
        DEATH_STATE,
    }

    protected Rigidbody mRigidbody = null;
    protected Animator characterAnimator = null;

    protected Dictionary<Behaviour_State, CharacterBaseState> stateDic = new Dictionary<Behaviour_State, CharacterBaseState>();

    private CharacterStatistics statistics = null;
    private Character targetCharacter = null;

    private Behaviour_State current_State = Behaviour_State.START_STATE;

    protected bool isDestroy = false;

    #region Property
    public Animator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }
    public Rigidbody MRigidbody { get => mRigidbody; set => mRigidbody = value; }
    public Vector3 MoveVector { get => moveVector; set => moveVector = value; }
    public CharacterStatistics Statistics { get => statistics; set => statistics = value; }
    public Character TargetCharacter { get => targetCharacter; set => targetCharacter = value; }
    public GameObject Model { get => model; set => model = value; }
    #endregion

    private void OnEnable()
    {
        //Statistics.notEnoughHpEvent += DeathCharacter;
    }

    private void OnDisable()
    {
        //Statistics.notEnoughHpEvent -= DeathCharacter;
    }


    // 캐릭터 초기화
    protected void CharacterStart(CharacterStatistics initStatistics)
    {
        statistics = initStatistics;

        model = transform.Find("Male Knight 01").gameObject;

        characterAnimator = model.GetComponent<Animator>();
        characterAnimator.SetFloat("moveMent", 0.01f);

        mRigidbody = GetComponent<Rigidbody>();

        stateDic[Behaviour_State.START_STATE] = new BasicStart();
        stateDic[Behaviour_State.IDLE_STATE] = new BasicIdle();
        stateDic[Behaviour_State.MOVE_STATE] = new KnightMove();
        stateDic[Behaviour_State.ATTACK_STATE] = new KnightAttack();
        stateDic[Behaviour_State.DEATH_STATE] = new BasicDeath();

        stateDic[Behaviour_State.START_STATE].Start(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 자체 업데이트
    protected void ObjectUpdate()
    {
        
        if (!isDestroy)
            CollisionUpdate();

        stateDic[current_State].Update(this);


#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            statistics.Hp -= 100;
            if (statistics.Hp <= 0)
                DeathCharacter();
            Debug.Log("!");
#endif
        }
    }

    /// <summary>
    /// 캐릭터의 현재 상태를 바꾼다.
    /// </summary>
    /// <param name="behaviour_State">바꿀 상태</param>
    public void ChangeCurrentState(Behaviour_State behaviour_State)
    {
        if (this.current_State == behaviour_State)
            return;

        stateDic[this.current_State].End(this);

        this.current_State = behaviour_State;

        stateDic[behaviour_State].Start(this);
    }
    
    /// <summary>
    /// 이동할 거리를 구한다.
    /// </summary>
    /// <returns>이동벡터의 크기.</returns>
    public float GetMoveMagnitude()
    {
        Vector3 nextPosition = transform.position + moveVector * Time.deltaTime * statistics.Speed;

        return (nextPosition - transform.position).magnitude; 
    }

    /// <summary>
    /// 충돌처리
    /// </summary>
    protected void CollisionUpdate()
    {
        float maxDistance = 0;
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, statistics.AttackDistance, Vector3.up, maxDistance);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.tag == "Character")
            {
                Character tempCharacter = hit[i].collider.GetComponent<Character>();

                if (tempCharacter == this)
                    continue;
                targetCharacter = tempCharacter;
                ChangeCurrentState(Behaviour_State.ATTACK_STATE);
                break;
            }
        }
    }

    private void DeathCharacter()
    {
        isDestroy = true;
        ChangeCurrentState(Behaviour_State.DEATH_STATE);
    }


    private void OnDrawGizmos()
    {
        float maxDistance = 100;
        RaycastHit hit;

        bool isHit = Physics.SphereCast(transform.position, 2, Vector3.zero,
            out hit, maxDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.0f);
    }

    /// <summary>
    /// 공격받을 때 실행되는 함수
    /// </summary>
    /// <param name="attackingEnemyStat">적 스탯</param>
    public void ComeUnderAttack(CharacterStatistics attackingEnemyStat)
    {
        statistics.Hp = statistics.Hp - attackingEnemyStat.Damage;
    }

    public void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }
}
