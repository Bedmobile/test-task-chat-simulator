using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserData
{
    [SerializeField] [ReadOnly] private int _id = 0;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _avatar;

    public int ID => _id;
    public string Name {
        get => _name;
        set => _name = value;
    }
    public Sprite Avatar {
        get => _avatar;
        set => _avatar = value;
    }

    public UserData(string name, Sprite avatar)
    {
        Name = name;
        Avatar = avatar;
    }

    public UserData() : this("-", null)
    {
    }

    public void SetUserID(List<UserData> usersList)
    {
        if (_id != 0) {
            return;
        }

        if(usersList.Count > 0) {
            int maxID = 0;
            foreach(UserData user in usersList) {
                if(user.ID > maxID) {
                    maxID = user.ID;
                }
            }
            _id = maxID + 1;
        } else {
            _id = 1;
        }

        usersList.Add(this);
    }
}