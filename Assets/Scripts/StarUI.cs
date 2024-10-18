using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public Image[] starImages; 
    public Sprite starLocked; 
    public Sprite starUnlocked; 

    private bool[] isStarUnlocked = new bool[3];

    public void UnlockStar(int index)
    {
        isStarUnlocked[index] = true;
        starImages[index].sprite = starUnlocked;
    }

    public void LockStar(int index)
    {
        isStarUnlocked[index] = false;
        starImages[index].sprite = starLocked;
    }
}
