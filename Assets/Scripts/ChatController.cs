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
    float minPso;

    //public List<string> chatHistory = new List<string>(); -> 나중에 서버로 주고 받고 할 때 사용

    private Coroutine currentCoroutine; // 현재 실행 중인 코루틴을 저장

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

                // 기존 코루틴이 실행 중이라면 멈춤
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                    currentCoroutine = null;
                }

                CreateEmptyObject();
            }
            else
            {
                CopyText();
                isChatActive = false;

                // 새로운 코루틴 실행
                currentCoroutine = StartCoroutine(HideChatPanelAfterDelay(2f));
            }
        }
    }

    void CreateEmptyObject()
    {
        RectTransform parentRect = parentObject.GetComponent<RectTransform>();
        // parentObject의 높이를 40씩 증가
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 40f);

        minPso = 0f - (chatCount * 40f); // 채팅 수에 따라 y 위치 조정

        newObj = new GameObject("ChatSubObject", typeof(RectTransform));
        newObj.transform.SetParent(parentObject.transform, false);

        RectTransform rect = newObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-5f, minPso);
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
