using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeAndCompletionDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SewerBestTimeDisplay;
    [SerializeField] TextMeshProUGUI SewerCompletionDisplay;
    [SerializeField] TextMeshProUGUI RooftopBestTimeDisplay;
    [SerializeField] TextMeshProUGUI RooftopCompletionDisplay;
    [SerializeField] TextMeshProUGUI ApartmentBestTimeDisplay;
    [SerializeField] TextMeshProUGUI ApartmentCompletionDisplay;

    // Start is called before the first frame update
    void Start()
    {
        SewerBestTimeDisplay.text = "Best time: " + SavedData.sewerBestTime.ToString();
        RooftopBestTimeDisplay.text = "Best time: " + SavedData.rooftopBestTime.ToString();
        ApartmentBestTimeDisplay.text = "Best time: " + SavedData.apartmentBestTime.ToString();

        if (SavedData.apartmentCompletion)
        {
            ApartmentCompletionDisplay.text = "Level Completed";
        }
        if (SavedData.rooftopCompletion)
        {
           RooftopCompletionDisplay.text = "Level Completed";
        }
        if (SavedData.sewerCompletion)
        {
            SewerCompletionDisplay.text = "Level Completed";
        }
    }
}
