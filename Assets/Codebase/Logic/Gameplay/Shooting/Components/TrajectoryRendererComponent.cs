using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Components
{
    public class TrajectoryRendererComponent : MonoBehaviour
    {
        [Header("Components")]
        
        [SerializeField] private LineRenderer _lineRendererA;
        [SerializeField] private LineRenderer _lineRendererB;

        [Header("Single line")]
        [SerializeField] private Color _normalColor = new(1, 1, 1, 1);
        [SerializeField] private Color _forbiddenColor = Color.red;
        
        [Header("Double line")]
        [SerializeField] private Color _startColor = new(1, 1, 1, 1);
        [SerializeField] private Color _endColor = new(1, 1, 1, 0);

        public void RenderSingle(Trajectory trajectory)
        {
            _lineRendererB.enabled = false;
            
            InternalRender(trajectory, _lineRendererA);
            
            _lineRendererA.startColor = _normalColor;
            _lineRendererA.endColor = _normalColor;
        }
        
        public void RenderForbidden(Trajectory trajectory)
        {
            _lineRendererB.enabled = false;
            
            InternalRender(trajectory, _lineRendererA);
            
            _lineRendererA.startColor = _forbiddenColor;
            _lineRendererA.endColor = _forbiddenColor;
        }

        public void RenderLineA(Trajectory borderA)
        {
            InternalRender(borderA, _lineRendererA);
            
            _lineRendererA.startColor = _startColor;
            _lineRendererA.endColor = _endColor;
        }

        public void RenderLineB(Trajectory borderB)
        {
            InternalRender(borderB, _lineRendererB);
            
            _lineRendererB.startColor = _startColor;
            _lineRendererB.endColor = _endColor;
        }

        private void InternalRender(Trajectory trajectory, LineRenderer lineRenderer)
        {
            lineRenderer.enabled = true;
            
            lineRenderer.positionCount = trajectory.Path.PointsCount;
            lineRenderer.SetPositions(trajectory.Path.Points);
        }

        public void Disable()
        {
            _lineRendererA.enabled = false;
            _lineRendererB.enabled = false;
        }
    }
}