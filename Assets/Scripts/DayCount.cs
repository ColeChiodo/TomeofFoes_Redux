using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCount : MonoBehaviour
{
    static int prevDay = 0;
    static int currentDay = 0;
    static int actionsDone;
    static int maxActions;

    public void Initialize()
    {
        prevDay = 0;
        currentDay = 1;
        actionsDone = 0;
        maxActions = 10;
    }

    public void incActions(int n)
    {
        actionsDone += n;
        if(actionsDone >= maxActions)
        {
            actionsDone -= maxActions;
            maxActions++;
            prevDay = currentDay;
            currentDay++;
        }
    }

    public int getCurrentDay()
    {
        return currentDay;
    }

    public int getPrevDay()
    {
        int temp = prevDay;
        prevDay = currentDay;
        return temp;
    }

    public void SkipDay()
    {
        prevDay = currentDay;
        currentDay++;
        actionsDone = 0;
        maxActions++;
    }
}
