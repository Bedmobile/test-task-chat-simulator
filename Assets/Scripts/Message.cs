using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message 
{
    private UserData _user;
    private string _messageText;
    private DateTime _time;
    private bool _isCurrentUser;

    public Message(UserData user, string messageText, DateTime time)
    {
        _user = user;
        _messageText = messageText;
        _time = time;
    }

    public UserData User => _user;
    public string MessageText => _messageText;
    public string MessageTime => _time.Hour.ToString("d2") + ":" + _time.Minute.ToString("d2") + ":" + _time.Second.ToString("d2");
}
