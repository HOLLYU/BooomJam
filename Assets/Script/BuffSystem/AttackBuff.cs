using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/AttackBuff")]
public class AttackBuff : Buff
{
    public float amount;
    [SerializeField] private float amount_real; //ʵ�ʵĵ�����ֵ

    public override void EnterBuff()
    {
        target.attack += amount;
    }

    public override void UseBuff()
    {
        count--;
    }

    public override void ExitBuff()
    {
        //target.attack -= amount;

/*        float tmp = 0;
        tmp = target.attack - amount;
        if (tmp < 0)
        {
            // ��������ٶ�
            amount_real = target.attack - 0; //�洢����ܹ��޸ĵ���ֵ
        }
        else
        {
            // û�г�������ٶȵĻ���amount_real����amount
            amount_real = amount;
        }

        target.attack -= amount_real;*/

        if(target.attack - amount <0)
        {
            target.attack = 1;
        }
        else
        {
            target.attack -= amount; 
        }
    }
}
