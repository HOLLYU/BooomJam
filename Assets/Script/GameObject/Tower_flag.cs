using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_flag : Tower
{
    [Header("Buff���")]
    public Buff addBuff;
    public List<Tower> towers = new List<Tower>();

    protected override void Attack()
    {
        Debug.Log("ͼ��");
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange); //��÷�Χ����������

        foreach (Collider col in colliders)
        {
            GameObject _obj = col.gameObject;
            if(_obj.name != this.gameObject.name)
            {
                Tower t = col.gameObject.GetComponent<Tower>();
                towers.Add(t);
                t.AddBuff(addBuff);
            }
        }

        canAttack = false;
    }

    protected override void Death()
    {
        DelFromBattleMgr(); // �Ӷ�Ӧ���б���ɾ���ö���
        gameObject.SetActive(false);

        if (isFlag)
        {
            foreach(Tower t in towers)
            {
                t.RemoveBuff(addBuff);
            }
        }
    }
}
