using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSimulation : MonoBehaviour
{
    [SerializeField] private UsersConfig _users;
    [SerializeField] private ChatScreenView _screen;

    private ChatScreenController _chatScreen;

    void Start()
    {
        int currenUserID = _users.CurrentUser.ID;
        List<UserData> users = _users.Users;
        _chatScreen = new ChatScreenController(new ChatScreenModel(currenUserID, users), _screen);
    }
}