using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UserMessageView : MonoBehaviour
{
    public event Action<Message, UserMessageView> OnDeleteButtonClicked;

    [Header("Message data")]
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _userName;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private TMP_Text _messageTime;
    [SerializeField] private Button _deleteButton;

    [Header("Message width helpers")]
    [SerializeField] private LayoutElement _messageTextLayout;
    [SerializeField] private float _messageTextMaxWidth;

    [Header("Message view helpers")]
    [SerializeField] private GameObject _backgroundWithCorner;
    [SerializeField] private GameObject _backgroundWithoutCorner;
    [SerializeField] private RectTransform _messageTransform;
    [SerializeField] private VerticalLayoutGroup _messageVerticalLayout;
    [SerializeField] private int _bottomOffset; 

    private Message _message;

    public Message Message => _message; 

    public RectTransform MessageTransform => _messageTransform;

    public void Set(Message message)
    {
        _message = message;
        _avatar.sprite = message.User.Avatar;
        _userName.text = message.User.Name;
        _messageText.text = message.MessageText;
        _messageTime.text = message.MessageTime;
        if(_deleteButton != null) {
            _deleteButton.onClick.AddListener(() => DeleteMessage(message));
        }
        SetMessageWidth();
    }

    private void SetMessageWidth()
    {
        Vector2 values = _messageText.GetPreferredValues();
        if (values.x < _messageTextMaxWidth) {
            _messageTextLayout.preferredWidth = values.x;
        } else {
            _messageTextLayout.preferredWidth = _messageTextMaxWidth;
        }
    }

    private void DeleteMessage(Message message)
    {
        if(OnDeleteButtonClicked != null) {
            OnDeleteButtonClicked(message, this);
        }
        _deleteButton.onClick.RemoveAllListeners();
    }

    public void ShowDeleteButton(bool activate)
    {
        if(_deleteButton != null) {
            if (activate) {
                _deleteButton.transform.localScale = Vector3.zero;
                _deleteButton.gameObject.SetActive(activate);
                _deleteButton.transform.DOScale(1, 0.2f);
            } else {
                _deleteButton.gameObject.SetActive(activate);
            }
        }
    }

    public void UpdateView(bool isTheUserLastMessage)
    {
        _avatar.gameObject.SetActive(isTheUserLastMessage);
        _userName.gameObject.SetActive(isTheUserLastMessage);
        _backgroundWithCorner.SetActive(isTheUserLastMessage);
        _backgroundWithoutCorner.SetActive(!isTheUserLastMessage);
        _messageVerticalLayout.padding.bottom = isTheUserLastMessage ?_bottomOffset : 0;
    }
}
