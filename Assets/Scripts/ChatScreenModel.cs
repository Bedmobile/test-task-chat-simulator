using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScreenModel
{
    public delegate void MessageCreated(Message message, bool isCurrentUser);
    public event MessageCreated OnMessageCreated;
    public delegate void UpdateMessageView(Message message, bool isTheUserLastMessage);
    public event UpdateMessageView OnUpdateMessageView;
    
    public List<Message> Messages = new List<Message>();
    private int _currentUserID;
    private List<UserData> _users;


    public ChatScreenModel(int currentUserID, List<UserData> users)
    {
        _currentUserID = currentUserID;
        _users = users;
    }

    public void CreateMessage(string messageText) 
    {
        int randomUserIndex = UnityEngine.Random.Range(0, _users.Count);
        Message message = new Message(_users[randomUserIndex], messageText, DateTime.Now);
        Messages.Add(message);
        if(OnMessageCreated != null) {
            OnMessageCreated(message, message.User.ID == _currentUserID);
        }
        UpdateChatScreen();
    }

    public void DeleteMessage(Message message) {
        Messages.Remove(message);
        UpdateChatScreen();
    }

    private void UpdateChatScreen()
    {
        if (OnUpdateMessageView != null) {
            for (int i = Messages.Count - 1; i >= 0; i--) {
                bool isThisUserLastMessage = true;
                if(i < Messages.Count - 1) {
                    if(Messages[i].User.ID == Messages[i + 1].User.ID) {
                        isThisUserLastMessage = false;
                    }
                }
                OnUpdateMessageView(Messages[i], isThisUserLastMessage);
            }
        }
    }
}
