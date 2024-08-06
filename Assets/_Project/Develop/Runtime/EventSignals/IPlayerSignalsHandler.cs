namespace Develop.Runtime.EventSignals
{
    public interface IPlayerSignalsHandler
    {
        void OnPlayerDied(){}
        void OnPlayerMoved(){}
        void OnPlayerSteered(){}
    }
}