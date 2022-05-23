using UnityEngine;

// ���� ���� �� �ִ� ��� ����� �������̽�
public interface IDamageable 
{
    /// <summary>
    /// �Ű� ����
    /// </summary>
    /// <param name="damage">��ü�� �޴� ������</param>
    /// <param name="hitPoint">�浹�� ��ġ</param>
    /// <param name="hitNormal">�浹�� ����</param>
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
