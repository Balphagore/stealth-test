namespace StealthTest
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UniRx;

    public class PlayerInputSystem : MonoBehaviour
    {
        private Subject<Vector2> movementSubject = new Subject<Vector2>();

        public ReactiveCommand SwitchCrouch = new ReactiveCommand();
        public IObservable<Vector2> MovementValue => movementSubject;

        public void OnMovement(InputValue value)
        {
            movementSubject.OnNext(value.Get<Vector2>());
        }

        public void OnCrouch(InputValue value)
        {
            SwitchCrouch.Execute();
        }
    }
}
