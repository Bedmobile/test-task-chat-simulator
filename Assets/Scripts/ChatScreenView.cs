using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatScreenView : MonoBehaviour
{
    public event Action<string> OnSendButtonClicked;
    public event Action<Message> OnDeleteButtonClicked;

    [Header("Messages")]
    [SerializeField] private UserMessageView _currentUserMessagePrefab;
    [SerializeField] private UserMessageView _otherUserMessagePrefab;
    [SerializeField] private VerticalLayoutGroup _messagesLayout;
    [SerializeField] private ScrollRect _scrollView;

    [Header("Buttons")]
    [SerializeField] private Button _sendButton;
    [SerializeField] private Button _deleteModeButton;
    [SerializeField] private Button _confirmButton;

    [Header("InputField")]
    [SerializeField] private TMP_InputField _inputField;

    [Header("Helpers")]
    [SerializeField] private GameObject DefaultState;
    [SerializeField] private GameObject DeleteState;

    private List<UserMessageView> _messagesViews = new List<UserMessageView>();

    private void OnEnable()
    {
        _deleteModeButton.onClick.AddListener(() => SwitchDeleteMode(true));
        _confirmButton.onClick.AddListener(() => SwitchDeleteMode(false));
        _sendButton.onClick.AddListener(() => SendMessage());
    }

    private void OnDisable()
    {
        _deleteModeButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.RemoveAllListeners();
        _sendButton.onClick.RemoveAllListeners();
    }

    public void CreateMessage(Message message, bool isCurrentUser)
    {
        var prefab = isCurrentUser ? _currentUserMessagePrefab : _otherUserMessagePrefab;
        UserMessageView messageView = Instantiate(prefab, _messagesLayout.transform);
        messageView.Set(message);
        messageView.OnDeleteButtonClicked += DeleteMessage;
        _messagesViews.Add(messageView);
        ShowMessage(messageView);
    }

    public void UpdateMessageView(Message message, bool isTheUserLastMessage)
    {
        for(int i = 0; i <_messagesViews.Count; i++) {
            if(_messagesViews[i].Message.Equals(message)) {
                _messagesViews[i].UpdateView(isTheUserLastMessage);
                break;
            }
        }
    }

    private void ShowMessage(UserMessageView message)
    {
        message.MessageTransform.localScale = 0.3f * Vector3.one;
        message.gameObject.SetActive(true);
        message.MessageTransform.DOScale(1, 0.2f).SetEase(Ease.OutExpo);
    }

    private void HideMessage(UserMessageView message, Action onCompleteAction)
    {
        message.MessageTransform.DOScale(0, 0.2f).SetEase(Ease.OutExpo).OnComplete(() => onCompleteAction());
    }

    private void DeleteMessage(Message message, UserMessageView messageView)
    {
        if(OnDeleteButtonClicked != null) {
            OnDeleteButtonClicked(message);
        }
        _messagesViews.Remove(messageView);
        HideMessage(messageView, () => Destroy(messageView.gameObject));
    }

    private void SwitchDeleteMode(bool activate)
    {
        DefaultState.SetActive(!activate);
        DeleteState.SetActive(activate);
        if (activate) {
            _messagesLayout.transform.DOLocalMoveX(-108, 0.2f).SetEase(Ease.OutExpo);
        } else {
            _messagesLayout.transform.DOLocalMoveX(0, 0.2f).SetEase(Ease.OutExpo);
        }
        for(int i = 0; i < _messagesViews.Count; i++) {
            _messagesViews[i].ShowDeleteButton(activate);
        }
    }

    private void SendMessage()
    {
        if(_inputField.text != string.Empty) {
            if(OnSendButtonClicked != null) {
                OnSendButtonClicked(_inputField.text);
            }
            _scrollView.verticalNormalizedPosition = 0;
            _inputField.text = string.Empty;
        }
    }
}
