namespace UInput
{
    public interface IProvider
    {
        public void Init();

        public void Begin();

        public void End();

        public void Close();
    }
}
