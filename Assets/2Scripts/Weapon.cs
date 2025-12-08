using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range }
    public Type type;

    public int damage;
    public int maxAmmo;
    public int curAmmo;
    public float rate;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;

    // ===============================
    //         SOUND SYSTEM
    // ===============================
    [Header("Weapon Sound")]
    public AudioClip fireSound;          // 발사/휘두르기 사운드
    private AudioSource audioSource;     // 무기 전용 AudioSource

    void Awake()
    {
        // 무기에 AudioSource 자동 부착 (Inspector에 없어도 됨)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.8f;
    }

    public void Use()
    {
        // 🔥 사운드 먼저 재생
        if (fireSound != null)
            audioSource.PlayOneShot(fireSound);

        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else
        {
            if (curAmmo > 0)
            {
                curAmmo--;
                StartCoroutine("Shot");
            }
        }
    }

    IEnumerator Swing()
    {
        // 1. Trail On
        trailEffect.enabled = true;
        meleeArea.enabled = true;

        // 공격 판정 유지 시간
        yield return new WaitForSeconds(0.2f);

        // 2. Trail Off
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // 총알 생성
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);

        Rigidbody rigid = instantBullet.GetComponent<Rigidbody>();
        rigid.linearVelocity = bulletPos.forward * 50;

        yield return null;
    }
}
