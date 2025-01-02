using System;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private const int _bandLength = 8;
    private float[] _spectrumData = new float[512];
    private float[] _previousBandLevels = new float[_bandLength];

    public event Action<float[], float[]> onUpdate;

    private void Update()
    {
        //Obtener datos del espectro
        _source.GetSpectrumData(_spectrumData, 0, FFTWindow.BlackmanHarris);

        //Calcular niveles por bandas de frecuencia
        float[] currentBandLevels = new float[_bandLength];
        for (int i = 0; i < _bandLength; i++) currentBandLevels[i] = GetFrequencyBandLevel(i);

        onUpdate?.Invoke(currentBandLevels, _previousBandLevels);

        //Actualizar niveles previos
        _previousBandLevels = currentBandLevels;
    }

    private float GetFrequencyBandLevel(int bandIndex)
    {
        int start = (int)Mathf.Pow(2, bandIndex) - 1;
        int width = (int)Mathf.Pow(2, bandIndex);

        float sum = 0f;
        for (int i = start; i < start + width; i++) sum += _spectrumData[i] * (i + 1);
        return Mathf.Log10(sum + 1);
    }
}