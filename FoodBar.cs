using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxProgress(int actullyFood)
    {
        slider.maxValue = actullyFood;
        slider.value = actullyFood;
    }
    public void SetProgress(int actullyFood)
    {
        slider.value = actullyFood;
    }
}
