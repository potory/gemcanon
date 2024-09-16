namespace Codebase.Infrastructure.Abstract
{
    public interface ILoadingScreen
    {
        public void Show();
        public void Hide();

        public void SetProgress(float value);
    }
}