using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Boom : MonoBehaviour
{
    public float attack; //�ӵ�����
    public float boomRange; // ��ը��Χ

    public float speed = 10f;
    private Vector3 direction = Vector3.right;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    /// <summary>
    /// ���ӵ�һ����λ��
    /// </summary>
    /// <param name="newDirection"></param>
    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<Obj>().Bleed(attack);
            // ����ע���������У���Ի���Ŀ�껹�����һ���˺�

            Debug.Log("����");
            string name = "Prefabs/Bullets/" + gameObject.name.Substring(0, gameObject.name.Length - 7);    // ȥ��(Clone)
            PoolMgr.GetInstance().PushObj(name, gameObject);

            Vector3 boomLocation = other.transform.position; // ��ը��
            boomAttack(boomLocation); 
        }
        else
        {
            // 
        }
    }

    private void boomAttack(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, boomRange); //��÷�Χ����������

        foreach (Collider col in colliders)
        {
            GameObject _obj = col.gameObject; 
            
            if (_obj.CompareTag("Enemy"))
            {
                _obj.GetComponent<Obj>().Bleed(attack);
            }
            

        }
    }
}
