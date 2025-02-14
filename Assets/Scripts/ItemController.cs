using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    #region Item
    [Header("速度")]
    [SerializeField] float itemMoveSpeed = 1f;
    [Header("効果音")]
    [SerializeField] AudioClip getItemSE;
    [Header("音量")]
    [SerializeField] float getItemSEVolume = 0.1f;
    #endregion

    #region Internal
    Rigidbody2D itemRB;
    PlayerController playerController;
    #endregion
    void Start()
    {
        itemRB = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindWithTag(Variables.tagPlayer)
            .GetComponent<PlayerController>();
        ItemMove();
    }
    void Update()
    {
        
    }
    void ItemMove()
    {
        itemRB.velocity = Vector2.left * itemMoveSpeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Variables.tagPlayer))
        {
            AudioSource.PlayClipAtPoint(getItemSE, Camera.main.transform.position, getItemSEVolume);
            playerController.SatietyGaugeHeal();
            Destroy(gameObject);
        } 
    }

}
