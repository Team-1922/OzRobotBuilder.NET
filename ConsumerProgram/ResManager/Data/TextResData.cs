namespace ConsumerProgram.ResManager.Data
{
    class TextResData : IResData
    {
        public string Data { get; protected set; }

        public TextResData(string data)
        {
            Data = data;
        }

        public string GetHumanReadable()
        {
            return Data;
        }

        public void CleanUp()
        {
            //nothing to do here, becuase "Data" Gets removed automatically
        }
    }
}
