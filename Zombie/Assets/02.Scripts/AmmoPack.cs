using UnityEngine;

// 탄알을 충전하는 아이템
public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; // 충전할 탄알 수

    public void Use(GameObject target)
    {
        // 전달받은 게임 오브젝트로부터 PlyaerShooter 컴포넌트 가져오기 시도
        
    }
}
