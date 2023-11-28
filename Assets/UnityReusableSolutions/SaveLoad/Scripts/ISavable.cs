namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// Интерфейс запросов на операции с сохраняемыми данными конкретного объекта.
    /// </summary>
    public interface ISavable
    {
        void Save();

        void Load();

        void DeleteSaves();
    }
}