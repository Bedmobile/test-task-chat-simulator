using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScreenController 
{
    private ChatScreenModel _chatScreenModel;
    private ChatScreenView _chatScreenView;

    public ChatScreenController(ChatScreenModel chatScreenModel, ChatScreenView screen)
    {
        _chatScreenModel = chatScreenModel;
        _chatScreenView = screen;
        _chatScreenView.OnSendButtonClicked += _chatScreenModel.CreateMessage;
        _chatScreenView.OnDeleteButtonClicked += _chatScreenModel.DeleteMessage;
        _chatScreenModel.OnMessageCreated += _chatScreenView.CreateMessage;
        _chatScreenModel.OnUpdateMessageView += _chatScreenView.UpdateMessageView;
    }
}
