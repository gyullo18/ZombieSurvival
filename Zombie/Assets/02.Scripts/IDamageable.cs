using UnityEngine;

// 공격 당할 수 있는 모든 대상의 인터페이스
public interface IDamageable 
{
    /// <summary>
    /// 매개 변수
    /// </summary>
    /// <param name="damage">물체가 받는 데미지</param>
    /// <param name="hitPoint">충돌한 위치</param>
    /// <param name="hitNormal">충돌한 방향</param>
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
