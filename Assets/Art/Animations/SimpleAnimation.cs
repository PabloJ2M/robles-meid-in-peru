using UnityEngine;

[RequireComponent(typeof(Animation))]
public class SimpleAnimation : MonoBehaviour
{
    private Animation _animation;

    private void Awake() => _animation = GetComponent<Animation>();
    private void Start() => _animation.Play();
}