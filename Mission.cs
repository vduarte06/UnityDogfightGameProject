public abstract class Mission
{
    public int sceneId;

    public abstract void UpdateMission();
    public abstract bool IsMissionComplete();
    public abstract string GetMissionKey();
}