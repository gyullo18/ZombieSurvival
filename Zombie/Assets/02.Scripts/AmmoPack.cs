using UnityEngine;

// 탄알을 충전하는 아이템
public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; // 충전할 탄알 수

    public void Use(GameObject target)
    {
        // 전달받은 게임 오브젝트로부터 PlyaerShooter 컴포넌트 가져오기 시도
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter 컴포넌트가 있으며 총 오브젝트가 존재하면
        if (playerShooter != null && playerShooter.gun != null )
        {
            // 총의 남은 탄알 수를 ammo만큼 더함
            playerShooter.gun.ammoRemain += ammo;
        }

        // 사용되었으므로 자신을 파괴
        Destroy(gameObject);
    }
}
