namespace StealthTest.Characters
{
    using System;
    using UniRx;

    public class EnemyCharacterModel : ICharacterModel
    {
        private EnemyCharacterController enemyCharacterController;

        private float fireCooldown;
        private float remainingTime;
        private bool isReadyToFire;

        private Subject<Unit> fireCooldownSubject = new Subject<Unit>();

        private CompositeDisposable disposables = new();

        public EnemyCharacterModel(ICharacterData characterData)
        {
            fireCooldown = ((EnemyCharacterData)characterData).FireCooldown;
            remainingTime = fireCooldown;
            isReadyToFire = false;
        }

        public void Initialize(CharacterController characterController)
        {
            enemyCharacterController = (EnemyCharacterController)characterController;
            Observable.Timer(TimeSpan.FromSeconds(remainingTime))
                .Repeat()
                .Subscribe(_ =>
                {
                    isReadyToFire = true;
                    fireCooldownSubject.OnNext(Unit.Default);
                })
                .AddTo(disposables);
            enemyCharacterController.TryFireFunc += OnTryFireFunc;
            enemyCharacterController.DisposeCommand.Subscribe(_ => Dispose()).AddTo(disposables);
        }

        public void Dispose()
        {
            disposables.Dispose();
            enemyCharacterController.TryFireFunc -= OnTryFireFunc;
        }

        private bool OnTryFireFunc()
        {
            if (isReadyToFire)
            {
                isReadyToFire = false;
                remainingTime = fireCooldown;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
