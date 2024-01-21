public class SampleMission : Mission
{
    public int targetScore = 10;
    public int currentScore = 0;

    public override void UpdateMission()
    {
        
        currentScore +=1; 
    }

    public override bool IsMissionComplete()
    {
        
        return currentScore >= targetScore;
    }

    public override string GetMissionName()
    {
        return "Sample Mission";
    }
}




