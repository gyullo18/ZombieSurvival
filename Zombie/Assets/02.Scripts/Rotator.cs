using UnityEngine;

// 게임 오브젝트를 실시간 회전시키는 스크립트
public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 60f;
    
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);        
    }
}
