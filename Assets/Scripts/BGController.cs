using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    #region Scroll
    [Header("スクロール速度")]
    [SerializeField] float scrollSpeed = 1f;
    [Header("背景横")]
    [SerializeField] scrollDirectionX directionX;
    [Header("背景縦")]
    [SerializeField] scrollDirectionY directionY;
    #endregion

    #region FollowTarget
    [Header("追従オブジェクト")]
    [SerializeField] GameObject followObject;
    [Header("指定オブジェクト追従")]
    [SerializeField] bool objectFollow = false;
    [Header("指定オブジェクト追従割合")]
    [SerializeField] float objectFollowRate = 1f;
    #endregion

    #region Internal
    const float scrollMax= 1f;
    Vector2 offset;
    Renderer materialRenderer;
    #endregion

    #region enum
    enum scrollDirectionX
    {
        NoMove=0,LeftToRight=-1,RightToLeft=1
    }
    enum scrollDirectionY
    {
        NoMove = 0, BottomToUpper = -1, UpperToBottom = 1
    }
    #endregion

    void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        if (followObject==null)
        {
            followObject = GameObject.FindWithTag(Variables.tagPlayer);
        }
        if (!objectFollow)
        {
            objectFollowRate = Variables.zero;
        }
    }
    void Update()
    {
        //ゲーム内時間で０からmax(1)になり、０に戻って繰り返す
        float scrollX = Mathf.Repeat(Time.time * scrollSpeed * (int)directionX, scrollMax);
        float scrollY = Mathf.Repeat(Time.time * scrollSpeed * (int)directionY, scrollMax);
        //プレイヤーの位置を取得
        float movePointX = followObject.transform.position.x;
        float movePointY = followObject.transform.position.y;
        //オフセットを作成
        offset = new Vector2(scrollX + movePointX * -objectFollowRate,
                             scrollY + movePointY * -objectFollowRate);
        //マテリアルにオフセットを設定
        materialRenderer.sharedMaterial.SetTextureOffset(Variables.mainTex, offset);
    }
}
