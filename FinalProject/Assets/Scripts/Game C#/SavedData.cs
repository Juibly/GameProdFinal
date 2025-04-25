using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedData : MonoBehaviour
{
    //best times for each level, game opens with them all being set to 60. perhaps in the best time display in the hub,
    //we can have it check for if the best time is 60 or above and not show it. or only show best time after the levels have been beaten.
    public static float rooftopBestTime = 60;
    public static float sewerBestTime = 60;
    public static float apartmentBestTime = 60;

    //bools for level completion to be used as needed.
    public static bool rooftopCompletion = false;
    public static bool sewerCompletion = false;
    public static bool apartmentCompletion = false;
}