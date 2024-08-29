using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region ScoreCollider
    [Header("コライダー速度")]
    [SerializeField] float scoreColliderMoveSpeed = 1f;
    #endregion

    #region SE
    [Header("GetSE")]
    [SerializeField] AudioClip SEGetScore;
    [Header("SEVol")]
    [SerializeField] float SEVolume = 0.1f;
    #endregion

    #region Internal
    Rigidbody2D scoreColliderRB;
    GameManager gameManager;
    #endregion

    void Start()
    {
        scoreColliderRB = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag(Variables.tagGameManager).GetComponent<GameManager>();
        ScoreColliderMove();
    }
    void Update()
    {
        
    }
    void ScoreColliderMove()
    {
        scoreColliderRB.velocity = new Vector2(-scoreColliderMoveSpeed, Variables.zero);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Variables.tagPlayer))
        {
            gameManager.AddScore();
            AudioSource.PlayClipAtPoint(SEGetScore, Camera.main.transform.position, SEVolume);
            Destroy(gameObject);
        }
    }
}
