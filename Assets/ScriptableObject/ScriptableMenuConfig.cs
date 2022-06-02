using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/MenuConfig", order = 2)]

public class ScriptableMenuConfig : ScriptableObject
{
   public bool mainMenuLevel;
   
   public string levelLoadId;  
   public string levelName;

   public bool lockLevel = true;

   public string nextLevelId;

   // for button 
   public string levelTextIndex;

   //open GameObecjt

}
