using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    #region Internal
    PlayerController playerController;
    #endregion

    void Start()
    {
        playerController = GameObject.FindWithTag(Variables.tagPlayer).GetComponent<PlayerController>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Variables.tagPlayer))
        {
            collision.gameObject.SetActive(false);
            PlayerController.isCollided = false;
            playerController.PlayerInoperable();
            GameManager.isGameOver = true;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
