using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.instance.teamColor = color;//set the team color of the instance of main manager
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        //Select the color saved previously at the start of the class
        ColorPicker.SelectColor(MainManager.instance.teamColor);
    }
    
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        //Save the current selected color in a file so the next time we will found it selected
        MainManager.instance.SaveColor();

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }
    public void SaveColorClicked()
    {
        MainManager.instance.SaveColor();
    }
    public void LoadColorClicked()
    {
        MainManager.instance.LoadColor();
        //Select the color saved previously at the start of the class
        ColorPicker.SelectColor(MainManager.instance.teamColor);
    }
}
