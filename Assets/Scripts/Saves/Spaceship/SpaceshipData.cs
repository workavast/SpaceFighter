namespace Saves.Spaceship
{
    public class SpaceshipData : DataBase<SpaceshipSave>
    {
        public int CurrentSpaceshipLevel { get; private set; }

        public SpaceshipData()
        {
            CurrentSpaceshipLevel = 1;
        }
        
        public override void SetData(SpaceshipSave save)
        {
            CurrentSpaceshipLevel = save.CurrentSpaceshipLevel;
        }

        public void IncreaseSpaceshipLevel()
        {
            CurrentSpaceshipLevel++;
        }
    }
}