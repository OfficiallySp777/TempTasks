using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxMessages = 25;
    public GameObject chatPanel;
    public GameObject textObject;
    public InputField chatBox;
    public bool CritOverride;
    [SerializeField]
    List<Message> messageList = new List<Message>();
    // Start is called before the first frame update
    void Start()
    {
        chatBox.ActivateInputField();
    }
    // Update is called once per frame
    void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatBox.text, Message.MessageType.Normal);
                chatBox.text = "";
            }
            if (CritOverride == true)
            {
                SendMessageToChat(chatBox.text, Message.MessageType.Critical);
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Tab))
                chatBox.ActivateInputField();
        }
    }
    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }
}
[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;
    public enum MessageType
    {
        Normal,
        Critical
    }
}
