namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// ��������� �������� �� �������� � ������������ ������� ����������� �������.
    /// </summary>
    public interface ISavable
    {
        void Save();

        void Load();

        void DeleteSaves();
    }
}