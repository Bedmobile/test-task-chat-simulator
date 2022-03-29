using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Users", menuName = "ScriptableObjects/Create Users Config", order = 1)]
public class UsersConfig : ScriptableObject
{
    [SerializeField] private UserData _currentUser = new UserData();
    [SerializeField] private List<UserData> _users = new List<UserData>();

    [SerializeField] private string _newUserName;
    [SerializeField] private Sprite _newUserAvatar;

    public UserData CurrentUser => _currentUser;
    public List<UserData> Users => _users;

    public string NewUserName {
        get => _newUserName;
        set => _newUserName = value;
    }
    public Sprite NewUserAvatar {
        get => _newUserAvatar;
        set => _newUserAvatar = value;
    }
    

    public void AddNewUser()
    {
        if(NewUserName == "" || NewUserAvatar == null) {
            return;
        }

        UserData newUser = new UserData(NewUserName, NewUserAvatar);
        newUser.SetUserID(_users);

        if(_currentUser.ID == 0) {
            SetCurrentUserByIndex(0);
        }
    }

    public void SetCurrentUserByIndex(int index)
    {
        _currentUser = _users[index];
    }

    public void DeleteUserByIndex(int index)
    {
        try {
            int targetID = _users[index].ID;
            _users.RemoveAt(index);
            if (_users.Count > 0) {
                if (_currentUser.ID == targetID) {
                    SetCurrentUserByIndex(0);
                }
            } else {
                _currentUser = new UserData();
            }
            
        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
        }
        
    }

    private UserData GetUserByID(int id)
    {
        foreach(UserData user in _users) {
            if(user.ID == id) {
                return user;
            }
        }
        Debug.LogError("The user with this id was not found");
        return null;
    }
}