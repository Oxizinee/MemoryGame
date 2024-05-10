using Memory.Model;
using Memory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ViewBaseClass<Player>
{
    public Text NameText, ScoreText, TimeText;
    private Player _playerModel;
    public void SetModel(Player player)
    {
       _playerModel = player;
       Model = _playerModel;

        NameText.text = player.Name;
        ScoreText.text = _playerModel.Score.ToString();
        TimeText.text = _playerModel.Elapsed.ToString();    
    }
    protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_playerModel.IsActive)
        {
            ScoreText.text = _playerModel.Score.ToString();
            TimeText.text = _playerModel.Elapsed.ToString("F2");
            NameText.color = Color.yellow;
            ScoreText.color = Color.yellow;
            TimeText.color = Color.yellow;
        }
        else
        {
            NameText.color = Color.gray;
            ScoreText.color = Color.gray;
            TimeText.color = Color.gray;
        }
    }

}
