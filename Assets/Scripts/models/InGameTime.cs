using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTime {

    private int hour;
    private int minute;

	public InGameTime(int hour, int minute) {
        this.hour = hour;
        this.minute = minute;
    }

    public int getHour() {
        return this.hour;
    }

    public int getMinute() {
        return this.minute;
    }

    public string getDisplayString() {
        string hourString = "" + hour;
        string minuteString = "";
        if (minute < 10) {
            minuteString = "0" + minute;
        } else {
            minuteString = "" + minute;
        }
        return hourString + ":" + minuteString;
    }

    public static bool isTimesEqual(InGameTime timeA, InGameTime timeB) {
        return (timeA.getHour() == timeB.getHour()) && (timeA.getMinute() == timeB.getMinute());
    }
}
