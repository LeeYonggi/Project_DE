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
        ATTACK_STATE
    }

    protected Rigidbody mRigidbody = null;
    protected Animator characterAnimator = null;

    protected CharacterBaseState startState = null;
    protected CharacterBaseState idleState = null;
    protected CharacterBaseState moveState = null;
    protected CharacterBaseState attackState = null;
    private CharacterStatistics statistics = null;
    private Character targetCharacter = null;

    private Behaviour_State behaviour_State = Behaviour_State.START_STATE;

    protected bool isDestroy = false;

    #region Property
    public Animator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }
    public Rigidbody MRigidbody { get => mRigidbody; set => mRigidbody = value; }
    public Vector3 MoveVector { get => moveVector; set => moveVector = value; }
    public CharacterStatistics Statistics { get => statistics; set => statistics = value; }
    public Character TargetCharacter { get => targetCharacter; set => targetCharacter = value; }
    public GameObject Model { get => model; set => model = value; }
    #endregion

    // ĳ���� �ʱ�ȭ
    protected void CharacterStart(CharacterStatistics initStatistics)
    {
        statistics = initStatistics;

        model = transform.Find("Male Knight 01").gameObject;

        characterAnimator = model.GetComponent<Animator>();
        characterAnimator.SetFloat("moveMent", 0.01f);

        mRigidbody = GetComponent<Rigidbody>();

        startState = new BasicStart();
        idleState = new BasicIdle();
        moveState = new KnightMove();
        attackState = new KnightAttack();

        startState.Start(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ��ü ������Ʈ
    protected void ObjectUpdate()
    {
        CollisionUpdate();

        switch (behaviour_State)
        {
            case Behaviour_State.START_STATE:
                startState.Update(this);
                break;
            case Behaviour_State.IDLE_STATE:
                idleState.Update(this);
                break;
            case Behaviour_State.MOVE_STATE:
                moveState.Update(this);
                break;
            case Behaviour_State.ATTACK_STATE:
                attackState.Update(this);
                break;
        }
    }


    /// <summary>
    /// ĳ������ ���� ���¸� �ٲ۴�.
    /// </summary>
    /// <param name="behaviour_State">�ٲ� ����</param>
    public void ChangeCurrentState(Behaviour_State behaviour_State)
    {
        if (this.behaviour_State == behaviour_State)
            return;

        switch (this.behaviour_State)
        {
            case Behaviour_State.START_STATE:
                startState.End(this);
                break;
            case Behaviour_State.IDLE_STATE:
                idleState.End(this);
                break;
            case Behaviour_State.MOVE_STATE:
                moveState.End(this);
                break;
            case Behaviour_State.ATTACK_STATE:
                attackState.End(this);
                break;
        }

        this.behaviour_State = behaviour_State;

        switch (this.behaviour_State)
        {
            case Behaviour_State.START_STATE:
                startState.Start(this);
                break;
            case Behaviour_State.IDLE_STATE:
                idleState.Start(this);
                break;
            case Behaviour_State.MOVE_STATE:
                moveState.Start(this);
                break;
            case Behaviour_State.ATTACK_STATE:
                attackState.Start(this);
                break;
        }
    }

    /// <summary>
    /// �̵��� �Ÿ��� ���Ѵ�.
    /// </summary>
    /// <returns>�̵������� ũ��.</returns>
    public float GetMoveMagnitude()
    {
        Vector3 nextPosition = transform.position + moveVector * Time.deltaTime * statistics.Speed;

        return (nextPosition - transform.position).magnitude; 
    }

    /// <summary>
    /// �浹ó��
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
    /// ���ݹ��� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="attackingEnemyStat">�� ����</param>
    public void ComeUnderAttack(CharacterStatistics attackingEnemyStat)
    {
        statistics.Hp = statistics.Hp - attackingEnemyStat.Damage;
    }
}
