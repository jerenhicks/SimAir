using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public static TimeController instance = null;
    private int hour = 0;
    private int minute = 0;
    private float lastChange = 0f;
    private static float normalSpeed = 1.0f;
    private static float fastSpeed = 0.5f;
    private static float veryFastSpeed = 0.25f;
    private float currentSpeed = normalSpeed;
    private bool playing = false;
    private List<TimeEventListener> listeners = null;
    public event EventHandler handler; 

    // Use this for initialization
    void Start() {
        listeners = new List<TimeEventListener>();
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.playing) {
            if (Time.time - lastChange > currentSpeed) {
                this.addTime();
                this.notifyListeners();
                lastChange = Time.time;
            }
        }
    }

    public InGameTime getTime() {
        InGameTime time = new InGameTime(hour, minute);
        return time;
    }

    public void pause() {
        this.playing = false;
    }

    public void play() {
        this.playing = true;
    }

    public void setNormalSpeed() {
        this.currentSpeed = normalSpeed;
    }

    public void setFastSpeed() {
        this.currentSpeed = fastSpeed;
    }

    public void setVeryFastSpeed() {
        this.currentSpeed = veryFastSpeed;
    }

    public bool isPlaying() {
        return this.playing;
    }

    public bool isNormalSpeed() {
        return this.currentSpeed == normalSpeed;
    }

    public bool isFastSpeed() {
        return this.currentSpeed == fastSpeed;
    }

    public bool isVeryFastSpeed() {
        return this.currentSpeed == veryFastSpeed;
    }

    public float getCurrentSpeedValue() {
        return this.currentSpeed;
    }

    private void addTime() {
        minute++;
        if (minute >= 60) {
            minute = 0;
            hour++;
        }
        if (hour >= 24) {
            hour = 0;
        }
    }

    public void addListener(TimeEventListener listener) {
        this.listeners.Add(listener);
    }

    private void notifyListeners() {
        if (handler != null) {
            handler(this, null);
        }
    }
}
