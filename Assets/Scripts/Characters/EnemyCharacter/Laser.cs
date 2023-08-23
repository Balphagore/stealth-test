namespace StealthTest.Characters
{
    using UniRx;
    using UnityEngine;

    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private float laserDuration = 0.5f;

        private Transform target;
        private CompositeDisposable disposables = new();

        public void Fire(Transform start, Transform target)
        {
            this.target = target;
            Vector3 startPos = start.position;
            Vector3 endPos = target.position;

            DrawLine(startPos, endPos);
        }

        private void Update()
        {
            if (target != null)
            {
                Vector3 endPos = target.position;
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(endPos));
            }
        }

        private void DrawLine(Vector3 startPos, Vector3 endPos)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.InverseTransformPoint(startPos));
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(endPos));

            Observable.Timer(System.TimeSpan.FromSeconds(laserDuration))
                .Subscribe(_ =>
                {
                    lineRenderer.positionCount = 0;
                    target = null;
                });
        }

        private void OnDisable()
        {
            disposables.Dispose();
        }
    }
}
