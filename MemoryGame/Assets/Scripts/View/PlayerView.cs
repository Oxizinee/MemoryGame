using Memory.Model;
using Memory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ViewBaseClass<Player>
{
    public Text NameText, ScoreText, TimeText;
    private Player _playerModel;
    public void SetModel(Player player)
    {
        _playerModel = player;

        _playerModel.PropertyChanged += Model_PropertyChanged;
    }
    protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_playerModel.IsActive)
        {
            NameText.text = _playerModel.Name;
            NameText.color = Color.yellow;
        }
        else
        {
            NameText.color = Color.gray;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
