using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : HPComponent
{
    // ������ Enemy�� TracePlayer���� ���� �ڵ��ε�, ���� ����� �и� �����Ұ���
    // Enemy���� �ٸ� ������� ���� ����� Transform�� �ǽð����� ������ �� ������
    // �̱������� �� �ʿ䵵 ������
    public static Basement Instance { get; private set; }
    public Transform BasementTransform { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        BasementTransform = transform;
    }

    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        UIManager.Instance.SetBasementHPSlider(curHP);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
