using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    // Creates a static score popup
    public static ScorePopup Create(Vector3 position, int scoreAmount) 
    {
        // Instantiates score popup prefab directly from the prefab folder
        GameObject scorePopupObject = Instantiate(GameAssets.i.scorePopupPrefab, position, Quaternion.identity) as GameObject;

        ScorePopup scorePopup = scorePopupObject.GetComponent<ScorePopup>();
        scorePopup.Setup(scoreAmount);

        return scorePopup;
    }

    private static int sortingOrder;
    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private TextMeshPro scoreText;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    void Awake()
    {
        scoreText = GetComponent<TextMeshPro>();
    }

    public void Setup(int scoreAmount)
    {
        scoreText.SetText("+" + scoreAmount.ToString());
        scoreText.fontSize = 7;
        textColor = scoreText.color;
        disappearTimer = 0.5f;

        sortingOrder++;
        scoreText.sortingOrder = sortingOrder;

        moveVector = new Vector3(0.1f, 0.2f) * 40f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5) {
            // First half of popup
            float increaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            // Second half of popup
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            scoreText.color = textColor;
            if (textColor.a < 0) { 
                Destroy(gameObject); 
            }
        }
    }

}
