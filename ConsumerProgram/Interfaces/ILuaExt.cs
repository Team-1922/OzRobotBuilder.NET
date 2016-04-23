namespace ConsumerProgram
{
    public interface ILuaExt
    {
        //this should return a formatted script with the extended class
        //  features (extended features CAN be per-object; that's how they
        //  are treated anyways)
        string GetFormattedExtScriptText();
    }
}
