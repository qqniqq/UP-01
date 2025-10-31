

namespace Hospital.Lib
{
    public interface IDrugRepository
    {
        // Создать медикамент. Возвращает true при успехе, false при ошибке.
        bool CreateDrug(Drug drug, out int createdId);

        Drug GetDrugById(int id);
    }
}
