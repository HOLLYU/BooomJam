using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float attack; //�ӵ�����

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Obj>().Bleed(attack);
            Debug.Log("����");
            Destroy(gameObject);

        }
        else
        {
            // 
        }
    }
}