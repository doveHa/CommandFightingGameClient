using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class ChatController : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_InputField chatInput;
    public Transform player;
    public string playerName = "Player1";
    private bool isChatActive = false;
    public GameObject parentObject;
    public TMP_FontAsset customFont;
    public GameObject inputSpace;

    private GameObject newObj;
    private TMP_Text tmpText;
    private int chatCount = 0; // 생성된 ChatSubObject 개수 추적

    public List<string> chatHistory = new List<string>();

    void Start()
    {
        chatPanel.SetActive(false);
        inputSpace.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatActive)
            {
                // 채팅 시작
                chatPanel.SetActive(true);
                inputSpace.SetActive(true);
                chatInput.interactable = true;
                chatInput.text = "";
                chatInput.ActivateInputField();
                isChatActive = true;

                CreateEmptyObject();
            }
            else
            {
                CopyText();
                isChatActive = false;
                StartCoroutine(HideChatPanelAfterDelay(2f));
            }
        }
    }

    void CreateEmptyObject()
    {
        float yPos = 290f - (chatCount * 40f); // 채팅 수에 따라 y 위치 조정

        newObj = new GameObject("ChatSubObject", typeof(RectTransform));
        newObj.transform.SetParent(parentObject.transform, false);

        RectTransform rect = newObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-5f, yPos);
        rect.sizeDelta = new Vector2(548f, 40f);

        CreateText();

        chatCount++; // 카운트 증가
    }

    void CreateText()
    {
        GameObject textObj = new GameObject("ChatText", typeof(RectTransform));
        textObj.transform.SetParent(newObj.transform, false);

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0f, 0f);
        textRect.sizeDelta = new Vector2(500f, 30f);

        tmpText = textObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = "";
        tmpText.fontSize = 25;
        tmpText.color = Color.green;
        tmpText.alignment = TextAlignmentOptions.Left;

        if (customFont != null)
            tmpText.font = customFont;
    }

    void CopyText()
    {
        string input = "[" + playerName + "] : " + chatInput.text;
        tmpText.text = input;

        chatInput.text = "";
        chatInput.interactable = false;
    }

    IEnumerator HideChatPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        chatPanel.SetActive(false);
        inputSpace.SetActive(false);
    }
}
