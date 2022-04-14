using UnityEngine;

// 공격 당할 수 있는 모든 대상의 인터페이스
public interface IDamageable 
{
    // 물체가 받는 데미지(damage), 충돌한 위치(hitPoint), 충돌한 방향(hitNormal)
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
