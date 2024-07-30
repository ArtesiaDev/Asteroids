namespace Develop.Runtime.Meta.EventSignals
{
    public interface IPlayerSignalsHandler
    {
        void OnPlayerDied(){}
        void OnPlayerMoved(){}
        void OnPlayerSteered(){}
    }
}