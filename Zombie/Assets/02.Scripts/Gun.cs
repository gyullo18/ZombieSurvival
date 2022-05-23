using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����
public class Gun : MonoBehaviour
{
    /// <summary>
    /// ���� ���¸� ǥ���ϴµ� ����� Ÿ���� ����
    /// </summary>
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }

    /// <summary>
    /// ���� ���� ���� - �б� ���� ������Ƽ
    /// </summary>
    public State state { get; private set; } 

    public Transform fireTransform; // ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; // ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; // ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer; // �� �Ҹ� �����

    public GunData gunData; // ���� ���� ������

    private float fireDistance = 50f; // �����Ÿ�

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� �����ִ� ź��

    private float lastFireTime; // ���� ���������� �߻��� ����

    /// <summary>
    /// GetComponent - ������Ʈ �ȿ��� ã�� : �� �ϳ��� ��.
    /// GetComponentInChildern - ������Ʈ ���� �ڽĵ鿡�� ã��.
    /// GetComponents - �ϳ��� ������Ʈ�� �ΰ� �̻��� ������Ʈ�� ���� ��(�ݶ��̴�) : ������ ��[�迭��].
    /// ex) LineRenderer[] bulletLineRenderers = GetComponents<LineRenderer>();
    /// GetComponentsInChildern - �ڽĵ��� ��ġ���� ���� ������ ��
    /// ex) Transform[] transform = GetComponents<Transform>();
    /// </summary>
    private void Awake()
    {
        // ����� ������Ʈ�� ���� ��������
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        // ����� ���� �ΰ��� ����
        bulletLineRenderer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }
    /// <summary>
    /// Start vs OnEnable
    /// OnEnable : ��ü�� Ȱ��ȭ �Ǿ��� �� ����
    /// OnDisable : ��ü�� ��Ȱ��ȭ �Ǿ��� �� ����
    /// ���� ������� ���� ��.
    /// </summary>
    private void OnEnable()
    {
        // �� ���� �ʱ�ȭ
        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        // ���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����(0�� ��)
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    // �߻�õ� - �߻� ó��x, �õ���
    public void Fire()
    {
        // ���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� gunData.timeBetFire �̻��� �ð��� ����
        if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            // ������ �� �߻� ���� ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
        }

    }

    // ���� �߻� ó��
    private void Shot()
    {
        // ���� ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        // ź���� ���� ���� ������ ����
        Vector3 hitPosition = Vector3.zero;

        // ���� ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
        if ( Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            // ���̰� � ��ü�� �浹�� ���
            // �浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            // GameObject a = hit.transform.gameObject;
            // a.GetComponent<IDamageable>;
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            
            // �������κ��� IDamageable ������Ʈ�� �������µ� �����ߴٸ�
            if ( target != null)
            {
                // ������ OnDamage �Լ��� ������� ���濡�� ����� �ֱ�
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            // ���̰� �浹�� ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            // ���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
            // ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        // �߻� ����Ʈ ��� ����
        StartCoroutine(ShotEffect(hitPosition));

        // ���� ź�� ���� -1
        magAmmo--;
        if( magAmmo <= 0)
        {
            //ź�˿� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }

    // �߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // �ѱ� ȭ�� ȿ�� ���
        muzzleFlashEffect.Play();
        // ź�� ���� ȿ�� ���
        shellEjectEffect.Play();

        // �Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        // ���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // ���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);
        // ���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderer.enabled = true;

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
    }

    // ������ �õ�
    public bool Reload()
    {
        if ( state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            // �̹� ������ ���̰ų� ���� ź���� ���ų�
            // źâ�� ź���� �̹� ������ ��� ������ �� �� ����.
            return false;
        }

        // ������ ó�� ����
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRoutine()
    {
        // ���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;
        // ������ �Ҹ� ���
        gunAudioPlayer.PlayOneShot(gunData.reloadClip);

        // ������ �ҿ� �ð���ŭ ó�� ����
        yield return new WaitForSeconds(gunData.reloadTime);

        // źâ�� ä�� ź�� ���
        int ammoToFill = gunData.magCapacity - magAmmo;

        // źâ�� ä���� �� ź���� ���� ź�˺��� ���ٸ�
        // ä���� �� ź�� ���� ���� ź�� ���� ���� ����
        if ( ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // źâ�� ä��
        magAmmo += ammoToFill;

        // ���� ź�˿��� źâ�� ä�ŭ ź���� ��
        ammoRemain -= ammoToFill;

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
