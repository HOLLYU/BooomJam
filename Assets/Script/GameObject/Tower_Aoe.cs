using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Tower_Aoe : Obj
{
    [Header("Boom")]
    public bool isBoom = false; //�Ƿ�Ϊը��
    public float BoomTimer; 

    [Header("Dark Type")]
    public bool isDark = false; // �Ƿ��ǰ�����
    public float dark_debuff_Hp; // ����ǰ����ԣ�һ��۶���Ѫ
    private float dark_debuff_Hp_Timer = 1.0f;

    private void Update()
    {
        darkTimer(); // �����Կ�Ѫ

        if (isBoom)
        {
            if(BoomTimer> 0)
            {
                BoomTimer -= Time.fixedDeltaTime;
            }
            else
            {
                Attack(); 
                Death();
                // ը�����꼴����
            }
        }
        else
        {
            checkTarget(); // ���Ŀ�� !!! Ŀǰֻ��ʹ�ü���+����������
            UpdateAttackSpeed();

            // ��һ����Ŀ��,Attack
            if (haveTarget && canAttack)
            {
                Attack();
            }
            else
            {
                // ����Idle
                Idle();

            }
        }

        

    }

    private void darkTimer()
    {
        if (dark_debuff_Hp_Timer <= 0f)
        {
            Bleed(dark_debuff_Hp);
            dark_debuff_Hp_Timer = 1.0f;
        }
        else
        {
            dark_debuff_Hp_Timer -= Time.fixedDeltaTime;
        }
    }

    /// 
    /// ���뵽Mgr��
    ///
    public void AddToBattleMgr()
    {
        BattleMgr.GetInstance().towers.Add(gameObject);
    }
    /// <summary>
    /// ��Mgr��ɾ��
    /// </summary>
    public void DelFromBattleMgr()
    {
        BattleMgr.GetInstance().towers.Remove(gameObject);
    }

    /// <summary>
    /// ��⹥����Χ���Ƿ���Ŀ��
    /// </summary>
    public void checkTarget()
    {
        // ! �ⲿ�����ļ��⳯�Ҵ򣬹ֵļ��⳯�����������������ٸ�
        //==================================================
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            if (hit.collider.CompareTag("Enemy"))
            {
                haveTarget = true;
                target = hit.collider.gameObject;
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * attackRange, Color.blue);
            haveTarget = false;
            target = null;
        }

    }

    /// <summary>
    /// ����
    /// </summary>
    protected override void Attack()
    {
        //todo: �����û�㣬������д��
        //anim.speed = attackSpeed;
        //anim.Play("attack");    // ���ڷ�������˵������������Ƿ��ض���


        // Ŀǰ��ֱ�ӿ�Ѫ�ˡ�ûд������

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange); //��÷�Χ����������

        foreach (Collider col in colliders)
        {
            GameObject _obj = col.gameObject;

            if (_obj.CompareTag("Enemy"))
            {
                _obj.GetComponent<Obj>().Bleed(attack);
            }
        }

        canAttack = false;

    }

    /// <summary>
    /// ��Ϣ״̬
    /// </summary>
    public void Idle()
    {
        // todo: ���Ҳ�ǣ�����֮���һ������������һЩ�Ƕ����¼�
        //anim.speed = 1;
        //anim.Play("idle");
    }
}