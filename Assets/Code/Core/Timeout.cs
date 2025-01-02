using System;
using System.Collections;
using UnityEngine;

public static class Timeout
{
    public static IEnumerator TimeStep(WaitForSecondsRealtime delay, bool isLoop, Action callback) {
        do { yield return TimeDelay(delay, callback); } while (isLoop);
    }
    public static IEnumerator TimeDelay(WaitForSecondsRealtime delay, Action callback) {
        yield return delay; callback?.Invoke();
    }

    public static bool IsExpirationDate(string id) => IsExpirationDate(id, out DateTime time);
    public static bool IsExpirationDate(string id, out DateTime savedTime)
    {
        string date = PlayerPrefs.GetString($"{id}_timeout");
        savedTime = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);
        return savedTime <= DateTime.Now;
    }

    public static int GetExpirationLeft(string id)
    {
        bool isExpired = IsExpirationDate(id, out DateTime savedTime);
        return isExpired ? 0 : DateTime.Now.Subtract(savedTime).Seconds;
    }
    public static int GetExpirationLength(string id)
    {
        bool isExpired = IsExpirationDate(id, out DateTime savedTime);
        return !isExpired ? 0 : DateTime.Now.Subtract(savedTime).Hours;
    }

    public static void SetExpirationDate(string id) => SetExpirationDate(id, 1);
    public static void SetExpirationDate(string id, int hours) => SetExpirationDate(id, hours, -1);
    public static void SetExpirationDate(string id, int hours, int minutes)
    {
        DateTime time = DateTime.Now.AddHours(hours).AddMinutes(minutes);
        PlayerPrefs.SetString($"{id}_timeout", time.ToString());
    }
}