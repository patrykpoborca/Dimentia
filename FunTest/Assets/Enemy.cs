using UnityEngine;
using System.Collections;

/// <summary>
/// 敌人AI脚本,带自动寻路,追踪,攻击反馈功能
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 敌人状态枚举,用来存放可以用状态
    /// </summary>
    enum EnemyState
    {
        STAND,
        WALK,
        RUN
    }
	public GameObject 	m_gameMan;
    /// <summary>
    /// 敌人状态机,放置当前敌人状态
    /// </summary>
    private EnemyState m_enemyState;

    /// <summary>
    /// 存放追踪对象
    /// </summary>
    public GameObject m_player;

    /// <summary>
    /// 敌人发现目标在什么范围内才追逐
    /// </summary>
    public int m_atkDis = 15;

    /// <summary>
    /// 允许攻击距离
    /// </summary>
    public int m_enableAtk = 15;

    /// <summary>
    /// 储存怪物开始地点
    /// </summary>
    public Vector3 _opos;

    /// <summary>
    /// 回去速度
    /// </summary>
    public float m_walkspeed = .1f;

    /// <summary>
    /// 跑步速度
    /// </summary>
    public float m_runspeed = 0.5f;

    void Start()
	{  
        // 初始化敌人状态
        m_enemyState = EnemyState.STAND;

        // 如果没有拖入目标 那么我这里就自动查找目标
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }

        //if (_opos == null)
        //{
        _opos = gameObject.transform.position;
        //}
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, m_player.transform.position) < m_atkDis &&
            m_enemyState != EnemyState.WALK)
        {
            // 玩家攻击单位
            if (Vector3.Distance(transform.position, m_player.transform.position) < m_enableAtk &&
               Input.GetMouseButton(0))
            {
                m_enemyState = EnemyState.WALK;
                Debug.Log("Test_Attack");
				gameObject.animation.Play("DronePatrol1");
            }
            else
            {
                // 敌人AI发现了玩家,开始追逐玩家
                m_enemyState = EnemyState.RUN;

                // 播放动作
//                gameObject.animation.Play("Take 001");
	//			gameObject.animation.Stop("DronePatrol1");
                //设置敌人的方向，面朝主角
                transform.LookAt(m_player.transform);
            }

        }
        // 玩家脱离追逐范围就切换为等待状态
        else
        {
            m_enemyState = EnemyState.WALK;
       //     gameObject.animation.Play("walk");
			transform.LookAt(transform.position);
        }

        // 根据状态机来完成操作
        switch (m_enemyState)
        {
            case EnemyState.STAND:
                {

                    break;
                }
            case EnemyState.WALK:
                {
                    // 调整敌人朝向,让他看向自己的初始点
                    transform.LookAt(_opos);
                    // 向初始点移动
                    transform.position = Vector3.MoveTowards(transform.position, _opos, m_walkspeed * Time.deltaTime);

                    // 判断是否回到了初始点
                    if (transform.position == _opos)
                    {
                        m_enemyState = EnemyState.STAND;
               //       gameObject.animation.Play("DronePatrol1");
                    }

                    break;
                }
            case EnemyState.RUN:
                {
                    // 敌人朝主角奔跑
                    if (Vector3.Distance(transform.position, m_player.transform.position) > 1)
                    {
                        // 朝主角移动
                        transform.Translate(Vector3.forward * Time.deltaTime * m_runspeed);
                    }
                    // 被抓到后的处理
                    else
                    {
                        // 重新加载菜单关卡
                       // Application.LoadLevel("menuScene");
				m_gameMan = GameObject.Find ("gameFM");
			            	m_gameMan.SendMessage("ResetMoney",SendMessageOptions.DontRequireReceiver);
				Application.LoadLevel("bridgetown");
						
                    }
                    break;
                }
        }
    }
}