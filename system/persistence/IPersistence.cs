public interface IPersistence
{
    void OnSave(PersistenceData persistenceData);
    void OnLoad(PersistenceData persistenceData);
}