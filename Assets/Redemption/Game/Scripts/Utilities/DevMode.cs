﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMode : MonoBehaviour
{
    bool devmode;

    int pressed;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            if(!devmode)
            {
                pressed++;
                if (pressed > 5)
                    pressed = 5;

                if (pressed == 5)
                {
                    devmode = true;
                }
            }
            else
            {
                pressed--;
                if (pressed < 0)
                    pressed = 0;

                if(pressed == 0)
                {
                    devmode = false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && devmode)
        {
            ResetPlayerPrefs();
        }
	}

    void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("NewGame", 0);
        PlayerPrefs.SetFloat("Experience", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("SkillPoints", 0);

        print("Player Prefs Reset");
    }
}
