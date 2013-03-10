using UnityEngine;
using System.Collections;

/// <summary>
/// �N�W�X��ɪ�����R��
/// </summary>
public class BorderDestroy : MonoBehaviour
{
    public float DestroyRadius = 15;     //��ɲy�Ϊ��b�|
    
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        //�T�{�O�_������i�J�d��
        if (Physics.CheckSphere(this.transform.position, this.DestroyRadius))
        { 
            //�R���i�J�d�򤺪�����
            foreach (var obj in Physics.OverlapSphere(this.transform.position, this.DestroyRadius))
                Destroy(obj.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�������
        Gizmos.DrawWireSphere(this.transform.position, this.DestroyRadius);
    }
}