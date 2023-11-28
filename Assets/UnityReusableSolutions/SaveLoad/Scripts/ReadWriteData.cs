namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// ������-������ ������.
    /// </summary>
    public abstract class ReadWriteData
    {
        /// <returns>
        /// ��������� ������� ���������� ������.
        /// True - ������ ����.
        /// False - ������ ���.
        /// </returns>
        public abstract bool HasWrittenData();

        public abstract void WriteData<T>(string id, T data);

        public abstract T ReadData<T>(string id);

        /// <returns>
        /// True - ������ ������� �������. False - ������ �� �������.
        /// </returns>
        public abstract bool DeleteData(string id);
    }
}