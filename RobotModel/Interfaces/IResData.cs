namespace RobotModel.Interfaces
{
    //resource data should be independent of other resources, therefore the order of which "CleanUp()" is called on each resource
    //      can NOT matter.
    public interface IResData
    {
        string GetHumanReadable();

        void CleanUp();
    }
}
