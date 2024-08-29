using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    #region Pipe
    [Header("土管速度")]
    [SerializeField] float pipeMoveSpeed = 1f;
    [Header("土管接触音")]
    [SerializeField] AudioClip SEPipeCollided;
    [Header("土管接触音量")]
    [SerializeField] float pipeCollidedSEVolume = 1f;
    #endregion

    #region Internal
    Rigidbody2D pipeRB;
    #endregion

    void Start()
    {
        pipeRB = GetComponent<Rigidbody2D>();
        PipeMove();
        
    }
    void Update()
    {
        
    }
    void PipeMove()
    {
        pipeRB.velocity = Vector2.left * pipeMoveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Variables.tagPlayer))
        {
            AudioSource.PlayClipAtPoint(SEPipeCollided, Camera.main.transform.position, pipeCollidedSEVolume);
            PlayerController.isCollided = true;
        }
    }
}
