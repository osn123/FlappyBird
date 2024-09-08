using  System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Jump
    [Header("ジャンプ力")]
    [SerializeField] float jumpPower = 1f;
    #endregion

    #region Inoperable
    [Header("落下速度")]
    [SerializeField] float fallSpeed = 1f;
    [Header("回転速度")]
    [SerializeField] float rotateSpeed = 1f;
    #endregion

    #region Respawn
    [Header("Relive")]
    [SerializeField] GameObject PlayerRespawnPos;
    #endregion

    #region Image
    [Header("通常画像")]
    [SerializeField] Sprite imageNormal;
    [Header("負傷画像")]
    [SerializeField] Sprite imageDamaged;
    #endregion

    #region CV
    [Header("負傷オーディオ")]
    [SerializeField] AudioSource audioSourceHurt;
    [Header("掛け声オーディオ")]
    [SerializeField] AudioSource audioSourceYell;
    [Header("負傷")]
    [SerializeField] AudioClip[] CVHurt;
    [Header("掛け声")]
    [SerializeField] AudioClip[] CVYell;
    [Header("空腹時オーディオ")]
    [SerializeField] AudioSource audioSourceHunger;
    [Header("空腹")]
    [SerializeField] AudioClip[] CVHunger;
    #endregion

    #region SatietyGauge

    [Header("満腹度 ゲージ")]
    [SerializeField] Slider satietyGauge;
    [Header("満腹度 数値")]
    [SerializeField] TMP_Text satietyGaugeText;

    [Header("満腹度 最大値")]
    [SerializeField] float gaugeMax = 100f;

    [Header("満腹度 最小値")]
    [SerializeField] float gaugeMin = 0f;

    [Header("満腹度 減少値")]
    [SerializeField] float gaugeDecreaseValue = 1f;

    [Header("満腹度 減少閾値")]
    [SerializeField] int gaugeDecreaseCount = 20;

    [Header("満腹度 ジャンプ減少値")]
    [SerializeField] float gaugeDecreaseValueJump = 0.3f;

    [Header("満腹度 回復量")]
    [SerializeField] float gaugeHealValue = 10f;

    #endregion

    #region Internal
    Rigidbody2D playerRB;
    Collider2D playerCollider;
    SpriteRenderer playerImage;
    float gaugeCurrentValue;
    int gaugeCount = 0;
    bool isSayCollided = false;
    public static bool isCollided = false;
    bool isSayHunger = false;
    #endregion

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerImage = GetComponent<SpriteRenderer>();
        SatietyGaugeInit();
    }
    void Update()
    {
        if (!isCollided)
        {
            if (Input.GetKey(KeyCode.F)|| Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
        else
        {
            PlayerInoperable();
        }

        gaugeCount++;
        if (gaugeCount>gaugeDecreaseCount)
        {
            SatietyGaugeDecrease(gaugeDecreaseValue);
            SatietyGaugeUpdate();
            gaugeCount = (int)Variables.zero;
        }
        if (gaugeCurrentValue<=gaugeMin)
        {
            PlayerInoperable();  
        }
    }
    void Jump()
    {
        SayCV(audioSourceYell, CVYell);
        playerRB.velocity = Vector2.up * jumpPower;
        SatietyGaugeDecrease(gaugeDecreaseValueJump);
        SatietyGaugeUpdate();

    }
    public void PlayerOperable()
    {
        isCollided = false;
        isSayCollided = false;
        isSayHunger = false;

        gameObject.transform.position = PlayerRespawnPos.transform.position;

        playerRB.velocity = Vector2.zero;
        gameObject.transform.Rotate(Vector3.zero);
        //gameObject.transform.Rotate(Variables.zero, Variables.zero, Variables.zero);
        playerRB.freezeRotation = true;
        playerCollider.isTrigger = false;
        playerImage.sprite = imageNormal;

        SatietyGaugeInit();

        gameObject.SetActive(true);
    }
    /// <summary>    /// test    /// </summary>
    public void PlayerInoperable()
    {
        if (!isSayCollided && isCollided)
        {
            SayCV(audioSourceHurt, CVHurt);
            isSayCollided = true;
        }
        else if (!isSayHunger && !isCollided)
        {
            SayCV(audioSourceHunger, CVHunger);
            isSayHunger = true;
        }
        playerRB.velocity = Vector2.down*fallSpeed;
        playerImage.sprite = imageDamaged;
        playerCollider.isTrigger = true;
        playerRB.freezeRotation = false;
        //gameObject.transform.Rotate(Vector3.zero);
        gameObject.transform.Rotate(Variables.zero, Variables.zero, rotateSpeed);
    }
    void SayCV(AudioSource audioSource,AudioClip[] clip)
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        else
        {
            audioSource.PlayOneShot(clip[Random.Range((int)Variables.zero, clip.Length)]);
        }
    }
    void SatietyGaugeInit()
    {
        satietyGauge.maxValue = gaugeMax;
        satietyGauge.minValue = gaugeMin;
        satietyGauge.value = gaugeMax;
        satietyGaugeText.SetText(gaugeMax.ToString());
        gaugeCurrentValue = satietyGauge.value;
    }
    void SatietyGaugeDecrease(float decreaseValue)
    {
        gaugeCurrentValue -= decreaseValue;
    }
    void SatietyGaugeUpdate()
    {
        if (gaugeCurrentValue>gaugeMax)
        {
            gaugeCurrentValue = gaugeMax;
        }
        else if (gaugeCurrentValue < gaugeMin)
        {
            gaugeCurrentValue = gaugeMin;
        }
        satietyGaugeText.SetText(gaugeCurrentValue.ToString("f1"));
        satietyGauge.value = gaugeCurrentValue;
    }
    public void SatietyGaugeHeal()
    {
        gaugeCurrentValue += gaugeHealValue;
        SatietyGaugeUpdate();
    }
}
