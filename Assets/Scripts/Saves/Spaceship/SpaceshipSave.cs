namespace Saves.Spaceship
{
    public class SpaceshipSave : SaveBase<SpaceshipData>
    {
        public int CurrentSpaceshipLevel;

        public override void SetData(SpaceshipData data)
        {
            CurrentSpaceshipLevel = data.CurrentSpaceshipLevel;
        }
    }
}