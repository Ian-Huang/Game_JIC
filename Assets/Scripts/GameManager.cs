using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public int CurrentMorale;
    public int MoraleRestoreRate;
    public int MaxMorale;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        this.MaxMorale = GameDefinition.MaxMorale;
        this.CurrentMorale = GameDefinition.MaxMorale;
        this.MoraleRestoreRate = GameDefinition.MoraleRestoreRate;

        InvokeRepeating("RestoreMoralePersecond", 0.1f, 1);
    }

    /// <summary>
    /// ���o���e�h��ȡA�w�ഫ��0~100
    /// </summary>
    /// <returns></returns>
    public float GetCurrentMorale()
    {
        return ((float)this.CurrentMorale / this.MaxMorale) * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ѥ����ʱ��ƭ�
        //�h��
        MUI_Monitor.script.SetValue("�h���"+"x", ((float)this.CurrentMorale / this.MaxMorale) * 100);
    }

    /// <summary>
    /// �C���T�w�^�_�h���
    /// </summary>
    void RestoreMoralePersecond()
    {
        this.CurrentMorale += this.MoraleRestoreRate;
        if (this.CurrentMorale >= this.MaxMorale)
            this.CurrentMorale = this.MaxMorale;
    }
}