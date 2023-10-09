using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy_Boss : Enemy
{

    [Header("Boss��ص���ֵ���")]
    public float ToNextLevelHp; // ʲôʱ�������׶�(Ѫ��)
    [SerializeField] private bool UpLevel; //������׶�
    public float newSpeed; //���׶��޸�������xx
    public float healEnemy_Time; //ʹ�û�Ѫ��ʱ��ֹͣ�ƶ���ʱ��
    public float healEnemy_Range; //��Ѫ��Χ   ��������д��20����ȫ����
    public float healEnemy_amount; //�ض���Ѫ
    public float healEnemy_CD; //ʹ��CD ;  
    private float healEnemy_CD_Timer; //��ʱ�� ;  


    public override void checkTarget()
    {
        // ! �ⲿ�����ļ��⳯�Ҵ򣬹ֵļ��⳯�����������������ٸ�
        //==================================================
        int layerMask = 1 << LayerMask.NameToLayer("Tower"); 

        // ! ������ֱ��д�����������߼���ˣ�������bug

        Ray ray = new Ray(transform.position, -transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            if (hit.collider.CompareTag("Tower"))
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

        // ==================
        ray = new Ray(transform.position + new Vector3(0,1,0), -transform.right);
        if (Physics.Raycast(ray, out hit, attackRange, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            if (hit.collider.CompareTag("Tower"))
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
        //=======================

        //=======================
        ray = new Ray(transform.position + new Vector3(0, -1, 0), -transform.right);
        if (Physics.Raycast(ray, out hit, attackRange, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            if (hit.collider.CompareTag("Tower"))
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
        //==============================
        canAttack= false;
    }

    public override void Bleed(float harm)
    {
        hp -= harm;

        if(!UpLevel)
        {
            if(hp <= ToNextLevelHp)
            {
                // �����ٶȲ���ǽ�����׶�
                speed = newSpeed;
                UpLevel= true;
                healEnemy_CD_Timer = healEnemy_CD; 
            }
        }

        if (hp <= 0)
        {
            state = ObjState.Death;
        }
    }


    public override void SkillCheck()
    {
        if (UpLevel)
        {
            if (healEnemy_CD_Timer <= 0)
            {
                //�Ż�Ѫ����
                HealEnemy();
                healEnemy_CD_Timer = healEnemy_CD;
            }
            else
            {
                healEnemy_CD_Timer -= Time.fixedDeltaTime;

            }

            if (healEnemy_CD_Timer <= healEnemy_CD - healEnemy_Time)
            {
                speed = newSpeed; // �ָ��ƶ�
            }
        }

    }

    private void HealEnemy()
    {
        speed = 0; //ֹͣ�ƶ�
        Collider[] colliders = Physics.OverlapSphere(transform.position , healEnemy_Range); //��÷�Χ����������

        foreach (Collider col in colliders)
        {
            GameObject _obj = col.gameObject;

            if (_obj.CompareTag("Enemy"))
            {
                _obj.GetComponent<Obj>().hp += healEnemy_amount; 
                // ��Ѫ
            }


        }
    }
}
