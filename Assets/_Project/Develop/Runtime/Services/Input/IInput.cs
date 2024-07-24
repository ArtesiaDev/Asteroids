using Develop.Runtime.Services.Input.InputActions;

namespace Develop.Runtime.Services.Input
{
    public interface IInput :
        IMoveAction,
        ISteerAction,
        IBulletShootAction,
        ILaserShootAction
    { }
}