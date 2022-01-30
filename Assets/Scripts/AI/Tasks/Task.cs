namespace Polyjam_2022
{
    public abstract class Task
    {
        protected Action currentAction = null;

        public abstract void Execute(float deltaTime);
    }
}