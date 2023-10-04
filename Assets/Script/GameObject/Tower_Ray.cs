using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ҷ���λ
/// </summary>
public class Tower_Ray : Obj
{
    [Header("Attack Type")]
    public GameObject Bullet; // ���ɵ��ӵ�
    public Transform firePoint; // �����ӵ���λ��

    private void Update()
    {
        UpdateAttackSpeed();

        // ��һ����Ŀ��,Attack
        if (canAttack)
        {
            checkAndAttack();

        }
        else
        {
            // ����Idle
            Idle();

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
    /// ��⹥����Χ�����й���
    /// </summary>
    public void checkAndAttack()
    {
        // ! �ⲿ�����ļ��⳯�Ҵ򣬹ֵļ��⳯�����������������ٸ�
        //==================================================
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit[] hits = Physics.RaycastAll(ray,attackRange); // ��÷�Χ�����еĵ���
        if(hits.Length> 0 )
        {
            canAttack = false;
            foreach (RaycastHit hit in hits)
            {
                GameObject _target = hit.collider.gameObject;
                if (_target.CompareTag("Enemy"))
                {
                    _target.GetComponent<Enemy>().Bleed(attack);
                }
            }
        }
        else
        {
            canAttack = true;
        }




        Debug.DrawLine(ray.origin, ray.origin + ray.direction * attackRange, Color.blue);


    }

    /// <summary>
    /// ����
    /// </summary>
    protected override void Attack()
    {

        // ������ü��⹥���ˣ�֮�����Ķ����üӵ�����ȥ
        // ���߸�����Ĳ���Ҳ��

        //todo: �����û�㣬������д��
        //anim.speed = attackSpeed;
        //anim.Play("attack");    // ���ڷ�������˵������������Ƿ��ض���


        // Ŀǰ��ֱ�ӿ�Ѫ�ˡ�ûд������

/*        target.GetComponent<Obj>().Bleed(attack);
        canAttack = false;
        Debug.Log("����");*/
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

    /// <summary>
    /// �����¼��������ӵ�
    /// </summary>
    public void Shoot()
    {
        // todo: ���ӵ�
        GameObject _bullet = Instantiate(Bullet, firePoint);
        _bullet.transform.parent = null;
        canAttack = false;
        // _bullet.GetComponent<Bullet>().

    }
}