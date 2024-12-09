using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent : MonoBehaviour // Player, Basement�� �θ� Ŭ����
{
    [SerializeField]
    public int MaxHP { get; private set; } = 100; // �ִ� ü��
    protected int curHP; // ���� ü��, protected�� �ϴ� ������, ���� �ڽ� Ŭ�������� curHP�� ������� UI�� �����ϱ� ����

    protected virtual void Awake()
    {
        curHP = MaxHP;
    }
    protected virtual void ApplyDamage(int damage) // �������� �����ϴ� �Լ�
    {
        curHP -= damage;
        curHP = Mathf.Clamp(curHP, 0, MaxHP); 
        // ���� curHP�� ���� 0 ~ MaxHP�� ������ ����
        // curHP�� ������ �������� ��, curHP�� 0���� �ٲ��ִ� ����
    }

    public bool IsHPZero() // curHP�� 0���� Ȯ���ϴ� �Լ�
    {
        return curHP == 0;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack")) // �浹 ������ ����� "EnemyAttack"�̶�� �±׸� ���� ���� ������Ʈ�� ��
        {
            Bullet EnemyAttack = other.GetComponent<Bullet>(); 
            if (EnemyAttack != null)
            {
                ApplyDamage(EnemyAttack.Damage); // ���� ���� ������Ʈ(Bullet)
            }
            if (IsHPZero()) // �÷��̾� �Ǵ�, ������ HP�� 0�̸�
            {
                // �÷��̾� ��� or ���� �ı��� ���� ���� ����
                GameManager.Instance.EndGame(false);
            }
        }
    }
}
