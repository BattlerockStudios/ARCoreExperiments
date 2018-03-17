using InControl;

public class PaddleActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerOneAxisAction Move;

    public PlayerAction Forward;
    public PlayerAction Backward;
    public PlayerTwoAxisAction Move2;

    public PaddleActions()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Move = CreateOneAxisPlayerAction(Left, Right);

        Forward = CreatePlayerAction("Move Forward");
        Backward = CreatePlayerAction("Move Backward");
        Move2 = CreateTwoAxisPlayerAction(Left, Right, Forward, Backward);
    }
}
