using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{

    public Obj target;
    public int count; // buff�ĳ���ʱ�䣬дx��

    public void SetBuffTarget(Obj target )
    {
        this.target = target;
    }

    public void SetBuffCount(int count)
    {
        this.count = count;
    }

    public abstract void EnterBuff();

    public abstract void UseBuff();

    public abstract void ExitBuff();

    
    
}
