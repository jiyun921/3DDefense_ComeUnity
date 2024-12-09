using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // �ܺο��� StageManager�� �����ϱ� ���� �ν��Ͻ�
    private int[] goalEnemyCount; // �������� �� ��ǥ �� óġ���� �����ϴ� �迭 
    private int numOfStages = 4; // ��ü �������� �� 
    private int currentStage = 1; // ���� ��������  
    private float delayBeforeNextStage = 3f; // �������� Ŭ���� �� ���� �������� ���� �������� ������
    private int enemyCountToFail = 5; // ���� ������ �Ǵ� �ּ� ���� ��
    public int CurrentEnemyCount { get; set; } = 0; // ���� ��ȯ�Ǿ� �ִ� ���� ��
    public int CurrentKilledEnemyCount { get; set; } = 0; // ���� �������������� �� óġ��
    public bool IsCleared { get; private set; } = false;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    void Start()
    {
        // goalEnemyCount �迭 ��ü �������� ����ŭ ���� �Ҵ�
        // �������� �� ��ǥ �� óġ ���� ����(���ϴ� ������ Ŀ���͸�����)
        // ����) 1���������� 20����, 2���������� 50����, 3���������� 100���� ...
        goalEnemyCount = new int[numOfStages + 1];
        for (int i = 1; i <= numOfStages; i++)
        {
            goalEnemyCount[i] = i * 1;
        }
        InitCount();
    }

    void Update()
    {
    }

    void InitCount()
    {
        CurrentEnemyCount = 0;
        CurrentKilledEnemyCount = 0;
    }

    void FailStage()
    {
        GameManager.Instance.EndGame(false);
    }

    public void CheckClearCondition()
    {
        if (goalEnemyCount[currentStage] <= CurrentKilledEnemyCount)
        {
            StartCoroutine(ClearStage());
        }
    }

    public void CheckFailCondition()
    {
        if (enemyCountToFail <= CurrentEnemyCount)
        {
            FailStage();
        }
    }

    IEnumerator ClearStage()
    {
        IsCleared = true;
        if (currentStage == numOfStages)
        {
            GameManager.Instance.EndGame(true);
            yield break;
        }
        else
        {
            currentStage++;
            InitCount();
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(delayBeforeNextStage);
            IsCleared = false;
            Time.timeScale = 1f;
        }
        yield break;
    }
}
