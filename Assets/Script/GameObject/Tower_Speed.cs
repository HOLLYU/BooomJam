using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower_Speed : Tower
{
    public Buff addBuff;

    protected override void Attack()
    {
        //todo: �����û�㣬������д��
        //anim.speed = attackSpeed;
        //anim.Play("attack");    // ���ڷ�������˵������������Ƿ��ض���


        // Ŀǰ��ֱ�ӿ�Ѫ�ˡ�ûд������
        target.GetComponent<Obj>().Bleed(attack);
        target.GetComponent<Obj>().AddBuff(addBuff);
        canAttack = false;
        Debug.Log("����");
    }
}