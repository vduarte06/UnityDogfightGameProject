public class Mission1 : Mission
{
    private string key = "Stage0";
    public int targetScore = 10;
    public int currentScore = 0;

    public override void UpdateMission()
    {
        

    }

    public override bool IsMissionComplete()
    {
        
        return currentScore >= targetScore;
    }

    public override string GetMissionKey()
    {
        return key;
    }

}




