namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    /// <summary>
    /// Чтение-запись данных.
    /// </summary>
    public abstract class ReadWriteData
    {
        /// <returns>
        /// Проверить наличие записанных данных.
        /// True - данные есть.
        /// False - данных нет.
        /// </returns>
        public abstract bool HasWrittenData();

        public abstract void WriteData<T>(string id, T data);

        public abstract T ReadData<T>(string id);

        /// <returns>
        /// True - данные успешно удалены. False - данные не найдены.
        /// </returns>
        public abstract bool DeleteData(string id);
    }
}