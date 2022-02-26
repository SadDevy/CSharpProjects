namespace TasksUI
{
    public class DownloadedPortion
    {
        public int Id { get; private set; }
        public byte[] Data { get; private set; }
        public int Length { get; private set; }

        public DownloadedPortion(int id, int length, byte[] data)
        {
            Id = id;
            Length = length;
            Data = data;
        }
    }
}
