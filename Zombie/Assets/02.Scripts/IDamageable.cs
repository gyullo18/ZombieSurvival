using UnityEngine;

// ���� ���� �� �ִ� ��� ����� �������̽�
public interface IDamageable 
{
    // ��ü�� �޴� ������(damage), �浹�� ��ġ(hitPoint), �浹�� ����(hitNormal)
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
