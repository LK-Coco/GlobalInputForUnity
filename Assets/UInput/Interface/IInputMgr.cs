namespace UInput
{
    public interface IInputMgr
    {
        public void OnInit();

        public void OnStart();

        public void OnUpdate();

        public void OnLateUpdate();

        public void OnStop();

        public void OnClose();
    }
}