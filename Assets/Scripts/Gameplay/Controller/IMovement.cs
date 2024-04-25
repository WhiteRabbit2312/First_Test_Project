namespace TraineeGame
{
    public interface IMovement
    {
        void Controller();

        bool GoLeft();
        bool GoRight();
        bool GoUp();
        bool GoDown();
    }
}